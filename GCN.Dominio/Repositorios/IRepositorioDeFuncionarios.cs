namespace GCN.Dominio.Repositorios
{
    public interface IRepositorioDeFuncionarios : IRepositorio<Funcionario>
    {
        bool VerificaSeJaFuncionario(string nome);
    }
}
