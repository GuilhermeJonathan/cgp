using Campeonato.Aplicacao.GestaoDeApostas.Modelos;
using Campeonato.Aplicacao.GestaoDePremiacoes;
using Campeonato.Aplicacao.GestaoDePremiacoes.Modelos;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Dominio.Entidades;
using Campeonato.Filter;
using Campeonato.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    [TratarErros]
    [Authorize]
    public class PremiacaoController : Controller
    {
        private readonly IServicoDeGestaoDePremiacoes _servicoDeGestaoDePremiacoes;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;

        public PremiacaoController(IServicoDeGestaoDePremiacoes servicoDeGestaoDePremiacoes, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas, IServicoDeGestaoDeApostas servicoDeGestaoDeApostas)
        {
            this._servicoDeGestaoDePremiacoes = servicoDeGestaoDePremiacoes;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
        }

        public ActionResult Index(ModeloDeListaDePremiacoes modelo)
        {
            if (!User.EhAdministrador())
                UsuarioSemPermissao();

            modelo.Filtro.Usuarios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                        () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                    () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo = this._servicoDeGestaoDePremiacoes.RetonarPremiacoesPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [TratarErros]
        [HttpGet]
        public ActionResult GerarPremiacao(int? id)
        {
            if (!id.HasValue)
            {
                this.AdicionarMensagemDeErro("Rodada não foi encontrada");
                return RedirectToAction(nameof(Index));
            }

            var modelo = new ModeloDeCadastroDePremiacao();

            modelo.UsuariosPrimeiro = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                       () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

            modelo.UsuariosSegundo = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

            modelo.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                      () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            var rodada = this._servicoDeGestaoDeRodadas.BuscarRodadaParaPremiacao(id.Value);

            if (rodada != null)
            {
                modelo.Rodada = rodada.Id;
                modelo.ValorTotal = rodada.ValorDasApostas.ToString("f");
                modelo.UsuarioPrimeiro = rodada.PrimeiroColocado;
                modelo.UsuarioSegundo = rodada.SegundoColocado;

                var percentualAdministracao = VariaveisDeAmbiente.Pegar<int>("percentualAdministracao");
                var percentualPrimeiro = VariaveisDeAmbiente.Pegar<int>("percentualPrimeiro");
                var percentualSegundo = VariaveisDeAmbiente.Pegar<int>("percentualSegundo");
                var percentualAcumulado = VariaveisDeAmbiente.Pegar<int>("percentualAcumulado");

                modelo.ValorAdministracao = Convert.ToDecimal((rodada.ValorDasApostas * percentualAdministracao) / 100).ToString("f");
                modelo.ValorAcumulado = Convert.ToDecimal((rodada.ValorDasApostas * percentualAcumulado) / 100).ToString("f");
                modelo.ValorPremiacaoPrimeiro = Convert.ToDecimal((rodada.ValorDasApostas * percentualPrimeiro) / 100).ToString("f");
                modelo.ValorPremiacaoSegundo = Convert.ToDecimal((rodada.ValorDasApostas * percentualSegundo) / 100).ToString("f");
            }

            return View(modelo);
        }

        [HttpPost]
        public ActionResult GerarPremiacao(ModeloDeCadastroDePremiacao modelo)
        {
            var retorno = this._servicoDeGestaoDePremiacoes.CadastrarPremiacao(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult RodadaNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("Rodada não foi encontrada");
            return RedirectToAction(nameof(Index));
        }

        private ActionResult UsuarioSemPermissao()
        {
            this.AdicionarMensagemDeErro("Usuário sem permissão para esta funcionalidade.");
            return RedirectToAction(nameof(Index));
        }
    }
}