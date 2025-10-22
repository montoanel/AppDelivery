// Arquivo: AppDelivery/Caixa.cs

namespace AppDelivery
{
    public class Caixa
    {
        // Mapeado para a coluna id_caixa
        public int Id { get; set; }

        // Mapeado para a coluna nome_caixa
        public string Nome { get; set; }

        // Mapeado para a coluna ativo ('A' para Ativo, 'I' para Inativo)
        public char Ativo { get; set; }

        public Caixa(int id, string nome, char ativo)
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