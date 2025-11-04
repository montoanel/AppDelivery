// Arquivo: AppDelivery/DAL/CaixaSessaoDAO.cs
using Microsoft.Data.SqlClient;
using System;
using System.Data; // Necessário para usar SqlDbType

namespace AppDelivery.DAL
{
    public class CaixaSessaoDAO
    {
        // Constante para a FormaS de PagamentoS 'Dinheiro,Cartão, etc'
        private const int ID_FORMA_PAGAMENTO_DINHEIRO = 1;
        private const int ID_FORMA_PAGAMENTO_CARTAO_DE_CREDITO = 2;
        private const int ID_FORMA_PAGAMENTO_CARTAO_DE_DEBITO = 3;
        private const int ID_FORMA_PAGAMENTO_PIX = 4;



        private const char TIPO_MOV_ABERTURA = 'A';
        private const char TIPO_MOV_SANGRIA = 'S';
        private const char TIPO_MOV_REFORCO = 'R';
        private const char TIPO_MOV_VENDA = 'V';

        /// <summary>
        /// Abre uma nova sessão de caixa e registra a movimentação de abertura,
        /// tudo dentro de uma transação.
        /// </summary>
        public void AbrirSessao(CaixaSessao sessao)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // --- PASSO 1: Inserir na tb_caixa_sessoes ---
                    string sqlSessao = @"
                INSERT INTO tb_caixa_sessoes 
                    (id_caixa, id_atendente_abertura, data_abertura, valor_abertura, status_sessao)
                VALUES 
                    (@IdCaixa, @IdAtendente, @DataAbertura, @ValorAbertura, @Status);
                
                SELECT SCOPE_IDENTITY();";

                    int novoSessaoId;
                    using (SqlCommand cmdSessao = new SqlCommand(sqlSessao, con, transaction))
                    {
                        // Parâmetros robustos (resolvendo o erro @ValorAbertura e DateTime)
                        cmdSessao.Parameters.Add("@IdCaixa", SqlDbType.Int).Value = sessao.IdCaixa;
                        cmdSessao.Parameters.Add("@IdAtendente", SqlDbType.Int).Value = sessao.IdAtendenteAbertura;
                        cmdSessao.Parameters.Add("@DataAbertura", SqlDbType.DateTime).Value = sessao.DataAbertura;
                        cmdSessao.Parameters.Add("@ValorAbertura", SqlDbType.Decimal).Value = sessao.ValorAbertura;
                        cmdSessao.Parameters.Add("@Status", SqlDbType.Char).Value = sessao.StatusSessao;

                        novoSessaoId = Convert.ToInt32(cmdSessao.ExecuteScalar());
                    }

                    if (sessao.ValorAbertura > 0)
                    {
                        // --- PASSO 2: Inserir na tb_caixa_movimentos ---
                        // CORREÇÃO: Usando 'id_sessao' e 'id_atendente' (conforme discutido)
                        string sqlMov = @"
                    INSERT INTO tb_caixa_movimentos 
                        (id_sessao, data_movimento, tipo_movimento, valor, id_forma_pagamento, id_atendente, observacao)
                    VALUES
                        (@IdSessao, @DataAbertura, 'A', @ValorAbertura, @IdFormaPagamento, @IdAtendente, @Obs);
                ";

                        using (SqlCommand cmdMov = new SqlCommand(sqlMov, con, transaction))
                        {
                            // Parâmetros para a movimentação
                            cmdMov.Parameters.Add("@IdSessao", SqlDbType.Int).Value = novoSessaoId;
                            cmdMov.Parameters.Add("@IdFormaPagamento", SqlDbType.Int).Value = ID_FORMA_PAGAMENTO_DINHEIRO;
                            cmdMov.Parameters.Add("@ValorAbertura", SqlDbType.Decimal).Value = sessao.ValorAbertura;
                            cmdMov.Parameters.Add("@DataAbertura", SqlDbType.DateTime).Value = sessao.DataAbertura;
                            cmdMov.Parameters.Add("@IdAtendente", SqlDbType.Int).Value = sessao.IdAtendenteAbertura; // 
                            cmdMov.Parameters.Add("@Obs", SqlDbType.VarChar).Value = "VALOR DE ABERTURA (SUPRIMENTO)";

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

        /// <summary>
        /// Busca uma sessão de caixa aberta para um determinado caixa.
        /// </summary>
        public CaixaSessao VerificarSessaoAberta(int caixaId)
        {
            CaixaSessao sessao = null;

            string query = @"
                SELECT 
                    * FROM 
                    tb_caixa_sessoes 
                WHERE 
                    id_caixa = @IdCaixa AND status_sessao = 'A'"; // status 'A' de Aberta

            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCaixa", caixaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mapeamento dos dados do banco para o objeto CaixaSessao
                            sessao = new CaixaSessao();
                            sessao.IdSessao = reader.GetInt32(reader.GetOrdinal("id_sessao"));
                            sessao.IdCaixa = reader.GetInt32(reader.GetOrdinal("id_caixa"));

                            // id_atendente_abertura (Conforme o seu padrão de coluna)
                            sessao.IdAtendenteAbertura = reader.GetInt32(reader.GetOrdinal("id_atendente_abertura"));

                            // Campos de Abertura
                            sessao.DataAbertura = reader.GetDateTime(reader.GetOrdinal("data_abertura"));
                            sessao.ValorAbertura = reader.GetDecimal(reader.GetOrdinal("valor_abertura"));
                            sessao.StatusSessao = Convert.ToChar(reader.GetString(reader.GetOrdinal("status_sessao")));

                            // Campos de Fechamento (Podem ser NULL no banco)
                            sessao.IdAtendenteFechamento = reader.IsDBNull(reader.GetOrdinal("id_atendente_fechamento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_atendente_fechamento"));
                            sessao.DataFechamento = reader.IsDBNull(reader.GetOrdinal("data_fechamento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_fechamento"));
                            sessao.ValorFechamentoApurado = reader.IsDBNull(reader.GetOrdinal("valor_fechamento_apurado")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("valor_fechamento_apurado"));

                            return sessao;
                        }
                    }
                }
            }
            return null;
        }

        // Outros métodos como FecharSessao e BuscarHistoricoSessoes seriam implementados aqui.
    }
}