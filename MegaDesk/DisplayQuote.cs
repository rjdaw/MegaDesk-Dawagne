using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class DisplayQuote : Form
    {

        public DisplayQuote(Form mainMenu, DeskQuote deskQuote)
        {
            InitializeComponent();

            CustomerNameBox.Text = deskQuote.CustomerName;
            widthInput.Value = deskQuote.Desk.Width;
            depthInput.Value = deskQuote.Desk.Depth;
            numberOfDrawers.Value = deskQuote.Desk.NumberOfDrawers;
            surfaceMaterial.Text = deskQuote.Desk.SurfaceMaterial.ToString();
            deliveryType.Text = deskQuote.DeliveryType.ToString();
            quotePrice.Text = "$ " + deskQuote.QuotePrice.ToString();
        }

        private void DisplayQuote_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainMenu)this.Tag).Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
  
        }

        private void CustomerNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void deliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DisplayQuote_Load(object sender, EventArgs e)
        {

        }
    }
}
