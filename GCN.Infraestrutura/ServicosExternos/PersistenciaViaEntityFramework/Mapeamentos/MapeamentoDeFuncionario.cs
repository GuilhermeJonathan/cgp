using Campeonato.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Mapeamentos
{
    public class MapeamentoDeFuncionario : EntityTypeConfiguration<Funcionario>
    {
        public MapeamentoDeFuncionario()
        {
            this.ToTable("Funcionarios");
            this.Property(f => f.Telefone.Numero).HasColumnName("Telefone");
            this.Property(f => f.Celular.Numero).HasColumnName("Celular");
            this.Property(f => f.Documento.Numero).HasColumnName("NumeroDoDocumento");
            this.Property(f => f.Endereco.Bairro).HasColumnName("Bairro");
            this.Property(f => f.Endereco.Cep).HasColumnName("Cep");
            this.Property(f => f.Endereco.Complemento).HasColumnName("Complemento");
            this.Property(f => f.Endereco.Cidade).HasColumnName("Cidade");
            this.Property(f => f.Endereco.Logradouro).HasColumnName("Logradouro");
            this.Property(f => f.Endereco.Numero).HasColumnName("Numero");
            this.Property(f => f.Endereco.Pais).HasColumnName("Pais");
            this.Property(f => f.Endereco.Uf).HasColumnName("Uf");
        }
    }
}
