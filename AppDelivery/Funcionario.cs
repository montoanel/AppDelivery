// Arquivo: AppDelivery/Funcionario.cs
using System;

namespace AppDelivery
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public char StatusFuncionario { get; set; } // <--- ADICIONADO CORRETAMENTE

        public decimal? ComissaoFuncionario { get; set; }
    }
}