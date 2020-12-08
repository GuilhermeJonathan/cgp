using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDePremiacao : Repositorio<Premiacao>, IRepositorioDePremiacoes
    {
        public RepositorioDePremiacao(Contexto contexto) : base(contexto) { }
    }
}
