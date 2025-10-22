// AppDelivery/DAL/CaixaDAO.cs
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AppDelivery.DAL
{
    // DAO = Data Access Object (Objeto de Acesso a Dados)
    public class CaixaDAO
    {
        // Método para LISTAR todos os caixas do banco
        public List<Caixa> ListarTodos()
        {
            List<Caixa> lista = new List<Caixa>();

            // Usar a classe Conexao que criamos
            using (SqlConnection con = Conexao.GetConnection())
            {
                // Comando SQL
                string sql = "SELECT Id, Nome, Ativo FROM tb_caixas ORDER BY Nome";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê linha por linha do resultado
                        while (reader.Read())
                        {
                            // Transforma a linha do banco em um objeto Caixa
                            Caixa caixa = new Caixa
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Ativo = reader.GetBoolean(2)
                            };
                            lista.Add(caixa); // Adiciona o objeto à lista
                        }
                    }
                }
            }
            return lista; // Retorna a lista preenchida
        }

        // Método para INSERIR um novo caixa no banco
        public void Inserir(Caixa caixa)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                // O @Nome e @Ativo são parâmetros para evitar SQL Injection
                string sql = "INSERT INTO tb_caixas (Nome, Ativo) VALUES (@Nome, @Ativo)";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Define o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@Nome", caixa.Nome);
                    cmd.Parameters.AddWithValue("@Ativo", caixa.Ativo);

                    // Executa o comando (sem retorno de dados)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para ATUALIZAR um caixa existente
        public void Atualizar(Caixa caixa)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                string sql = "UPDATE tb_caixas SET Nome = @Nome, Ativo = @Ativo WHERE Id = @Id";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", caixa.Nome);
                    cmd.Parameters.AddWithValue("@Ativo", caixa.Ativo);
                    cmd.Parameters.AddWithValue("@Id", caixa.Id); // O Id é usado no WHERE

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}