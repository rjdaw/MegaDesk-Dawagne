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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void bthAddQuote_Click(object sender, EventArgs e)
        {
            var addQuote = new AddQuote(this);
            //Tag lets you store whatver you want
            addQuote.Tag = this;
            addQuote.Show();
            this.Hide(); //this refers to the current class object, which in this case is the main menu form 
        }

        private void btnViewQuotes_Click(object sender, EventArgs e)
        {
            var viewQuote = new ViewAllQuotes(this);
            //Tag lets you store whatver you want
            viewQuote.Tag = this;
            viewQuote.Show();
            this.Hide(); //this refers to the current class object, which in this case is the main menu form 
        }

        private void btnSearchQuotes_Click(object sender, EventArgs e)
        {
            var searchQuote = new SearchQuotes(this);
            //Tag lets you store whatver you want
            searchQuote.Tag = this;
            searchQuote.Show();
            this.Hide(); //this refers to the current class object, which in this case is the main menu form 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
