using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace AppDelivery.DAL
{
    public class CaixaSessaoDAO
    {
        /// <summary>
        /// Verifica se existe uma sessão de caixa aberta para o ID do caixa informado.
        /// </summary>
        /// <param name="idCaixa">ID do caixa (vindo do ParametroSistema.IdCaixaAtual)</param>
        /// <returns>Um objeto CaixaSessao se houver uma sessão aberta, ou null se estiver fechado.</returns>
        public CaixaSessao VerificarSessaoAberta(int idCaixa)
        {
            CaixaSessao sessao = null;

            using (SqlConnection con = Conexao.GetConnection())
            {
                // Busca a sessão que está com status 'A' (Aber_t_a)
                string sql = "SELECT * FROM tb_caixa_sessoes " +
                             "WHERE id_caixa = @idCaixa AND status_sessao = 'A'";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idCaixa", idCaixa);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Se encontrou um registro
                        {
                            sessao = new CaixaSessao();
                            sessao.IdSessao = Convert.ToInt32(reader["id_sessao"]);
                            sessao.IdCaixa = Convert.ToInt32(reader["id_caixa"]);
                            sessao.DataAbertura = Convert.ToDateTime(reader["data_abertura"]);
                            sessao.IdAtendenteAbertura = Convert.ToInt32(reader["id_atendente_abertura"]);
                            sessao.ValorAbertura = Convert.ToDecimal(reader["valor_abertura"]);
                            sessao.StatusSessao = Convert.ToChar(reader["status_sessao"]);

                            // (Campos de fechamento estarão nulos, não precisamos lê-los aqui)
                        }
                    }
                }
            }
            return sessao; // Retorna null se não encontrou nada
        }

        /// <summary>
        /// Insere um novo registro de sessão de caixa no banco de dados.
        /// </summary>
        /// <param name="sessao">O objeto CaixaSessao preenchido com os dados da abertura.</param>
        /// <returns>O ID da nova sessão criada.</returns>
        public int AbrirSessao(CaixaSessao sessao)
        {
            using (SqlConnection con = Conexao.GetConnection())
            {
                string sql = "INSERT INTO tb_caixa_sessoes " +
                             "(id_caixa, data_abertura, id_atendente_abertura, valor_abertura, status_sessao) " +
                             "VALUES " +
                             "(@idCaixa, @dataAbertura, @idAtendente, @valorAbertura, 'A'); " +
                             "SELECT CAST(scope_identity() AS int);"; // Retorna o ID que acabou de ser criado

                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idCaixa", sessao.IdCaixa);
                    cmd.Parameters.AddWithValue("@dataAbertura", sessao.DataAbertura);
                    cmd.Parameters.AddWithValue("@idAtendente", sessao.IdAtendenteAbertura);
                    cmd.Parameters.AddWithValue("@valorAbertura", sessao.ValorAbertura);

                    // ExecuteScalar é usado aqui para pegar o ID de retorno
                    int novoIdSessao = (int)cmd.ExecuteScalar();
                    return novoIdSessao;
                }
            }
        }

        // (No futuro, adicionaremos aqui o método 'FecharSessao(int idSessao, ...)' )
    }
}