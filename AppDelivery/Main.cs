using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDelivery
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void pctBoxClientes_Click(object sender, EventArgs e)
        {
            // 1. Crie uma nova instância do seu formulário ListaClientes
            ListaClientes formListaClientes = new ListaClientes();

            // 2. Exiba o formulário

            // Opção A: Exibir o formulário como uma janela modal (bloqueia o formulário pai até ser fechada)
            formListaClientes.ShowDialog();

            // Opção B: Exibir o formulário como uma janela não modal (permite interagir com o formulário pai)
            //formListaClientes.Show();

            // Opção C: Se você quiser esconder o formulário atual ao abrir o novo
            //this.Hide();
            //formListaClientes.Show(); // ou ShowDialog();
        }

        private void Main_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pctBoxClientes_MouseEnter(object sender, EventArgs e)
        {
            pctBoxClientes.BackColor = Color.LightBlue; // Uma cor suave de destaque

        }

        private void pctBoxClientes_MouseLeave(object sender, EventArgs e)
        {
            pctBoxClientes.BackColor = Color.Transparent; // Ou Color.White, dependendo do fundo do seu formulário
        }

        private void pctBoxProdutos_MouseLeave(object sender, EventArgs e)
        {
            pctBoxProdutos.BackColor = Color.Transparent;
        }

        private void pctBoxProdutos_MouseEnter(object sender, EventArgs e)
        {
            pctBoxProdutos.BackColor = Color.LightBlue; // Uma cor suave de destaque
        }

        private void pctBoxProdutos_Click(object sender, EventArgs e)
        {
            //criar nova instancia do formulario
            ListaProdutosFRM listaProdutosFRM = new ListaProdutosFRM();
            listaProdutosFRM.ShowDialog();  
        }
    }
}
