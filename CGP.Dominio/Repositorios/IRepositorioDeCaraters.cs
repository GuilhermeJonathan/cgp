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
        IList<Carater> RetornarCaratersPorFiltro(string placa, int[] cidades, int[] crimes, int situacao, DateTime? dataTarefaInicial, DateTime? dataTarefaFinal, out int quantidadeEncontrada);
        IList<Carater> RetornarCaratersPorCidades(int[] cidades, DateTime dataParaBusca);
        Carater PegarPorId(int id);
        IList<Carater> BuscarCaratersPorFragmentos(string fragmento);
        Carater PegarCaraterPorPlaca(string placa);
        Foto PegarFotoPorId(int id);
    }
}