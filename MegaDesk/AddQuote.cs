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
    public partial class AddQuote : Form
    {
        private Form _mainMenu;
        public AddQuote(Form mainMenu)
        {
            InitializeComponent();

            _mainMenu = mainMenu;

            //This gives the desktop material options
            List<DesktopMaterial> materials = Enum.GetValues(typeof(DesktopMaterial))
                            .Cast<DesktopMaterial>()
                            .ToList();

            surfaceMaterial.DataSource = materials;

            //this makes it so that the intial value is blank
            surfaceMaterial.SelectedIndex = -1; 

            //This gives the delivery options
            List<Delivery> deliveries = Enum.GetValues(typeof(Delivery))
                            .Cast<Delivery>()
                            .ToList();

            deliveryType.DataSource = deliveries;

            //this makes it so that the intial value is blank
            deliveryType.SelectedIndex = -1; 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddQuote_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainMenu)this.Tag).Show();
        }

        private void btnGetQuote_Click(object sender, EventArgs e)
        {
            var desk = new Desk
            {
                Depth = depthInput.Value,
                Width = widthInput.Value,
                NumberOfDrawers = (int)numberOfDrawers.Value,  //This turns the input into an int
                SurfaceMaterial = (DesktopMaterial)surfaceMaterial.SelectedValue
            };

            var deskQuote = new DeskQuote
            {
                Desk = desk,
                CustomerName = CustomerNameBox.Text,
                QuoteDate = DateTime.Now,
                DeliveryType = (Delivery)deliveryType.SelectedValue

            };

            try
            {
                //This will get the quote total from deskQuote
                var price = deskQuote.GetQuotePrice();

                //Add the amount to quote
                deskQuote.QuotePrice = price;

                //Adds the quote to the file 
                AddQuoteToFile(deskQuote);

                // this will show the 'DisplayQuote' form 
                DisplayQuote frmDisplayQuote = new DisplayQuote(_mainMenu, deskQuote);
                frmDisplayQuote.Show();
                Hide();               
            }
            catch (Exception err)
            {
                MessageBox.Show("There was an error creating the quote. {0}", err.InnerException.ToString());
            }
        }

        private void AddQuoteToFile(DeskQuote deskQuote)
        {
            var quotesFile = @"quotes.json";
            List<DeskQuote> deskQuotes = new List<DeskQuote>(); //This is an empty list


            if (File.Exists(quotesFile))
            {
                using (StreamReader reader = new StreamReader(quotesFile))
                {
                    string quotes = reader.ReadToEnd();

                    if (quotes.Length > 0)
                    {
                        deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes); //This adds to the list
                    }
                }
            }

            //This adds the new Quote
            deskQuotes.Add(deskQuote);

            //This saves the file 
            SaveQuote(deskQuotes);
        }

        private void SaveQuote(List<DeskQuote> quotes)
        {
            var quotesFile = @"quotes.json";

            //Takes the list and turns it into a string 
            var serializedQuotes = JsonConvert.SerializeObject(quotes);

            //Writes the quotes to the file 
            File.WriteAllText(quotesFile, serializedQuotes);
        }

        private void deliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddQuote_Load(object sender, EventArgs e)
        {

        }
    }
}
