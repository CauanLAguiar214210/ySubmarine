using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp1.Entities
{
    internal class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Departamento(string nome)
        {
            Nome = nome.ToUpper();
        }

        public static void CadastrarDepartamentos(SqlConnection connection)
        {
                List<Departamento> listDepartamentos = new List<Departamento>();

                Console.WriteLine("Quantos Departamentos serão cadastrados?");
                int n = int.Parse(Console.ReadLine());

                for (int i = 1; i <= n; i++)
                {
                    Console.WriteLine(i + " °Departamento: ");

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    listDepartamentos.Add(new Departamento(nome));
                }
                foreach (var departamentos in listDepartamentos)
                {
                    departamentos.SalvarNoBanco(connection);
                }
        }
        public void SalvarNoBanco(SqlConnection connection)
        {
            string query = "INSERT INTO Departamento (Nome) VALUES (@Nome)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", Nome);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Departament " + Nome + " salvo com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar o Departamento: " + ex.Message);
                }
            }
        }
        public static void ListarBanco(SqlConnection connection)
        {
            string query = "SELECT * FROM vwDepartamentos";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    Console.WriteLine("\nTABELA DEPARTAMENTO:");
                    var dataset = new DataSet();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataset);
                    var rows = dataset.Tables[0].Rows;

                    foreach (DataRow item in rows)
                    {
                        var colunas = item.ItemArray;
                        Console.WriteLine($"Nome: {colunas[0]}, Numero De Funcionarios: {colunas[1]}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao Listar Tabela Departamento: " + ex.Message);
                }
            }
        }

    }
}
