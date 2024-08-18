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
        public string Departamento { get; set; }
        public int DepartamentoId { get; set; }

        public Dictionary<string, int> departamentoIds = new Dictionary<string, int>
        {
        { "VENDAS", 19 },
        { "QUALIDADE", 20 },
        { "QA", 20 },
        { "LIMPEZA", 22 },
        { "RH", 23 },
        { "SEGURANÇA", 24 },
        { "TRANSPORTE", 25 },
        { "ADMINISTRAÇÃO", 26 },
        { "FINANCEIRO", 27 },
        { "CONTÁBIL", 28 },
        { "PRODUÇÃO", 29 },
        { "TECNOLOGIA DA INFORMAÇÃO", 30 },
        { "TI", 30 },
        { "JURÍDICO", 31 },
        { "PESQUISA", 32 },
        { "SUPRIMENTOS", 33 },
        { "SERVICE DESK", 34 }
        };

        public Funcionario(string name, double salary, string departamento, WorkerLevel workerLevel)
        {
            Nome = name.ToUpper();
            Salario = salary;
            Departamento = departamento.ToUpper();
            WorkerLevel = workerLevel;

            if (departamentoIds.TryGetValue(Departamento, out int deptId))
            {
                DepartamentoId = deptId;
            }
            else
            {
                throw new Exception("Departamento inválido: " + departamento);
            }
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
                string depart = Console.ReadLine();

                Console.Write("Nivel: (0)Junior (1)Pleno (2)Senior \nSelecione:");
                int lvl = int.Parse(Console.ReadLine());
                WorkerLevel level = (WorkerLevel)lvl;

                Console.Write("Salario: ");
                double salario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                listFuncionarios.Add(new Funcionario(nome, salario, depart, level));
                
            }
            foreach (var funcionario in listFuncionarios)
            {
                funcionario.SalvarNoBanco(connection);
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
            string query = "SELECT * FROM vwFuncionariosDepartamentos order by DepartamentoNome";
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
