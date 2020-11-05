using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios
{
    public interface IServicoDeGestaoDeUsuarios
    {
        string CadastrarNovoUsuario(ModeloDeCadastroDeUsuario modelo);
        IList<ModeloDeUsuarioDaLista> RetonarTodosOsUsuariosAtivos();
    }
}
