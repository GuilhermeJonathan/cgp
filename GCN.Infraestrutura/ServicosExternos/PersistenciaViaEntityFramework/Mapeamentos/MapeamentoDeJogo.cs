using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeJogo : EntityTypeConfiguration<Jogo>
    {
        public MapeamentoDeJogo()
        {
            this.ToTable("Jogos");
        }
    }
}
