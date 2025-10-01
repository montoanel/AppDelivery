namespace AppDelivery.Enums // Assegure-se de que o namespace AppDelivery existe e adicione o sub-namespace Enums
{
    // A definição do enum
    public enum TipoAtendimento
    {
        // 0. Venda Rápida (Opção que não exige cliente ou entregador)
        VendaRapida,

        // 1. Delivery (Exige cliente e entregador)
        Delivery,

        // 2. Retirada (Exige cliente)
        Retirada,

        // 3. Encomenda (Exige cliente e data/notificação)
        Encomenda
    }
}