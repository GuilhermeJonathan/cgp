using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeVeiculos
{
    public class ServicoDeGestaoDeVeiculos: IServicoDeGestaoDeVeiculos
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeVeiculos(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public string CadastrarProprietarioPossuidor(ModeloDeBuscaDaLista modelo, UsuarioLogado usuario)
        {
            try
            {
                var proprietario = modelo.Proprietario;
                var possuidor = modelo.Possuidor;
                var veiculo = this._servicoExternoDePersistencia.RepositorioDeVeiculos.PegarPorPlaca(modelo.Placa);
                
                if (veiculo == null)
                {
                    var marca = modelo.MarcaModelo.Split('/')[0];
                    var modeloCaro = modelo.MarcaModelo.Split('/')[1];
                    veiculo = new Veiculo(modelo.Placa, marca, modeloCaro, modelo.AnoModelo, modelo.Cor, modelo.Chassi, modelo.Uf);
                    this._servicoExternoDePersistencia.RepositorioDeVeiculos.Inserir(veiculo);
                    this._servicoExternoDePersistencia.Persistir();
                } 

                if(veiculo.Proprietario == null)
                    CadastrarProprietario(veiculo, proprietario);

                if (veiculo.Possuidor == null)
                    CadastrarPossuidor(veiculo, possuidor);

                this._servicoExternoDePersistencia.Persistir();
                return "Proprietário cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível cadastrar o proprietário: " + ex.InnerException);
            }
        }

        private void CadastrarProprietario(Veiculo veiculo, ModeloDeProprietarioDaLista proprietario)
        {
            if (proprietario != null)
            {
                var proprietarioBanco = this._servicoExternoDePersistencia.RepositorioDeProprietarios.PegarPorDocumento(proprietario.Documento);
                if (proprietarioBanco == null)
                    veiculo.CadastrarProprietario(new Proprietario(proprietario.Nome, proprietario.Documento, proprietario.Endereco, proprietario.TipoDocumento));
                else
                    veiculo.CadastrarProprietario(proprietarioBanco);
            }
        }

        private void CadastrarPossuidor(Veiculo veiculo, ModeloDePossuidorDaLista possuidor)
        {
            if (possuidor != null)
            {
                var possuidorBanco = this._servicoExternoDePersistencia.RepositorioDePossuidores.PegarPorDocumento(possuidor.Documento);
                if (possuidorBanco == null)
                    veiculo.CadastrarPossuidor(new Possuidor(possuidor.Nome, possuidor.Documento, possuidor.Endereco, possuidor.TipoDocumento));
                else
                    veiculo.CadastrarPossuidor(possuidorBanco);
            }
        }
    }
}
