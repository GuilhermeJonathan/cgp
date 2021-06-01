using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeAcessosDeUsuario : EntityTypeConfiguration<AcessoUsuario>
    {
        public MapeamentoDeAcessosDeUsuario()
        {
            this.ToTable("AcessosUsuario");
        }
    }
}
