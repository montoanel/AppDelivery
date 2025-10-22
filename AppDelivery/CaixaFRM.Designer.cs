// AppDelivery/CaixaFRM.Designer.cs
namespace AppDelivery
{
    partial class CaixaFRM
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gbDados = new GroupBox();
            chkAtivo = new CheckBox();
            txtID = new TextBox();
            lblID = new Label();
            txtNome = new TextBox();
            lblNome = new Label();
            panelBotoes = new Panel();
            btnEditar = new Button();
            btnSair = new Button();
            btnSalvar = new Button();
            btnNovo = new Button();
            gridCaixas = new DataGridView();
            gbDados.SuspendLayout();
            panelBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCaixas).BeginInit();
            SuspendLayout();
            // 
            // gbDados
            // 
            gbDados.Controls.Add(chkAtivo);
            gbDados.Controls.Add(txtID);
            gbDados.Controls.Add(lblID);
            gbDados.Controls.Add(txtNome);
            gbDados.Controls.Add(lblNome);
            gbDados.Location = new Point(17, 20);
            gbDados.Margin = new Padding(4, 5, 4, 5);
            gbDados.Name = "gbDados";
            gbDados.Padding = new Padding(4, 5, 4, 5);
            gbDados.Size = new Size(800, 167);
            gbDados.TabIndex = 0;
            gbDados.TabStop = false;
            gbDados.Text = "Dados do Caixa";
            // 
            // chkAtivo
            // 
            chkAtivo.AutoSize = true;
            chkAtivo.Checked = true;
            chkAtivo.CheckState = CheckState.Checked;
            chkAtivo.Location = new Point(699, 68);
            chkAtivo.Margin = new Padding(4, 5, 4, 5);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(80, 29);
            chkAtivo.TabIndex = 2;
            chkAtivo.Text = "Ativo";
            chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(27, 67);
            txtID.Margin = new Padding(4, 5, 4, 5);
            txtID.Name = "txtID";
            txtID.Size = new Size(107, 31);
            txtID.TabIndex = 0;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(27, 37);
            lblID.Margin = new Padding(4, 0, 4, 0);
            lblID.Name = "lblID";
            lblID.Size = new Size(30, 25);
            lblID.TabIndex = 2;
            lblID.Text = "ID";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(144, 67);
            txtNome.Margin = new Padding(4, 5, 4, 5);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(544, 31);
            txtNome.TabIndex = 1;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(144, 37);
            lblNome.Margin = new Padding(4, 0, 4, 0);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(144, 25);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome/Descrição";
            // 
            // panelBotoes
            // 
            panelBotoes.Controls.Add(btnEditar);
            panelBotoes.Controls.Add(btnSair);
            panelBotoes.Controls.Add(btnSalvar);
            panelBotoes.Controls.Add(btnNovo);
            panelBotoes.Dock = DockStyle.Bottom;
            panelBotoes.Location = new Point(0, 651);
            panelBotoes.Margin = new Padding(4, 5, 4, 5);
            panelBotoes.Name = "panelBotoes";
            panelBotoes.Size = new Size(834, 102);
            panelBotoes.TabIndex = 2;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(144, 30);
            btnEditar.Margin = new Padding(4, 5, 4, 5);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(119, 52);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(699, 30);
            btnSair.Margin = new Padding(4, 5, 4, 5);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(119, 52);
            btnSair.TabIndex = 3;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(271, 30);
            btnSalvar.Margin = new Padding(4, 5, 4, 5);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(119, 52);
            btnSalvar.TabIndex = 1;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(17, 30);
            btnNovo.Margin = new Padding(4, 5, 4, 5);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(119, 52);
            btnNovo.TabIndex = 0;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // gridCaixas
            // 
            gridCaixas.AllowUserToAddRows = false;
            gridCaixas.AllowUserToDeleteRows = false;
            gridCaixas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCaixas.Location = new Point(17, 197);
            gridCaixas.Margin = new Padding(4, 5, 4, 5);
            gridCaixas.Name = "gridCaixas";
            gridCaixas.ReadOnly = true;
            gridCaixas.RowHeadersWidth = 62;
            gridCaixas.RowTemplate.Height = 25;
            gridCaixas.Size = new Size(800, 445);
            gridCaixas.TabIndex = 1;
            gridCaixas.CellClick += gridCaixas_CellClick;
            // 
            // CaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 753);
            Controls.Add(gridCaixas);
            Controls.Add(panelBotoes);
            Controls.Add(gbDados);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "CaixaFRM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro de Caixas";
            Load += CaixaFRM_Load;
            gbDados.ResumeLayout(false);
            gbDados.PerformLayout();
            panelBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCaixas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbDados;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnSair;
        // O btnExcluir foi removido daqui
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.DataGridView gridCaixas;
        private Button btnEditar;
    }
}