using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Contact
{
    public partial class SearchContact : Form
    {
        public SearchContact()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contactId = Convert.ToInt32(textBox1.Text);

            DisplayContactInfo df = new DisplayContactInfo(contactId);
            df.ShowDialog();

        }
    }
}
