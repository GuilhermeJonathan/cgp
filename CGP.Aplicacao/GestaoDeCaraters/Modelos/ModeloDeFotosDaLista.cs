using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeFotosDaLista
    {
        public ModeloDeFotosDaLista()
        {

        }

        public ModeloDeFotosDaLista(Foto foto)
        {
            if (foto == null)
                return;

            this.Id = foto.Id;
            this.Descricao = foto.Descricao;
            var caminhoBlob = $"{VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob")}fotos/{foto.Caminho}";
            this.Caminho = caminhoBlob;
            this.IdCarater = foto.Carater != null ? foto.Carater.Id : 0;
            this.Ativo = foto.Ativo;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public int IdCarater { get; set; }
        public bool Ativo { get; set; }
    }
}
