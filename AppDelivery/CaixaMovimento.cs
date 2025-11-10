using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppDelivery.Enums;


namespace AppDelivery
{
    public class CaixaMovimento
    {
        public int IdMovimento { get; set; }
        public int IdSessao { get; set; } // <-- Corrigido (como você confirmou)
        public int? IdFormaPagamento { get; set; }
        public int? IdAtendimento { get; set; }
        public TipoOperacaoCaixa TipoMovimento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataMovimento { get; set; }
        public string Observacao { get; set; }

        // --- CAMINHO 2 (Recomendado) ---
        public int IdAtendente { get; set; } // O ID de quem fez este movimento
        // ------------------------------

        public CaixaMovimento()
        {
            this.DataMovimento = DateTime.Now;
            this.IdFormaPagamento = null;
            this.IdAtendimento = null;
        }
    }
}
