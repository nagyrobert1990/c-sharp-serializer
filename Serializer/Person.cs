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
        public String Name { get; }
        public String Address { get; }
        public String Phone { get; }
        public DateTime Recorded { get; }
        [NonSerialized()] private int serial;
        public int Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        public Person(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Recorded = DateTime.Now;
        }

        public static Person GetPersonBySerialNum(int serialNum)
        {
            string strSerial = "";
            if (serialNum <= 9)
            {
                strSerial = "0" + serialNum.ToString();
            }
            else
            {
                strSerial = serialNum.ToString();
            }
            string[] files = Directory.GetFiles(@".\");
            Person person = null;
            foreach (string item in files)
            {
                if (Regex.IsMatch(item, @"(Person)"))
                {
                    string numberString = Regex.Match(item, @"\d+").Value;
                    if (numberString.Equals(strSerial))
                    {
                        person = Deserialize(item);
                    }
                }
            }
            return person;
        }

        public static int FindLastPersonCount()
        {
            int lastCount = -1;
            string[] files = Directory.GetFiles(@".\");
            foreach (string item in files)
            {
                if (Regex.IsMatch(item, @"(Person)"))
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

        public void Serialize(string lblNum)
        {
            int number = 0;
            if (!String.IsNullOrEmpty(lblNum))
            {
                number = Int16.Parse(lblNum);
            }
            else
            {
                number = FindLastPersonCount() + 1;
            }
            if (number <= 99)
            {
                string output = "";
                if (number <= 9)
                {
                    output = "Person0" + number.ToString() + ".dat";
                }
                else
                {
                    output = "Person" + number.ToString() + ".dat";
                }
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(output,
                                         FileMode.Create,
                                         FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
            }
            else
            {
                MessageBox.Show("Too many person");
            }
        }

        public static Person Deserialize(string input)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(input,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
            Person person = (Person)formatter.Deserialize(stream);
            stream.Close();
            person.Serial = Int16.Parse(Regex.Match(input, @"\d+").Value);
            return person;
        }
    }
}
