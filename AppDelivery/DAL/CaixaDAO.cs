// Arquivo: AppDelivery/DAL/CaixaDAO.cs
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;

namespace AppDelivery.DAL
{
    public class CaixaDAO
    {
        // Assumindo que a classe Conexao existe e o método GetConnection() está correto
        private Conexao conexao = new Conexao();

        // Método para LISTAR todos os caixas
        public List<Caixa> ListarTodos()
        {
            List<Caixa> lista = new List<Caixa>();

            // Usar a classe Conexao
            using (SqlConnection con = Conexao.GetConnection())
            {
                // Queries ajustadas para as colunas id_caixa, nome_caixa, ativo
                string sql = "SELECT id_caixa, nome_caixa, ativo FROM tb_caixas ORDER BY nome_caixa";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Caixa caixa = new Caixa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_caixa")),
                                Nome = reader.GetString(reader.GetOrdinal("nome_caixa")),
                                // Leitura do CHAR(1)
                                Ativo = reader.GetString(reader.GetOrdinal("ativo"))[0]
                            };
                            lista.Add(caixa);
                        }
                    }
                }
            }
            return lista;
        }

        // Método para INSERIR um novo caixa
        public void Inserir(Caixa caixa)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                string sql = "INSERT INTO tb_caixas (nome_caixa, ativo) VALUES (@Nome, @Ativo)";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", caixa.Nome);
                    cmd.Parameters.AddWithValue("@Ativo", caixa.Ativo); // Envia 'A' ou 'I'

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para ATUALIZAR um caixa existente (Edição e Inativação)
        public void Atualizar(Caixa caixa)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                string sql = "UPDATE tb_caixas SET nome_caixa = @Nome, ativo = @Ativo WHERE id_caixa = @Id";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", caixa.Nome);
                    cmd.Parameters.AddWithValue("@Ativo", caixa.Ativo);
                    cmd.Parameters.AddWithValue("@Id", caixa.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para buscar um caixa pelo ID 
        public Caixa BuscarPorId(int idCaixa)
        {
            Caixa caixa = null;

            using (SqlConnection con = Conexao.GetConnection())
            {
                string sql = "SELECT id_caixa, nome_caixa, ativo FROM tb_caixas WHERE id_caixa = @Id";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idCaixa);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caixa = new Caixa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_caixa")),
                                Nome = reader.GetString(reader.GetOrdinal("nome_caixa")),
                                Ativo = reader.GetString(reader.GetOrdinal("ativo"))[0]
                            };
                        }
                    }
                }
            }
            return caixa;
        }
    }
}