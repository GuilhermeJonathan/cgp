using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeCarro : EntityTypeConfiguration<Carro>
    {
        public MapeamentoDeCarro()
        {
            this.ToTable("Carros");
        }
    }
}
