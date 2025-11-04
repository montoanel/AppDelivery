// Arquivo: AppDelivery/DAL/CaixaSessaoDAO.cs
using Microsoft.Data.SqlClient;
using System;

namespace AppDelivery.DAL
{
    public class CaixaSessaoDAO
    {
        private const int ID_FORMA_PAGAMENTO_DINHEIRO = 1;

        public void AbrirSessao(CaixaSessao sessao)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // --- PASSO 1: Inserir na tb_caixa_sessoes --- (NOME CORRIGIDO)
                    string sqlSessao = @"
                        INSERT INTO tb_caixa_sessoes 
                            (id_caixa, id_atendente_abertura, data_abertura, valor_abertura, status_sessao)
                        VALUES 
                            (@IdCaixa, @IdAtendente, @DataAbertura, @ValorAbertura, @Status);
                        
                        SELECT SCOPE_IDENTITY();";

                    int novoSessaoId;
                    using (SqlCommand cmdSessao = new SqlCommand(sqlSessao, con, transaction))
                    {
                        cmdSessao.Parameters.AddWithValue("@IdCaixa", sessao.IdCaixa);
                        cmdSessao.Parameters.AddWithValue("@IdAtendente", sessao.IdAtendenteAbertura);
                        cmdSessao.Parameters.AddWithValue("@DataAbertura", sessao.DataAbertura);
                        cmdSessao.Parameters.AddWithValue("@ValorAbertura", sessao.ValorAbertura);
                        cmdSessao.Parameters.AddWithValue("@Status", sessao.StatusSessao);

                        novoSessaoId = Convert.ToInt32(cmdSessao.ExecuteScalar());
                    }

                    if (sessao.ValorAbertura > 0)
                    {
                        // --- PASSO 2: Inserir na tb_caixa_movimentacao ---
                        // (Assumindo que esta tabela está com o nome certo)
                        string sqlMov = @"
                            INSERT INTO tb_caixa_movimentacao
                                (id_sessao, id_forma_pagamento, tipo_mov, valor, data_mov, descricao, id_atendente)
                            VALUES
                                (@IdSessao, @IdFormaPag, 'E', @Valor, @DataMov, 'VALOR DE ABERTURA (SUPRIMENTO)', @IdAtendente)";

                        using (SqlCommand cmdMov = new SqlCommand(sqlMov, con, transaction))
                        {
                            cmdMov.Parameters.AddWithValue("@IdSessao", novoSessaoId);
                            cmdMov.Parameters.AddWithValue("@IdFormaPag", ID_FORMA_PAGAMENTO_DINHEIRO);
                            cmdMov.Parameters.AddWithValue("@Valor", sessao.ValorAbertura);
                            cmdMov.Parameters.AddWithValue("@DataMov", sessao.DataAbertura);
                            cmdMov.Parameters.AddWithValue("@IdAtendente", sessao.IdAtendenteAbertura);

                            cmdMov.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao tentar abrir o caixa (Transação revertida): " + ex.Message);
                }
            }
        }

        public CaixaSessao VerificarSessaoAberta(int idCaixa)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                // --- Busca na tb_caixa_sessoes --- (NOME CORRIGIDO)
                string sql = @"
                    SELECT TOP 1 
                        id_sessao, id_caixa, id_atendente_abertura, id_atendente_fechamento,
                        data_abertura, data_fechamento, valor_abertura, valor_fechamento_apurado, status_sessao
                    FROM 
                        tb_caixa_sessoes 
                    WHERE 
                        id_caixa = @IdCaixa AND status_sessao = 'A'
                    ORDER BY 
                        data_abertura DESC";

                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@IdCaixa", idCaixa);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CaixaSessao sessao = new CaixaSessao();
                            sessao.IdSessao = reader.GetInt32(reader.GetOrdinal("id_sessao"));
                            sessao.IdCaixa = reader.GetInt32(reader.GetOrdinal("id_caixa"));
                            sessao.IdAtendenteAbertura = reader.GetInt32(reader.GetOrdinal("id_atendente_abertura"));

                            sessao.IdAtendenteFechamento = reader.IsDBNull(reader.GetOrdinal("id_atendente_fechamento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_atendente_fechamento"));
                            sessao.DataFechamento = reader.IsDBNull(reader.GetOrdinal("data_fechamento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_fechamento"));
                            sessao.ValorFechamento = reader.IsDBNull(reader.GetOrdinal("valor_fechamento_apurado")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("valor_fechamento_apurado"));

                            sessao.DataAbertura = reader.GetDateTime(reader.GetOrdinal("data_abertura"));
                            sessao.ValorAbertura = reader.GetDecimal(reader.GetOrdinal("valor_abertura"));
                            sessao.StatusSessao = Convert.ToChar(reader.GetString(reader.GetOrdinal("status_sessao")));

                            return sessao;
                        }
                    }
                }
            }

            return null;
        }

    }
}