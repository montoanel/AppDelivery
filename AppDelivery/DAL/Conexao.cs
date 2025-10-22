// AppDelivery/DAL/Conexao.cs
using Microsoft.Data.SqlClient;
using System.Configuration; // <--- Adicione este using

namespace AppDelivery.DAL
{
    public class Conexao
    {
        // Esta classe vai ler a string de conexão direto do seu App.config
        private static string ObterStringConexao()
        {
            // "MinhaConexaoDB" é o 'name' que você definiu no App.config
            string stringConexao = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            return stringConexao;
        }

        // Os outros arquivos (DAO) vão chamar este método para se conectar
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ObterStringConexao());
        }
    }
}