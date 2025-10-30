// AppDelivery/DAL/ConfiguracaoCaixaDAO.cs

using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;

namespace AppDelivery.DAL
{
    public class ConfiguracaoCaixaDAO
    {
        private Conexao conexao = new Conexao();

        // (MÉTODOS SalvarConfiguracao, BuscarCaixaConfigurado, ListarConfiguracoes já estão aqui...)

        // NOVO MÉTODO: Para REMOVER a vinculação de uma máquina
        public void RemoverConfiguracao(int idConfig)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                // DELETE baseado no ID da configuração
                string sql = "DELETE FROM tb_config_caixa WHERE id_config = @IdConfig";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@IdConfig", idConfig);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para INSERIR ou ATUALIZAR a vinculação Máquina-Caixa
        // Exemplo do SalvarConfiguracao (deve estar no ConfiguracaoCaixaDAO.cs)
        public void SalvarConfiguracao(string nomeMaquina, int idCaixa)
        {
            // ...
            string sqlCheck = "SELECT id_config FROM tb_config_caixa WHERE nome_maquina = @NomeMaquina";
            // ...
            // Se idConfigExistente > 0, faz UPDATE
            // Se idConfigExistente == 0, faz INSERT
            // ...
        }

        // Método para buscar a configuração da máquina (para uso no sistema principal)
        public Caixa BuscarCaixaConfigurado(string nomeMaquina)
        {
            // ... [Código de Busca por Máquina permanece o mesmo] ...
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

        // Método para listar todas as configurações
        public DataTable ListarConfiguracoes()
        {
            DataTable dt = new DataTable();

            // Adicionado cfg.id_caixa para exibição na grid e carregamento na interface
            string sql = @"
                SELECT cfg.id_config, cfg.nome_maquina, c.nome_caixa, c.id_caixa
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