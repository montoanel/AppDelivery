// AppDelivery/DAL/ParametroSistema.cs

// CORRIGIDO: Adicionado 'using AppDelivery;' para encontrar a classe 'Caixa'
using AppDelivery;
using System;
using System.Windows.Forms;
// REMOVIDO: 'using AppDelivery.DAL;' não é mais necessário, pois estamos neste namespace

// CORRIGIDO: O namespace DEVE ser 'AppDelivery.DAL' para corresponder à pasta
namespace AppDelivery.DAL
{
    public static class ParametroSistema
    {
        public static int IdCaixaAtual { get; private set; }
        public static string NomeCaixaAtual { get; private set; }
        public static bool IsCaixaAtivo { get; private set; }

        // Esta propriedade estática armazena o objeto Caixa
        public static Caixa CaixaDaEstacao { get; private set; }

        // O DAO para buscar o caixa (agora é encontrado pois está no mesmo namespace)
        private static CaixaDAO caixaDAO = new CaixaDAO();


        // [MÉTODO REFATORADO E CORRIGIDO]
        public static bool CarregarParametrosCaixa()
        {
            // 1. Lê o ID salvo no config.ini local
            // ConfigLocal está no mesmo namespace (AppDelivery.DAL), então é encontrado
            int idCaixaConfigurado = ConfigLocal.LerIdCaixa();

            if (idCaixaConfigurado > 0)
            {
                // 2. Busca os dados completos no banco
                // caixaDAO é o campo estático (definido acima)
                // Caixa é encontrado por causa do 'using AppDelivery;'
                CaixaDaEstacao = caixaDAO.BuscarPorId(idCaixaConfigurado);

                if (CaixaDaEstacao != null && CaixaDaEstacao.Ativo == 'A')
                {
                    // Encontrou e o caixa está Ativo
                    IdCaixaAtual = CaixaDaEstacao.Id;
                    NomeCaixaAtual = CaixaDaEstacao.Nome;
                    IsCaixaAtivo = true;
                    return true;
                }
                else if (CaixaDaEstacao != null && CaixaDaEstacao.Ativo == 'I')
                {
                    // Encontrou, mas o caixa está Inativo
                    IdCaixaAtual = CaixaDaEstacao.Id;
                    NomeCaixaAtual = CaixaDaEstacao.Nome + " (INATIVO)";
                    IsCaixaAtivo = false;
                    return false;
                }
                else
                {
                    // O ID salvo no config.ini não existe mais no banco
                    CaixaDaEstacao = null;
                    IdCaixaAtual = 0;
                    NomeCaixaAtual = "Config. Inválida";
                    IsCaixaAtivo = false;
                    return false;
                }
            }
            else
            {
                // Não encontrou ID no config.ini
                CaixaDaEstacao = null;
                IdCaixaAtual = 0;
                NomeCaixaAtual = "Não Configurado";
                IsCaixaAtivo = false;
                return false;
            }
        }

        // Método de Ação Rápida (não precisa de alteração)
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