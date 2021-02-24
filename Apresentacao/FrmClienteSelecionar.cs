using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjectoDeTransferencia;
namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();
            ClienteColecao clientes = clienteNegocios.PesquisaPorNome(txtBoxPesquisa.Text);
            dataGridViewPrincipal.DataSource = null;
            dataGridViewPrincipal.DataSource = clientes;
            dataGridViewPrincipal.Update();
            dataGridViewPrincipal.Refresh();
        }
    }
}
