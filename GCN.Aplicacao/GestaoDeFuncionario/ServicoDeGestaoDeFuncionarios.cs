using GCN.Aplicacao.Comum;
using GCN.Aplicacao.GestaoDeFuncionario.Modelos;
using GCN.Dominio;
using GCN.Dominio.ObjetosDeValor;
using GCN.Infraestrutura.InterfaceDeServicosExternos;
using GCN.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Aplicacao.GestaoDeFuncionarios
{
    public class ServicoDeGestaoDeFuncionarios : IServicoDeGestaoDeFuncionarios
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;

        public ServicoDeGestaoDeFuncionarios(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
        }

        public void CadastrarNovoFuncionario(ModeloDeCadastroDeFuncionario modelo)
        {
            if (!this._servicoExternoDePersistencia.RepositorioDeFuncionarios.VerificaSeJaFuncionario(modelo.Nome))
            {
                var endereco = new Endereco(modelo.Pais, modelo.Uf, modelo.Cidade, modelo.Bairro, modelo.Cep, modelo.Logradouro, modelo.Numero, modelo.Complemento);

                var novoFuncionario = new Funcionario(modelo.Nome, modelo.Documento, modelo.Email, new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash),
                    modelo.Telefone, modelo.Celular, modelo.PerfilDeFuncionario, endereco);

                this._servicoExternoDePersistencia.RepositorioDeFuncionarios.Inserir(novoFuncionario);
                this._servicoExternoDePersistencia.Persistir();
            }
        }
    }
}
