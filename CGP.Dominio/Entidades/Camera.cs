using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Camera : Entidade
    {
        public Camera()
        {

        }

        public Camera(string ponto, string nome, string latitude, string longitude, Cidade cidade, Usuario usuario)
        {
            this.Ponto = ponto;
            this.Nome = nome;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Cidade = cidade;
            this.UsuarioQueAlterou = usuario;
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Ponto { get; set; }
        public Cidade Cidade { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Ativo { get; set; } = true;
        public Usuario UsuarioQueAlterou { get; set; }

        public void AlterarDados(string ponto, string nome, string latitude, string longitude, Cidade cidade, bool ativo, Usuario usuario)
        {
            this.Ponto = ponto;
            this.Nome = nome;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Cidade = cidade;
            this.Ativo = ativo;
            Atualizar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            this.UsuarioQueAlterou = usuario;
        }

        public void Ativar(Usuario usuario)
        {
            this.Ativo = true;
            Atualizar(usuario);
        }

        public void Inativar(Usuario usuario)
        {
            this.Ativo = false;
            Atualizar(usuario);
        }
    }
}
