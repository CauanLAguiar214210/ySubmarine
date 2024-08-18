using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using ConsoleApp1.Entities.Enum;

namespace ConsoleApp1.Entities
{
    internal class Funcionario
    {
        public string Nome { get; set; }
        public WorkerLevel WorkerLevel { get; set; }
        public double Salario { get; set; }
        public int DepartamentoId { get; set; }

        public Funcionario(string name, double salary, int departamento, WorkerLevel workerLevel)
        {
            Nome = name.ToUpper();
            Salario = salary;
            DepartamentoId = departamento;
            WorkerLevel = workerLevel;
        }

        public static void CadastrarFuncionarios(SqlConnection connection)
        {
            List<Funcionario> listFuncionarios = new List<Funcionario>();

            Console.WriteLine("Quantos funcionnarios serão cadastrados?");
            int n = int.Parse(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(i + "°Funcionario: ");

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Departamento: ");
                int depart = int.Parse(Console.ReadLine());

                Console.Write("Nivel: (0)Junior (1)Pleno (2)Senior \nSelecione:");
                int lvl = int.Parse(Console.ReadLine());
                WorkerLevel level = (WorkerLevel)lvl;

                Console.Write("Salario: ");
                double salario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                listFuncionarios.Add(new Funcionario(nome, salario, depart, level));

                foreach (var funcionario in listFuncionarios)
                {
                    funcionario.SalvarNoBanco(connection);
                }
            }
        }
        public void SalvarNoBanco(SqlConnection connection)
        {
            string query = "INSERT INTO Funcionario (Nome, Salario, DepartamentoId, WorkerLevel) VALUES (@Nome, @Salario, @DepartamentoId, @WorkerLevel)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", Nome);
                command.Parameters.AddWithValue("@Salario", Salario);
                command.Parameters.AddWithValue("@DepartamentoId", DepartamentoId);
                command.Parameters.AddWithValue("@WorkerLevel", WorkerLevel.ToString());

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Funcionário salvo com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar o funcionário: " + ex.Message);
                }
            }

        }
        public static void ListarBanco(SqlConnection connection)
        {
            string query = "SELECT * FROM vwFuncionariosDepartamentos";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    Console.WriteLine("\nTABELA FUNCIONARIO:");
                    var dataset = new DataSet();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataset);
                    var rows = dataset.Tables[0].Rows;
                    foreach (DataRow item in rows)
                    {                        
                        var colunas = item.ItemArray;                        
                        Console.WriteLine($"Nome: {colunas[0]}, WorkerLevel: {colunas[1]}, Salario: {colunas[2]}, Departamento: {colunas[3]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao Listar Tabela Funcionario: " + ex.Message);
                }
            }
        }

    }
}
