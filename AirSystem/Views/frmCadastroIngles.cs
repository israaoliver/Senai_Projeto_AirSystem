using AirSystem.Models;
using AirSystem.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSystem.Views
{
    public partial class frmCadastroIngles : Form
    {
        public frmCadastroIngles()
        {
            InitializeComponent();
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            lblSenhaAviso.Visible = false;
            lblSenhaRegex.Visible = false;

            InvisibleLabel(lblNome);
            InvisibleLabel(lblEndereco);
            InvisibleLabel(lblNascimento);
            InvisibleLabel(lblConfirmarSenha);
            InvisibleLabel(lblSenha);
            InvisibleLabel(lblSenhaAviso);
            InvisibleLabel(lblSenhaRegex);
            InvisibleLabel(lblUsuario);

            cbxAdm.Parent = panel2;
            cbxAdm.BackColor = Color.Transparent;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();

            bool campoVazio = false;
            foreach (Control item in this.Controls)
            {
                MarkedWaterTextBox tbx = item as MarkedWaterTextBox;

                if (tbx != null && tbx.WaterMarkText == tbx.Text)
                {
                    campoVazio = true;
                }
            }

            if (!campoVazio)
            {

                if (tbxSenha.Text == tbxConfirmarSenha.Text)
                {
                    Usuario usuario = new Usuario
                    {
                        Id = 0,
                        Nome = tbxNome.Text,
                        Sobrenome = tbxSobrenome.Text,
                        DataNascimento = Convert.ToDateTime(dtpNascimento.Text),
                        Endereco = $"{tbxEndereco.Text}, {tbxNendereco.Text}",
                        Username = tbxUsuario.Text,
                        Senha = tbxSenha.Text,
                        IsAdmin = Convert.ToBoolean(cbxAdm.Checked)
                    };

                    if (usuarioRepository.Cadastrar(usuario))
                    {
                        MessageBox.Show("Registered Successfully.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Password must be the same.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void tbxConfirmarSenha_TextChanged(object sender, EventArgs e)
        {
            _ = tbxSenha.Text == tbxConfirmarSenha.Text ? lblSenhaAviso.Visible = false : lblSenhaAviso.Visible = true;
        }

        private void tbxSenha_TextChanged(object sender, EventArgs e)
        {
            string pattern = "[A-Z]{1}[a-z]{1,}[0-9]{1,}";

            lblSenhaRegex.Visible = !Regex.IsMatch(tbxSenha.Text, pattern);

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File(*.jpg;*.png)|*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pcbUserFoto.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            pcbUserFoto.Image = null;
        }

        private void InvisibleLabel(Label labelName)
        {
            labelName.Parent = panel2;
            labelName.BackColor = Color.Transparent;

        }

        private void Focus_enter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(60, Color.Yellow);
        }

        private void Focus_leave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
        }

        private void frmCadastroUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(this, "Are you sure want to quit?", "Confirm", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
