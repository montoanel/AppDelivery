using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace AppDelivery.DAL
{
    // Tornando a classe 'public' para ser acessível pelo RecebimentoFRM
    public class FormasPagamentoDAO
    {
        // 1. Removi a instância 'private Conexao conexao'
        // 2. Removi o construtor, pois não é mais necessário

        /// <summary>
        /// Busca SOMENTE as formas de pagamento com status 'A' (Ativo)
        /// </summary>
        /// <returns>DataTable com id_forma_pagamento e descricao</returns>
        public DataTable GetFormasPagamentoAtivas()
        {
            DataTable dt = new DataTable();

            // Query usando os nomes de coluna do seu print
            string query = "SELECT id_forma_pagamento, nome_forma_pagamento FROM tb_formas_pagamento WHERE status = 'A' ORDER BY nome_forma_pagamento";

            // ***** PADRÃO CORRIGIDO (igual ao CaixaDAO) *****
            // Usando o método estático Conexao.GetConnection()
            using (SqlConnection con = Conexao.GetConnection())
            {
                // Abrindo a conexão manualmente, assim como no CaixaDAO
                con.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar formas de pagamento: " + ex.Message, "Erro DAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // A conexão é fechada automaticamente pelo 'using (con)'
            }
            return dt;
        }

        // Você pode adicionar outros métodos de CRUD (Salvar, Editar, etc.) aqui
        // seguindo este mesmo padrão de conexão.
    }
}