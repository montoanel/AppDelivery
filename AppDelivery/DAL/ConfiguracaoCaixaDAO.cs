// AppDelivery/DAL/ConfiguracaoCaixaDAO.cs

using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;

namespace AppDelivery.DAL
{
    // Responsável por gerenciar a tabela tb_config_caixa
    public class ConfiguracaoCaixaDAO
    {
        private Conexao conexao = new Conexao();

        // Método para INSERIR ou ATUALIZAR a vinculação Máquina-Caixa
        public void SalvarConfiguracao(string nomeMaquina, int idCaixa)
        {
            // O nomeMaquina deve ser tratado como UNIQUE. 
            // O método verifica se existe e faz INSERT ou UPDATE.
            string sqlCheck = "SELECT id_config FROM tb_config_caixa WHERE nome_maquina = @NomeMaquina";
            int idConfigExistente = 0;

            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();

                // 1. Verifica se a máquina já está cadastrada
                using (SqlCommand cmd = new SqlCommand(sqlCheck, con))
                {
                    cmd.Parameters.AddWithValue("@NomeMaquina", nomeMaquina);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idConfigExistente = Convert.ToInt32(result);
                    }
                }

                string sql;
                if (idConfigExistente > 0)
                {
                    // 2. Se existe, ATUALIZA a vinculação (UPDATE)
                    sql = "UPDATE tb_config_caixa SET id_caixa = @IdCaixa WHERE nome_maquina = @NomeMaquina";
                }
                else
                {
                    // 3. Se não existe, INSERE a nova vinculação (INSERT)
                    sql = "INSERT INTO tb_config_caixa (nome_maquina, id_caixa) VALUES (@NomeMaquina, @IdCaixa)";
                }

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@NomeMaquina", nomeMaquina);
                    cmd.Parameters.AddWithValue("@IdCaixa", idCaixa);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para buscar a configuração da máquina (para uso no sistema principal)
        public Caixa BuscarCaixaConfigurado(string nomeMaquina)
        {
            Caixa caixa = null;

            string sql = @"
                SELECT c.id_caixa, c.nome_caixa, c.ativo
                FROM tb_config_caixa AS cfg
                INNER JOIN tb_caixas AS c ON cfg.id_caixa = c.id_caixa
                WHERE cfg.nome_maquina = @NomeMaquina";

            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@NomeMaquina", nomeMaquina);

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

        // Método para listar todas as configurações (útil para o Grid da tela de Configuração)
        public DataTable ListarConfiguracoes()
        {
            DataTable dt = new DataTable();

            string sql = @"
                SELECT cfg.id_config, cfg.nome_maquina, c.nome_caixa, c.ativo
                FROM tb_config_caixa AS cfg
                INNER JOIN tb_caixas AS c ON cfg.id_caixa = c.id_caixa
                ORDER BY cfg.nome_maquina";

            using (SqlConnection con = Conexao.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}