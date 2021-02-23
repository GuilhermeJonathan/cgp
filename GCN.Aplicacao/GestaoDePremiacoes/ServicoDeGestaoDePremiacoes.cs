using Campeonato.Aplicacao.GestaoDePremiacoes.Modelos;
using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDePremiacoes
{
    public class ServicoDeGestaoDePremiacoes : IServicoDeGestaoDePremiacoes
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        public ServicoDeGestaoDePremiacoes(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        public ModeloDeListaDePremiacoes RetonarPremiacoesPorFiltro(ModeloDeFiltroDePremiacao filtro, int pagina, int registrosPorPagina = 30)
        {
            int quantidadeEncontrada = 0;
            var premiacoes = this._servicoExternoDePersistencia.RepositorioDePremiacoes.RetornarPremiacoesPorFiltro(filtro.Rodada, filtro.Usuario, pagina, registrosPorPagina, out quantidadeEncontrada);

            return new ModeloDeListaDePremiacoes(premiacoes, quantidadeEncontrada, filtro);
        }

        public string CadastrarPremiacao(ModeloDeCadastroDePremiacao modelo, UsuarioLogado usuario)
        {
            if (modelo.Rodada == 0)
                throw new ExcecaoDeAplicacao("Deve-se escolher obrigatoriamente uma rodada.");

            if (modelo.UsuarioPrimeiro == 0)
                throw new ExcecaoDeAplicacao("Deve-se escolher obrigatoriamente o primeiro colocado.");

            if (modelo.UsuarioSegundo == 0)
                throw new ExcecaoDeAplicacao("Deve-se escolher obrigatoriamente o segundo colocado.");

            if (modelo.UsuarioPrimeiro == modelo.UsuarioSegundo)
                throw new ExcecaoDeAplicacao("Os ganhadores devem ser diferentes.");

            var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
            var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(modelo.Rodada);

            if(rodada.LancouPremiacao)
                throw new ExcecaoDeAplicacao("A Premiação ja foi gerada para esta rodada.");

            var usuarioPrimeiro = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(modelo.UsuarioPrimeiro);
            var usuarioSegundo = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(modelo.UsuarioSegundo);

            var valorTotal = !String.IsNullOrEmpty(modelo.ValorTotal) ? Convert.ToDecimal(modelo.ValorTotal) : 0;
            var valorAcumulado = !String.IsNullOrEmpty(modelo.ValorAcumulado) ? Convert.ToDecimal(modelo.ValorAcumulado) : 0;
            var valorAdministracao = !String.IsNullOrEmpty(modelo.ValorAdministracao) ? Convert.ToDecimal(modelo.ValorAdministracao) : 0;
            var valorPremiacaoPrimeiro = !String.IsNullOrEmpty(modelo.ValorPremiacaoPrimeiro) ? Convert.ToDecimal(modelo.ValorPremiacaoPrimeiro) : 0;
            var valorPremiacaoSegundo = !String.IsNullOrEmpty(modelo.ValorPremiacaoSegundo) ? Convert.ToDecimal(modelo.ValorPremiacaoSegundo) : 0;

            var novaPremiacao = new Premiacao(rodada, usuarioPrimeiro, usuarioSegundo, valorTotal, valorPremiacaoPrimeiro, valorPremiacaoSegundo, valorAcumulado, valorAdministracao, usuarioBanco);
            
            this._servicoExternoDePersistencia.RepositorioDePremiacoes.Inserir(novaPremiacao);
            if (novaPremiacao != null)
            {
                rodada.LancarPremiacao(usuarioBanco);
                if (modelo.GerarCredito)
                {
                    if (valorPremiacaoPrimeiro > 0)
                        this._servicoDeGestaoDeUsuarios.CadastrarSaldoParaPremiacao(usuarioPrimeiro, valorPremiacaoPrimeiro, usuario, $"Primeiro Colocado na {rodada.Nome}.");
                    
                    if (valorPremiacaoSegundo > 0)
                        this._servicoDeGestaoDeUsuarios.CadastrarSaldoParaPremiacao(usuarioSegundo, valorPremiacaoSegundo, usuario, $"Segundo Colocado na {rodada.Nome}.");
                }
            }

            this._servicoExternoDePersistencia.Persistir();

            return "Premiação cadastrada com sucesso.";
        }

        public ModeloDeEdicaoDePremiacao BuscarPremiacaoPorId(int id)
        {
            try
            {
                var premiacao = this._servicoExternoDePersistencia.RepositorioDePremiacoes.PegarPorId(id);
                return new ModeloDeEdicaoDePremiacao(premiacao);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar premiação");
            }
        }
    }
}
