using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeCaraters : IRepositorio<Carater>
    {
        IList<Carater> RetornarCaratersPorFiltro(int cidade, int crime, int situacao, DateTime? dataTarefaInicial, DateTime? dataTarefaFinal, out int quantidadeEncontrada);
        IList<Carater> RetornarCaratersPorCidades(int[] cidades, DateTime dataParaBusca);
        Carater PegarPorId(int id);
        IList<Carater> BuscarCaratersPorPlaca(string placa);
        Carater PegarCaraterPorPlaca(string placa);
    }
}