using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class SearchQuotes : Form
    {
        private Form _mainMenu; 
        public SearchQuotes(Form mainMenu)
        {
                InitializeComponent();

                _mainMenu = mainMenu;

                //populates the material combo box
                List<DesktopMaterial> materials = Enum.GetValues(typeof(DesktopMaterial))
                                .Cast<DesktopMaterial>()
                                .ToList();

                surfaceMaterial.DataSource = materials;

                //this makes it so that the intial value is blank
                surfaceMaterial.SelectedIndex = -1;


            loadGrid();

        }

        private void SearchQuotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainMenu)this.Tag).Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SurfaceMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            if (combo.SelectedIndex < 0)
            {
                loadGrid();
            }
            else
            {
                // Search the Grid
                loadGrid((DesktopMaterial)combo.SelectedValue);
            }
        }

        private void loadGrid()
        {
            var quotesFile = @"quotes.json";

            if (File.Exists(quotesFile))
            {
                using (StreamReader reader = new StreamReader(quotesFile))
                {
                    string quotes = reader.ReadToEnd();

                    List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes);

                    dataGridView1.DataSource = deskQuotes.Select(d => new
                    {
                        Date = d.QuoteDate,
                        Customer = d.CustomerName,
                        Depth = d.Desk.Depth,
                        Width = d.Desk.Width,
                        Drawers = d.Desk.NumberOfDrawers,
                        SurfaceMaterial = d.Desk.SurfaceMaterial,
                        DeliveryType = d.DeliveryType,
                        QuoteAmount = d.QuotePrice.ToString("c")
                    }) //This is select method 
                        .ToList(); //This is what makes it run 
                }
            }
        }

        private void loadGrid(DesktopMaterial desktopMaterial)
        {
            var quotesFile = @"quotes.json";

            if (File.Exists(quotesFile))
            {
                using (StreamReader reader = new StreamReader(quotesFile))
                {
                    string quotes = reader.ReadToEnd();

                    List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes);

                    dataGridView1.DataSource = deskQuotes.Select(d => new
                    {
                        Date = d.QuoteDate,
                        Customer = d.CustomerName,
                        Depth = d.Desk.Depth,
                        Width = d.Desk.Width,
                        Drawers = d.Desk.NumberOfDrawers,
                        SurfaceMaterial = d.Desk.SurfaceMaterial,
                        DeliveryType = d.DeliveryType,
                        QuoteAmount = d.QuotePrice

                    })
                        .Where(q => q.SurfaceMaterial == desktopMaterial) //Where the quote equals the desktop material, filters 
                        .ToList(); //This is what makes it run 
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
