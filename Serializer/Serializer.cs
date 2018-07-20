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

namespace Serializer
{
    public partial class Serializer : Form
    {
        private static int personCounter;

        public Serializer()
        {
            InitializeComponent();
        }

        private void DisplayPerson(Person person)
        {
            txtName.Text = person.Name;
            txtAddress.Text = person.Address;
            txtPhone.Text = person.Phone;
            personCounter = person.Serial;
            lblCounter.Text = $"Serial: {personCounter}";
            lblDate.Text = $"Recorded: {person.Recorded.Year}. {person.Recorded.Month}. {person.Recorded.Day}.";
        }

        private void NewPerson()
        {
            txtName.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtPhone.Text = String.Empty;
            lblCounter.Text = String.Empty;
            lblDate.Text = String.Empty;
        }

        private void Serializer_Load(object sender, EventArgs e)
        {
            try
            {
                Person person = Person.GetPersonBySerialNum(Person.FindFirstPersonCount());
                DisplayPerson(person);
            }
            catch (Exception)
            {
                Person person = new Person("nem joo", "nem joo", "nem joo");
                txtName.Text = person.Name;
                txtAddress.Text = person.Address;
                txtPhone.Text = person.Phone;
                lblCounter.Text = "nem joo";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text) & !String.IsNullOrEmpty(txtAddress.Text) & !String.IsNullOrEmpty(txtPhone.Text))
            {
                Person person = new Person(txtName.Text, txtAddress.Text, txtPhone.Text);
                string lblNum = Regex.Match(lblCounter.Text, @"\d+").Value;
                person.Serialize(lblNum);
            }
            else
            {
                MessageBox.Show("Fill it all!");
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            NewPerson();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            Person person;
            if(personCounter < Person.FindLastPersonCount())
            {
                person = Person.GetPersonBySerialNum(personCounter + 1);
                DisplayPerson(person);
            }
            person = Person.GetPersonBySerialNum(personCounter);
            DisplayPerson(person);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            Person person;
            if (personCounter > 0)
            {
                person = Person.GetPersonBySerialNum(personCounter - 1);
                DisplayPerson(person);
            }
            person = Person.GetPersonBySerialNum(personCounter);
            DisplayPerson(person);
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            Person person = Person.GetPersonBySerialNum(Person.FindFirstPersonCount());
            DisplayPerson(person);
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            Person person = Person.GetPersonBySerialNum(Person.FindLastPersonCount());
            DisplayPerson(person);
        }
    }
}
