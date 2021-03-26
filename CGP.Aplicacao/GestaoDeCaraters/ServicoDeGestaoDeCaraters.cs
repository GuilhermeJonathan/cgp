using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Aplicacao.Util;
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
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorFiltro(filtro.Cidade, filtro.Crime, filtro.SituacaoDoCarater, out quantidadeEncontrada);

                var modelo = new ModeloDeListaDeCaraters(caraters, quantidadeEncontrada, filtro);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }

        public ModeloDeListaDeCaraters BuscarCaraterPorPlaca(string placa)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.BuscarCaratersPorPlaca(placa);

                var modelo = new ModeloDeListaDeCaraters(caraters, quantidadeEncontrada, new ModeloDeFiltroDeCarater());
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }

        public ModeloDeListaDeCaraters RetonarCaratersPorCidades(ModeloDeFiltroDeCarater filtro)
        {
            try
            {
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorCidades(filtro.CidadesSelecionadas);
                return new ModeloDeListaDeCaraters(caraters, 0, filtro);
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
                    veiculo = new Veiculo(modelo.Placa, modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo);
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
                    veiculo = new Veiculo(modelo.Placa, modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo);
                    this._servicoExternoDePersistencia.Persistir();
                } else
                {
                    veiculo.AlterarDadosVeiculo(modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo);
                }

                if (!string.IsNullOrEmpty(modelo.Data) && !string.IsNullOrEmpty(modelo.Hora))
                {
                    var data = Convert.ToDateTime(modelo.Data);
                    var hora = Convert.ToDateTime(modelo.Hora);
                    dataHoraFato = new DateTime(data.Year, data.Month, data.Day, hora.Hour, hora.Minute, 0);
                }

                carater.AlterarDados(modelo.Descricao, modelo.ComplementoEndereco, dataHoraFato, cidade, crime, veiculo,  modelo.UrlImagem, usuarioBanco);
                this._servicoExternoDePersistencia.Persistir();

                return "Caráter alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Caráter: " + ex.InnerException);
            }
        }

        public string RealizarBaixaVeiculo(int id, string descricao, int cidade, UsuarioLogado usuario)
        {
            try
            {
                var carater = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var cidadeBanco = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(cidade);
                
                carater.RealizarBaixaVeiculo(descricao, cidadeBanco, usuarioBanco);
                this._servicoExternoDePersistencia.Persistir();

                return "Baixa realizada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível realizar baixa: " + ex.InnerException);
            }
        }

        public string RetornaHtmlDaLista(List<ModeloDeCaratersDaLista> caraters, UsuarioLogado usuario)
        {
            StringBuilder html = new StringBuilder();
            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");

            html.Append($"<html><head> <meta charset='UTF-8'></head>");
            html.Append("<style> .table{width: 100%; margin-bottom: 1rem; color: #212529; background-color: transparent;}  .table th, .table td { padding: 0.75rem; vertical-align: top; border-top: 1px solid #dee2e6;}  .table thead th { vertical-align: bottom; border-bottom: 2px solid #dee2e6;}  .table tbody + tbody { border-top: 2px solid #dee2e6; }  .text-muted { color: #6c757d !important;} .placar { font-size: 16pt; font-weight: bold; } .verde {color: #139f11 !important; font-weight: bold; } .amarelo {color: #ff6a00 !important;font-weight: bold;}");
            html.Append($"</style>");
            html.Append($"<body>");
            html.Append($"<table class='table'>");
            html.Append($"<tr><th colspan='4'>RELAÇÃO DE CARÁTER GERAL</th></tr>");
            html.Append($"<tr><th>PLACA</th><th>MODELO</th><th>COR</th><th>ANO</th><th>CIDADE</th><th>CRIME</th></tr>");

            foreach (var carater in caraters)
            {   
                html.Append($"<tr>");

                html.Append($"<td><span class='text-muted'>{carater.PlacaVeiculo}</span></td>");
                html.Append($"<td><span class='text-muted'>{carater.NomeVeiculo}</span></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.CorVeiculo}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.AnoVeiculo}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.NomeCidade}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.NomeCrime}</span></p></td>");
                html.Append($"</tr>");

                //html.Append($"<td><p class='d-flex flex-column text-center'><span style='font-weight: bold;'> {jogo.DataDoJogo}</span><span> {jogo.HoraDoJogo}</span></p>");
            }

            html.Append($"</table></body>");
            html.Append($"<footer class='main-footer'> Emitido pelo {usuario.Nome} em {DateTime.Now.ToShortDateString()}<br> <b>Bolão</b>Brasileirão 2020 - All rights reserved.</footer>");

            html.Append($"</html>");
            return html.ToString();
        }
    }
}
