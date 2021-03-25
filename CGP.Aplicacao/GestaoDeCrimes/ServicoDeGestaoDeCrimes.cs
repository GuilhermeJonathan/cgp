using Cgp.Aplicacao.GestaoDeCrimes.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes
{
    public class ServicoDeGestaoDeCrimes : IServicoDeGestaoDeCrimes
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCrimes(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeCrimes RetonarCrimesPorFiltro(ModeloDeFiltroDeCrime filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var batalhoes = this._servicoExternoDePersistencia.RepositorioDeCrimes.RetornarCrimesPorFiltro(filtro.Nome, filtro.Artigo, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeCrimes(batalhoes, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os crimes");
            }
        }

        public ModeloDeEdicaoDeCrime BuscarCrimePorId(int id)
        {
            try
            {
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.PegarPorId(id);
                return new ModeloDeEdicaoDeCrime(crime);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar crime");
            }
        }

        public IList<Crime> RetonarTodosOsCrimesAtivos()
        {
            var crimes = this._servicoExternoDePersistencia.RepositorioDeCrimes.RetornarTodosOsCrimesAtivos();
            return crimes;
        }

        public string CadastrarCrime(ModeloDeCadastroDeCrime modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                
                var novoCrime = new Crime(modelo.Nome, modelo.Artigo, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeCrimes.Inserir(novoCrime);
                this._servicoExternoDePersistencia.Persistir();

                return "Crime incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o crime: " + ex.InnerException);
            }
        }

        public string AlterarDadosDoCrime(ModeloDeEdicaoDeCrime modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.PegarPorId(modelo.Id);
                crime.AlterarDados(modelo.Nome, modelo.Artigo, modelo.Ativo, usuarioBanco);

                this._servicoExternoDePersistencia.Persistir();

                return "Crime alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Crime: " + ex.InnerException);
            }
        }

        public string AtivarCrime(int id, UsuarioLogado usuario)
        {
            try
            {
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (crime != null)
                {
                    if (crime.Ativo)
                        crime.Inativar(usuarioBanco);
                    else
                        crime.Ativar(usuarioBanco);
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Crime alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Crime: " + ex.InnerException);
            }
        }
    }
}
