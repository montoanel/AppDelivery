// AppDelivery/Caixa.cs
namespace AppDelivery
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } // true = Ativo, false = Inativo

        public Caixa(int id, string nome, bool ativo)
        {
            Id = id;
            Nome = nome;
            Ativo = ativo;
        }

        public Caixa()
        {
        }
    }
}