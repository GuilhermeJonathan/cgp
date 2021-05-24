using Cgp.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDePossuidor : EntityTypeConfiguration<Possuidor>
    {
        public MapeamentoDePossuidor()
        {
            this.ToTable("Possuidores");
        }
    }
}
