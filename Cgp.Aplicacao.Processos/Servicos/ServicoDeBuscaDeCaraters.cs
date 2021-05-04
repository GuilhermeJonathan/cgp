using Cgp.Aplicacao.Processos.InterfaceDeServicos;
using Cgp.Dominio.Entidades;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Processos.Servicos
{
    public class ServicoDeBuscaDeCaraters : IServicoDeBuscaDeCaraters
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoDePersistencia;
        private Contexto contexto = new Contexto();
        public ServicoDeBuscaDeCaraters(SqlConnection conexao)
        {
            this._servicoDePersistencia = new ServicoExternoDePersistenciaViaEntityFramework(contexto);
        }

        public async Task<List<Carater>> Executar()
        {
            var caraters = this._servicoDePersistencia.RepositorioDeCaraters.RetornarTodosCaraters();

            Console.WriteLine($"Lista de Caráters:{caraters.Count}");

            foreach (var item in caraters)
            {
                Console.WriteLine($"Caráter: {item.Veiculo.Placa}");
            }

            Console.Read();
            return caraters.ToList();
        }
    }
}
