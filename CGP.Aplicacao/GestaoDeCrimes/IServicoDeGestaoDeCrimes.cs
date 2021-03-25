using Cgp.Aplicacao.GestaoDeCrimes.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes
{
    public interface IServicoDeGestaoDeCrimes
    {
        ModeloDeListaDeCrimes RetonarCrimesPorFiltro(ModeloDeFiltroDeCrime filtro, int pagina, int registrosPorPagina = 30);
        ModeloDeEdicaoDeCrime BuscarCrimePorId(int id);
        IList<Crime> RetonarTodosOsCrimesAtivos();
        string CadastrarCrime(ModeloDeCadastroDeCrime modelo, UsuarioLogado usuario);
        string AlterarDadosDoCrime(ModeloDeEdicaoDeCrime modelo, UsuarioLogado usuario);
        string AtivarCrime(int id, UsuarioLogado usuario);
    }
}
