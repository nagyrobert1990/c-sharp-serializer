using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serializer
{
    public partial class Serializer : Form
    {
        public Serializer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text) & !String.IsNullOrEmpty(txtAddress.Text) & !String.IsNullOrEmpty(txtPhone.Text)) {
                Person person = new Person(txtName.Text, txtAddress.Text, txtPhone.Text);
                person.Serialize();
            }
            else
            {
                MessageBox.Show("Fill it all!");
            }
        }
    }
}
