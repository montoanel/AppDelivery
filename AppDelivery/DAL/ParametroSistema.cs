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

        // Nome do computador para buscar no banco
        public static readonly string NomeMaquina = System.Environment.MachineName;

        // Tenta buscar a configuração do caixa para a máquina atual
        public static bool CarregarParametrosCaixa()
        {
            ConfiguracaoCaixaDAO dao = new ConfiguracaoCaixaDAO();
            Caixa caixa = dao.BuscarCaixaConfigurado(NomeMaquina);

            if (caixa != null)
            {
                IdCaixaAtual = caixa.Id;
                NomeCaixaAtual = caixa.Nome;
                IsCaixaAtivo = (caixa.Ativo == 'A');
                return true;
            }
            else
            {
                IdCaixaAtual = 0;
                NomeCaixaAtual = "Não Configurado";
                IsCaixaAtivo = false;
                return false;
            }
        }

        // Método de Ação Rápida para ser chamado antes de operações financeiras
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