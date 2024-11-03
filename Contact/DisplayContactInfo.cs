using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Contact
{
    public partial class DisplayContactInfo : Form
    {
        private List<ContactInfo> contacts = new List<ContactInfo>();
        private int currentIndex = -1;
        public DisplayContactInfo()
        {
            InitializeComponent();
            

        }

        public DisplayContactInfo(int id)
        {
            InitializeComponent();
            LoadContactDataByID(id);
        }

        private void LoadContactData()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ContacInfo;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to get all data from ContactInfo table
                    string query = "SELECT ContactID, FirstName, MiddleName, LastName, Type, Telephone, Extension, EmailAddress FROM ContactInfo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create a list to store contacts temporarily
                            List<ContactInfo> contactList = new List<ContactInfo>();

                            while (reader.Read())
                            {
                                // Create a new Contact object for each row
                                ContactInfo contact = new ContactInfo
                                {
                                    ID = reader.GetInt32(0), // Assuming ContactId is the first column
                                    FirstName = reader.GetString(1),
                                    MiddleName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    LastName = reader.GetString(3),
                                    Type = reader.GetString(4),
                                    Telephone = reader.GetString(5),
                                    Extension = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    EmailAddress = reader.IsDBNull(7) ? null : reader.GetString(7)
                                };

                                contactList.Add(contact);
                            }

                            // Convert the list to an array
                            contacts = contactList.ToList();
                            if(contacts.Count > 0)
                            {
                                currentIndex = 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            if(currentIndex != -1)
            {
                textBox1.Text = contacts[currentIndex].ID.ToString();
                textBox2.Text = contacts[currentIndex].FirstName;
                textBox3.Text = contacts[currentIndex].MiddleName;
                textBox4.Text = contacts[currentIndex].LastName;
                comboBox1.Text = contacts[currentIndex].Type;
                textBox5.Text = contacts[currentIndex].Telephone;
                textBox6.Text = contacts[currentIndex].Extension;
                textBox7.Text = contacts[currentIndex].EmailAddress;

            }
        }


        private void DisplayContactInfo_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void DisplayCurrentContact()
        {
            if (currentIndex != -1 && currentIndex < contacts.Count)
            {
                textBox1.Text = contacts[currentIndex].ID.ToString();
                textBox2.Text = contacts[currentIndex].FirstName;
                textBox3.Text = contacts[currentIndex].MiddleName ?? ""; // Handle potential null values
                textBox4.Text = contacts[currentIndex].LastName;
                comboBox1.Text = contacts[currentIndex].Type;
                textBox5.Text = contacts[currentIndex].Telephone;
                textBox6.Text = contacts[currentIndex].Extension ?? ""; // Handle potential null values
                textBox7.Text = contacts[currentIndex].EmailAddress ?? ""; // Handle potential null values
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--; // Move to the previous record
                DisplayCurrentContact(); // Update the UI with the new contact
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentIndex < contacts.Count - 1)
            {
                currentIndex++; // Move to the next record
                DisplayCurrentContact(); // Update the UI with the new contact
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContactData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewContact f = new AddNewContact();

            f.ShowDialog();
        }

        private void LoadContactDataByID(int id)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ContacInfo;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to get data for a specific ContactId
                    string query = "SELECT ContactID, FirstName, MiddleName, LastName, Type, Telephone, Extension, EmailAddress " +
                                   "FROM ContactInfo WHERE ContactID = @ContactID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContactID", id); // Parameterized query to prevent SQL injection

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Read only one record for the specific ID
                            {
                                // Assign the values to the fields
                                textBox1.Text = reader.GetInt32(0).ToString(); // ContactID
                                textBox2.Text = reader.GetString(1); // FirstName
                                textBox3.Text = reader.IsDBNull(2) ? "" : reader.GetString(2); // MiddleName
                                textBox4.Text = reader.GetString(3); // LastName
                                comboBox1.Text = reader.GetString(4); // Type
                                textBox5.Text = reader.GetString(5); // Telephone
                                textBox6.Text = reader.IsDBNull(6) ? "" : reader.GetString(6); // Extension
                                textBox7.Text = reader.IsDBNull(7) ? "" : reader.GetString(7); // EmailAddress
                            }
                            else
                            {
                                MessageBox.Show("Contact not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void seachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchContact searchContact = new SearchContact();
            searchContact.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
