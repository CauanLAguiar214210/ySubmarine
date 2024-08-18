using ConsoleApp1.Entities;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionManager.TestarConexao();
            var connection = ConnectionManager.GetSqlConnection();
            connection.Open();

            string Acao;
            do
            {
                Console.WriteLine("\nMenu: \n(CF)Cadastrar Funcionarios | (LF)Listar Funcionarios \n(CD)Cadastrar Derpartamentos | (LD)Listar Derpartamentos \n\n(F)Finalizar e Sair");

                Acao = Console.ReadLine().ToUpper();

                switch (Acao)
                {
                    case "CF":
                        Funcionario.CadastrarFuncionarios(connection);
                        break;
                    case "LF":
                        Funcionario.ListarBanco(connection);
                        break;
                    case "CD":
                        Departamento.CadastrarDepartamentos(connection);
                        break;
                    case "LD":
                        Departamento.ListarBanco(connection);
                        break;
                }
            }while (Acao != "F");
            connection.Close();
        }
    }
}

