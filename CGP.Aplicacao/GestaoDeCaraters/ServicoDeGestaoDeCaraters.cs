using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters
{
    public class ServicoDeGestaoDeCaraters : IServicoDeGestaoDeCaraters
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCaraters(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeCaraters RetonarCaratersPorFiltro(ModeloDeFiltroDeCarater filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorFiltro(filtro.Cidade, filtro.Crime, out quantidadeEncontrada);

                return new ModeloDeListaDeCaraters(caraters, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }

        public ModeloDeEdicaoDeCarater BuscarCaraterPorId(int id)
        {
            try
            {
                var carater = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarPorId(id);
                return new ModeloDeEdicaoDeCarater(carater);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar caráter");
            }
        }

        public string CadastrarCarater(ModeloDeCadastroDeCarater modelo, UsuarioLogado usuario)
        {
            try
            {
                DateTime dataHoraFato = new DateTime();
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.BuscarPorId(modelo.Crime);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);
                var veiculo = this._servicoExternoDePersistencia.RepositorioDeVeiculos.PegarPorPlaca(modelo.Placa);

                if (veiculo == null)
                {
                    veiculo = new Veiculo(modelo.Placa.ToUpper(), modelo.MarcaVeiculo.ToUpper(), modelo.ModeloVeiculo.ToUpper(), modelo.AnoVeiculo, modelo.CorVeiculo.ToUpper());
                    this._servicoExternoDePersistencia.Persistir();
                }

                if (!string.IsNullOrEmpty(modelo.Data) && !string.IsNullOrEmpty(modelo.Hora))
                {
                    var data = Convert.ToDateTime(modelo.Data);
                    var hora = Convert.ToDateTime(modelo.Hora);
                    dataHoraFato = new DateTime(data.Year, data.Month, data.Day, hora.Hour, hora.Minute, 0);
                }

                var novoCarater = new Carater(modelo.Descricao, modelo.ComplementoEndereco, dataHoraFato, veiculo, cidade, crime, modelo.UrlImagem, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeCaraters.Inserir(novoCarater);
                this._servicoExternoDePersistencia.Persistir();

                return "Caráter incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o Caráter: " + ex.InnerException);
            }
        }

        public string AlterarDadosDoCarater(ModeloDeEdicaoDeCarater modelo, UsuarioLogado usuario)
        {
            try
            {
                DateTime dataHoraFato = new DateTime();
                var carater = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarPorId(modelo.Id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.BuscarPorId(modelo.Crime);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);
                var veiculo = this._servicoExternoDePersistencia.RepositorioDeVeiculos.PegarPorPlaca(modelo.Placa);

                if (veiculo == null && !String.IsNullOrEmpty(modelo.Placa))
                {
                    veiculo = new Veiculo(modelo.Placa.ToUpper(), modelo.MarcaVeiculo.ToUpper(), modelo.ModeloVeiculo.ToUpper(), modelo.AnoVeiculo, modelo.CorVeiculo.ToUpper());
                    this._servicoExternoDePersistencia.Persistir();
                }

                if (!string.IsNullOrEmpty(modelo.Data) && !string.IsNullOrEmpty(modelo.Hora))
                {
                    var data = Convert.ToDateTime(modelo.Data);
                    var hora = Convert.ToDateTime(modelo.Hora);
                    dataHoraFato = new DateTime(data.Year, data.Month, data.Day, hora.Hour, hora.Minute, 0);
                }

                carater.AlterarDados(modelo.Descricao, modelo.ComplementoEndereco, dataHoraFato, cidade, crime, veiculo, modelo.SituacaoDoCarater, modelo.UrlImagem, usuarioBanco);
                this._servicoExternoDePersistencia.Persistir();

                return "Caráter alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Caráter: " + ex.InnerException);
            }
        }
    }
}
