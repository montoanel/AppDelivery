// Arquivo: AppDelivery/CaixaSessao.cs
// (Substitua o conteúdo deste arquivo)
using System;

namespace AppDelivery
{
    public class CaixaSessao
    {
        public int IdSessao { get; set; }
        public int IdCaixa { get; set; }
        public int IdAtendenteAbertura { get; set; }
        public int? IdAtendenteFechamento { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal ValorAbertura { get; set; }
        public decimal? ValorFechamentoApurado { get; set; }
        public char StatusSessao { get; set; } // A = Aberta, F = Fechada
    }
}