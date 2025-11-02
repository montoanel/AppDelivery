using System;

namespace AppDelivery
{
    // Esta classe é o 'espelho' da sua tabela tb_caixa_sessoes
    public class CaixaSessao
    {
        public int IdSessao { get; set; }
        public int IdCaixa { get; set; }

        // Dados de Abertura
        public DateTime DataAbertura { get; set; }
        public int IdAtendenteAbertura { get; set; }
        public decimal ValorAbertura { get; set; }

        // Dados de Fechamento (permitem nulos)
        public DateTime? DataFechamento { get; set; }
        public int? IdAtendenteFechamento { get; set; }
        public decimal? ValorFechamentoApurado { get; set; }

        // 'A' = Aberto, 'F' = Fechado
        public char StatusSessao { get; set; }

        // Construtor padrão
        public CaixaSessao() { }
    }
}