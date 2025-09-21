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
    public partial class AtendimentosFRM : Form
    {
        public AtendimentosFRM()
        {
            InitializeComponent();
        }

        private void pctNovoAtendimento_MouseLeave(object sender, EventArgs e)
        {
            pctNovoAtendimento.BackColor = Color.Transparent;
        }

        private void pctNovoAtendimento_MouseEnter(object sender, EventArgs e)
        {
            pctNovoAtendimento.BackColor = Color.LightGray;
        }
    }
}
