// AppDelivery/ParametroSistema.cs

using AppDelivery.DAL;
using System;
using System.Windows.Forms;

namespace AppDelivery
{
    public static class ParametroSistema
    {
        public static int IdCaixaAtual { get; private set; }
        public static string NomeCaixaAtual { get; private set; }
        public static bool IsCaixaAtivo { get; private set; }
        public static CaixaSessao SessaoAtual { get; private set; }

        // --- NOVO MÉTODO ---
        /// <summary>
        /// Verifica no banco de dados se existe uma sessão aberta para o caixa desta máquina.
        /// Atualiza a propriedade 'SessaoAtual'.
        /// </summary>
        /// <returns>True se encontrou uma sessão aberta, False se o caixa está fechado.</returns>
        public static bool CarregarSessaoAtual()
        {
            // Primeiro, garante que sabemos qual é o caixa
            if (IdCaixaAtual == 0)
            {
                SessaoAtual = null;
                return false;
            }

            try
            {
                // Usa o DAO que acabamos de criar
                CaixaSessaoDAO sessaoDAO = new CaixaSessaoDAO();
                SessaoAtual = sessaoDAO.VerificarSessaoAberta(IdCaixaAtual);

                return (SessaoAtual != null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar sessão de caixa: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SessaoAtual = null;
                return false;
            }
        }

        // REMOVIDO: O nome da máquina não é mais necessário aqui.
        // public static readonly string NomeMaquina = System.Environment.MachineName;

        // [MÉTODO REFATORADO]
        // Tenta buscar a configuração do caixa para a máquina atual
        public static bool CarregarParametrosCaixa()
        {
            // Instancia o DAO que BUSCA os dados do caixa
            CaixaDAO caixaDAO = new CaixaDAO();

            // 1. Lê o ID salvo no arquivo config.ini local
            int idCaixaConfigurado = ConfigLocal.LerIdCaixa();

            if (idCaixaConfigurado > 0)
            {
                // 2. Se encontrou um ID, busca os dados completos no banco
                Caixa caixa = caixaDAO.BuscarPorId(idCaixaConfigurado);

                if (caixa != null && caixa.Ativo == 'A')
                {
                    // Encontrou e o caixa está Ativo
                    IdCaixaAtual = caixa.Id;
                    NomeCaixaAtual = caixa.Nome;
                    IsCaixaAtivo = true;
                    return true;
                }
                else if (caixa != null && caixa.Ativo == 'I')
                {
                    // Encontrou, mas o caixa está Inativo
                    IdCaixaAtual = caixa.Id;
                    NomeCaixaAtual = caixa.Nome + " (INATIVO)";
                    IsCaixaAtivo = false;
                    return false;
                }
                else
                {
                    // O ID salvo no config.ini não existe mais no banco (ex: foi excluído)
                    IdCaixaAtual = 0;
                    NomeCaixaAtual = "Config. Inválida";
                    IsCaixaAtivo = false;
                    return false;
                }
            }
            else
            {
                // Não encontrou ID no config.ini
                IdCaixaAtual = 0;
                NomeCaixaAtual = "Não Configurado";
                IsCaixaAtivo = false;
                return false;
            }
        }

        // Método de Ação Rápida para ser chamado antes de operações financeiras
        // Este método [ValidarCaixaAtivo] já estava correto e não precisa de mudanças.
        public static bool ValidarCaixaAtivo(string operacao)
        {
            if (IdCaixaAtual == 0 || !IsCaixaAtivo)
            {
                MessageBox.Show(
                    $"Não é possível realizar a operação de {operacao}. " +
                    "Esta máquina não está configurada para um Caixa Ativo ou o Caixa está inativo. " +
                    "Verifique a configuração de caixas.",
                    "Acesso Bloqueado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}