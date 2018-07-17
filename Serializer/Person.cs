using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serializer
{
    [Serializable()]
    public class Person
    {
        public static int personCounter = FindLastPersonCount();
        public String Name { get; }
        public String Address { get; }
        public String Phone { get; }
        public DateTime Recorded { get; }
        [NonSerialized()] public int serial;

        public Person(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Recorded = DateTime.Now;
        }

        public static int FindLastPersonCount()
        {
            int lastCount = 0;
            string[] files = Directory.GetFiles(@".\");
            foreach (string item in files)
            {
                if(Regex.IsMatch(item, @"(Person)"))
                {
                    string numberString = Regex.Match(item, @"\d+").Value;
                    int number = Int16.Parse(numberString);
                    if (number > lastCount)
                    {
                        lastCount = number;
                    }
                }
            }
            return lastCount;
        }

        public static int FindFirstPersonCount()
        {
            int firstCount = 99;
            int number = 0;
            string[] files = Directory.GetFiles(@".\");
            foreach (string item in files)
            {
                if (Regex.IsMatch(item, @"(Person)"))
                {
                    string numberString = Regex.Match(item, @"\d+").Value;
                    number = Int16.Parse(numberString);
                    if (number < firstCount)
                    {
                        firstCount = number;
                    }
                }
            }
            return firstCount;
        }

        public void Serialize()
        {
            if (personCounter <= 99)
            {
                string output = "Person" + personCounter.ToString() + ".dat";
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(output,
                                         FileMode.Create,
                                         FileAccess.Write, FileShare.None);
                this.serial = personCounter;
                formatter.Serialize(stream, this);
                stream.Close();
                personCounter++;
            }
            else
            {
                MessageBox.Show("Too many person");
            }
        }
    }
}
