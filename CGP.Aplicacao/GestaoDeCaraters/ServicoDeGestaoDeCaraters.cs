using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cgp.Aplicacao.GestaoDeCaraters
{
    public class ServicoDeGestaoDeCaraters : IServicoDeGestaoDeCaraters
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;
        private readonly IServicoExternoDeArmazenamentoEmNuvem _servicoExternoDeArmazenamentoEmNuvem;

        public ServicoDeGestaoDeCaraters(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha, IServicoExternoDeArmazenamentoEmNuvem servicoExternoDeArmazenamentoEmNuvem)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
            this._servicoExternoDeArmazenamentoEmNuvem = servicoExternoDeArmazenamentoEmNuvem;
        }

        public ModeloDeListaDeCaraters RetonarCaratersPorFiltro(ModeloDeFiltroDeCarater filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                DateTime? dataInicial = null;
                DateTime? dataFinal = null;

                if (!String.IsNullOrEmpty(filtro.DataInicio))
                {
                    dataInicial = DateTime.Parse(filtro.DataInicio);
                    dataInicial = new DateTime(dataInicial.Value.Year, dataInicial.Value.Month, dataInicial.Value.Day, 0, 0, 0);
                }

                if (!String.IsNullOrEmpty(filtro.DataFim))
                {
                    dataFinal = DateTime.Parse(filtro.DataFim);
                    dataFinal = new DateTime(dataFinal.Value.Year, dataFinal.Value.Month, dataFinal.Value.Day, 23, 59, 59);
                }

                var quantidadeEncontrada = 0;
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorFiltro(filtro.Placa, filtro.CidadesSelecionadas, filtro.CrimesSelecionados, filtro.SituacaoDoCarater, dataInicial, dataFinal, out quantidadeEncontrada);

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
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.BuscarCaratersPorFragmentos(placa);

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
                DateTime dataParaBusca = DateTime.Now.AddDays(-3);
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorCidades(filtro.CidadesSelecionadas, dataParaBusca);
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
            var mensagemErro = String.Empty;
            try
            {
                var caraterCadastrado = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarCaraterPorPlaca(modelo.Placa);
                if (caraterCadastrado != null)
                {
                    mensagemErro = "Já existe um caráter para o mesmo veículo.";
                    throw new ExcecaoDeAplicacao("Já existe um caráter para o mesmo veículo.");
                }

                DateTime dataHoraFato = new DateTime();
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var crime = this._servicoExternoDePersistencia.RepositorioDeCrimes.BuscarPorId(modelo.Crime);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);
                var veiculo = this._servicoExternoDePersistencia.RepositorioDeVeiculos.PegarPorPlaca(modelo.Placa);

                if (veiculo == null)
                {
                    veiculo = new Veiculo(modelo.Placa, modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo, modelo.UfVeiculo);
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

                novoCarater.AdicionarHistorico(new HistoricoDeCarater("Criou o Caráter", "", TipoDeHistoricoDeCarater.Criacao, usuarioBanco, novoCarater.Id));
                this._servicoExternoDePersistencia.Persistir();

                return "Caráter incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o Caráter: " + ex.InnerException + mensagemErro);
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
                    veiculo = new Veiculo(modelo.Placa, modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo, modelo.UfVeiculo);
                    this._servicoExternoDePersistencia.Persistir();
                } else
                {
                    veiculo.AlterarDadosVeiculo(modelo.MarcaVeiculo, modelo.ModeloVeiculo, modelo.AnoVeiculo, modelo.CorVeiculo, modelo.ChassiVeiculo, modelo.UfVeiculo);
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
                var descricaHistorico = $"{descricao} <br>Cidade: {cidadeBanco.Descricao}.";

                carater.AdicionarHistorico(new HistoricoDeCarater("Realizou baixa do Caráter", descricaHistorico, TipoDeHistoricoDeCarater.Baixa, usuarioBanco, carater.Id));

                this._servicoExternoDePersistencia.Persistir();

                return "Baixa realizada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível realizar baixa: " + ex.InnerException);
            }
        }

        public ModeloDeListaDeCaraters GerarPDFeRetornar(ModeloDeFiltroDeCarater filtro, UsuarioLogado usuario)
        {
            try
            {
                DateTime? dataInicial = null;
                DateTime? dataFinal = null;

                if (!String.IsNullOrEmpty(filtro.DataInicio))
                {
                    dataInicial = DateTime.Parse(filtro.DataInicio);
                    dataInicial = new DateTime(dataInicial.Value.Year, dataInicial.Value.Month, dataInicial.Value.Day, 0, 0, 0);
                }

                if (!String.IsNullOrEmpty(filtro.DataFim))
                {
                    dataFinal = DateTime.Parse(filtro.DataFim);
                    dataFinal = new DateTime(dataFinal.Value.Year, dataFinal.Value.Month, dataFinal.Value.Day, 23, 59, 59);
                }

                var quantidadeEncontrada = 0;
                var caraters = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarCaratersPorFiltro(filtro.Placa, filtro.CidadesSelecionadas, filtro.CrimesSelecionados, filtro.SituacaoDoCarater, dataInicial, dataFinal, out quantidadeEncontrada);

                var modelo = new ModeloDeListaDeCaraters(caraters, quantidadeEncontrada, filtro);
                modelo.ArquivoHtml = RetornaHtmlDaLista(modelo.Lista.ToList(), dataInicial != null? dataInicial.Value : DateTime.MinValue, dataFinal != null ? dataFinal.Value : DateTime.MinValue, usuario);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }

        private string RetornaHtmlDaLista(List<ModeloDeCaratersDaLista> caraters, DateTime? dataInicial, DateTime? dataFinal, UsuarioLogado usuario)
        {
            StringBuilder html = new StringBuilder();
            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");

            html.Append($"<html><head> <meta charset='UTF-8'></head>");
            html.Append("<style> .table{width: 100%; margin-bottom: 1rem; color: #212529; background-color: transparent;}  .table th, .table td { padding: 0.75rem; vertical-align: top; border-top: 1px solid #dee2e6;}  .table thead th { vertical-align: bottom; border-bottom: 2px solid #dee2e6;}  .table tbody + tbody { border-top: 2px solid #dee2e6; }  .text-muted { color: #6c757d !important;} .placa { font-size: 16pt; font-weight: bold; } .verde {color: #139f11 !important; font-weight: bold; } .amarelo {color: #ff6a00 !important;font-weight: bold;}");
            html.Append($"</style>");
            html.Append($"<body>");
            html.Append($"<table class='table'>");
            html.Append($"<tr><th colspan='7'>RELAÇÃO DE CARÁTER GERAL");
            if((dataInicial.HasValue && dataInicial != DateTime.MinValue)  && (dataFinal.HasValue && dataFinal != DateTime.MinValue)) html.Append($" - {dataInicial.Value.ToString("dd/MM")} a {dataFinal.Value.ToString("dd/MM/yyyy")}");
            html.Append($"</th></tr>");
            html.Append($"<tr><th width='130px'>PLACA</th><th>MODELO</th><th>COR</th><th>ANO</th><th>CIDADE</th><th>CRIME</th><th>DATA</th></tr>");

            foreach (var carater in caraters)
            {   
                html.Append($"<tr>");

                html.Append($"<td><span class='placa'>{carater.PlacaInvertida.ToUpper()}</span></td>");
                html.Append($"<td><span class='text-muted'>{carater.NomeVeiculo}</span></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.CorVeiculo}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.AnoVeiculo}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.NomeCidade}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.NomeCrime}</span></p></td>");
                html.Append($"<td><p style='text-align:center;'><span>{carater.DataDoFato}</span></p></td>");
                html.Append($"</tr>");

            }

            html.Append($"</table></body>");
            html.Append($"<footer class='main-footer'> Emitido por {usuario.Nome} em {DateTime.Now.ToShortDateString()}<br> <b>Caráter Geral</b> Policial 2021(CGP) - All rights reserved.</footer>");

            html.Append($"</html>");
            return html.ToString();
        }

        public bool VerificaCadastroDeCarater(string placa)
        {
            var caraterCadastrado = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarCaraterPorPlaca(placa);
            if (caraterCadastrado != null)
                return true;
            else
                return false;
        }

        public async Task<string> AdicionarFotos(int id, HttpFileCollectionBase files, UsuarioLogado usuario)
        {
            try
            {
                var carater = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (carater != null)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase imagem = files[i];
                        var caminho = _servicoDeGeracaoDeHashSha.GerarParaStream(imagem.InputStream) + ".jpg";
                        var caminhoBlob = $"fotos";

                        if (carater.VerificarSeJaTemEssaFoto(caminho))
                            throw new ExcecaoDeAplicacao("Foto já cadastrada");

                        var descricaoFoto = imagem.FileName.Split('.');
                        var foto = new Foto(carater, descricaoFoto[0], caminho);
                        carater.Fotos.Add(foto);
                        this._servicoExternoDePersistencia.Persistir();

                        carater.AdicionarHistorico(new HistoricoDeCarater("Adicionou uma foto ao caráter", descricaoFoto[0], TipoDeHistoricoDeCarater.Foto, usuarioBanco, foto.Id));

                        imagem.InputStream.Position = 0;
                        await this._servicoExternoDeArmazenamentoEmNuvem.EnviarArquivoAsync(imagem.InputStream, caminhoBlob, caminho);
                        this._servicoExternoDePersistencia.Persistir();
                    }

                    this._servicoExternoDePersistencia.Persistir();
                }

                return "Fotos adicionadas com sucesso";
            }

            catch (ExcecaoDeAplicacao ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }

            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public string ExcluirFoto(int id, UsuarioLogado usuario)
        {
            try
            {
                var foto = this._servicoExternoDePersistencia.RepositorioDeCaraters.PegarFotoPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (foto == null)
                    throw new ExcecaoDeAplicacao("Não foi encontrada a foto para a exclusão");

                foto.InativarFoto();
                foto.Carater.AdicionarHistorico(new HistoricoDeCarater("Excluir uma foto do caráter", foto.Descricao, TipoDeHistoricoDeCarater.Foto, usuarioBanco, foto.Id));

                this._servicoExternoDePersistencia.Persistir();
                return "Foto excluída com sucesso.";
            }

            catch (ExcecaoDeAplicacao ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }

            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }
    }
}
