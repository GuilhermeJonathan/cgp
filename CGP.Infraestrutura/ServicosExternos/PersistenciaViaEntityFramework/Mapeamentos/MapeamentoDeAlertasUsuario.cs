using Cgp.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeAlertasUsuario : EntityTypeConfiguration<AlertaUsuario>
    {
        public MapeamentoDeAlertasUsuario()
        {
            this.ToTable("AlertasUsuario");
        }
    }
}
