using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeAposta : EntityTypeConfiguration<Aposta>
    {
        public MapeamentoDeAposta()
        {
            this.ToTable("Apostas");
        }
    }
}
