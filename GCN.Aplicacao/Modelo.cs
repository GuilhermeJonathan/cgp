using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao
{
    public abstract class Modelo<TEntidade> where TEntidade : class
    {
        protected Modelo()
        {
        }

        protected Modelo(TEntidade entidade)
        {
            if (entidade == null)
                return;

            this.PreencherModelo(entidade);
        }

        private void PreencherModelo(TEntidade entidade)
        {
            var propriedadesDoModelo = GetType().GetProperties();
            var propriedadesDaEntidade = entidade.GetType().GetProperties();

            foreach (var propriedade in propriedadesDoModelo)
            {
                var propriedadeEncontrada = propriedadesDaEntidade.FirstOrDefault(p => p.Name == propriedade.Name);

                if (propriedadeEncontrada == null)
                    continue;

                propriedade.SetValue(this, propriedadeEncontrada.GetValue(entidade));
            }
        }
    }
}
