using Cgp.Aplicacao.GestaoDeApostas.Modelos;
using Cgp.Aplicacao.GestaoDePremiacoes;
using Cgp.Aplicacao.GestaoDePremiacoes.Modelos;
using Cgp.Aplicacao.GestaoDeRodada;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
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

        [TratarErros]
        [HttpGet]
        public ActionResult Visualizar(int? id)
        {
            if (!id.HasValue)
            {
                this.AdicionarMensagemDeErro("Premiação não foi encontrada");
                return RedirectToAction(nameof(Index));
            }

            var modelo = this._servicoDeGestaoDePremiacoes.BuscarPremiacaoPorId(id.Value);
            return View(modelo);
        }

        [HttpPost]
        public ActionResult GerarPremiacao(ModeloDeCadastroDePremiacao modelo)
        {
            var retorno = this._servicoDeGestaoDePremiacoes.CadastrarPremiacao(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult PremiacaoNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("Premiação não foi encontrada");
            return RedirectToAction(nameof(Index));
        }

        private ActionResult UsuarioSemPermissao()
        {
            this.AdicionarMensagemDeErro("Usuário sem permissão para esta funcionalidade.");
            return RedirectToAction(nameof(Index));
        }
    }
}