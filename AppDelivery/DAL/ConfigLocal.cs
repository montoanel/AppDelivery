using System;
using System.IO;
using System.Linq;

namespace AppDelivery.DAL
{
    /// <summary>
    /// Classe estática para ler e escrever a configuração local (config.ini).
    /// </summary>
    public static class ConfigLocal
    {
        // Define o nome do arquivo de configuração.
        // Ele ficará na mesma pasta do AppDelivery.exe
        private static string GetConfigFilePath()
        {
            // AppDomain.CurrentDomain.BaseDirectory é o caminho mais seguro
            // para encontrar a pasta do executável.
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        }

        /// <summary>
        /// Lê o arquivo config.ini e retorna o ID do caixa configurado.
        /// </summary>
        /// <returns>O ID do Caixa, ou 0 se não estiver configurado ou der erro.</returns>
        public static int LerIdCaixa()
        {
            string filePath = GetConfigFilePath();

            // Se o arquivo não existe, não há configuração.
            if (!File.Exists(filePath))
            {
                return 0; // 0 = Não configurado
            }

            try
            {
                // Lê todas as linhas do arquivo
                var lines = File.ReadAllLines(filePath);

                // Procura pela linha que começa com "ID_CAIXA="
                var configLine = lines.FirstOrDefault(line =>
                    line.Trim().StartsWith("ID_CAIXA=", StringComparison.OrdinalIgnoreCase));

                if (configLine != null)
                {
                    // Pega o valor (o que vem depois do "=")
                    string valor = configLine.Split('=')[1].Trim();

                    // Tenta converter para número
                    if (int.TryParse(valor, out int idCaixa))
                    {
                        return idCaixa;
                    }
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro de leitura (ex: permissão), assume 0
                Console.WriteLine("Erro ao ler config.ini: " + ex.Message);
            }

            return 0; // Padrão
        }

        /// <summary>
        /// Salva o ID do Caixa selecionado no arquivo config.ini.
        /// </summary>
        /// <param name="idCaixa">O ID do caixa a ser salvo (ou 0 para desvincular).</param>
        public static void SalvarIdCaixa(int idCaixa)
        {
            string filePath = GetConfigFilePath();

            // Cria o conteúdo formatado
            string content = $"[Configuracao]{Environment.NewLine}ID_CAIXA={idCaixa}{Environment.NewLine}";

            try
            {
                // (Sobre)escreve o arquivo com a nova configuração
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                // Se der erro (ex: pasta sem permissão de escrita), avisa o usuário.
                throw new Exception("Falha ao salvar o arquivo 'config.ini'. Verifique as permissões da pasta. Erro: " + ex.Message, ex);
            }
        }
    }
}