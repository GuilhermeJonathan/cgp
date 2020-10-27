using GCN.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeUsuario : EntityTypeConfiguration<Usuario>
    {
        public MapeamentoDeUsuario()
        {
            this.ToTable("Usuarios");
            this.Property(u => u.Nome.Valor).HasColumnName("Nome").HasColumnType("varchar").HasMaxLength(128);
            this.Property(u => u.Login.Valor).HasColumnName("Login").HasColumnType("varchar").HasMaxLength(256);
            this.Property(u => u.Senha.Valor).HasColumnName("Senha").HasColumnType("varchar").HasMaxLength(128);
        }
    }
}
