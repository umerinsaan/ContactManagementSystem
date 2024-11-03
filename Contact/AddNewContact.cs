using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact
{
    public partial class AddNewContact : Form
    {
        public AddNewContact()
        {
            InitializeComponent();
            ID_init();
        }

        private void ID_init()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ContacInfo;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to get the maximum ContactId
                    string query = "SELECT MAX(ContactId) FROM ContactInfo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            // If there's a maximum value, convert it to an integer and assign it to textBox7
                            int maxContactId = Convert.ToInt32(result);
                            textBox7.Text = (maxContactId + 1).ToString(); // Optionally, add 1 to prepare for the next ID
                        }
                        else
                        {
                            // If no records exist, set textBox7 to 1 (or any other default value)
                            textBox7.Text = "1";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //phone textbox
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string input = Regex.Replace(textBox4.Text, "[^0-9]", "");

            // Format the number
            if (input.Length > 0)
            {
                // Example format: (123) 456-7890
                if (input.Length > 6)
                {
                    textBox4.Text = $"({input.Substring(0, 3)}) {input.Substring(3, 3)}-{input.Substring(6, Math.Min(4, input.Length - 6))}";
                }
                else if (input.Length > 3)
                {
                    textBox4.Text = $"({input.Substring(0, 3)}) {input.Substring(3)}";
                }
                else
                {
                    textBox4.Text = input;
                }
            }
            else
            {
                textBox4.Text = "(___) ___-____";
            }

            // Move the cursor to the end of the text
            textBox4.SelectionStart = textBox4.Text.Length;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "(___)___ - ____")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "(___) ___-____";
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ContacInfo;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Step 1: Get the maximum ContactId
                    string maxContactIdQuery = "SELECT MAX(ContactId) FROM ContactInfo";
                    int maxContactId = 0;

                    using (SqlCommand command = new SqlCommand(maxContactIdQuery, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            maxContactId = Convert.ToInt32(result);
                        }
                    }

                    // Step 2: Prepare the data to insert
                    int contactID = maxContactId + 1; // New ContactId
                    string firstName = textBox1.Text;
                    string middleName = textBox2.Text;
                    string lastName = textBox3.Text;
                    string type = comboBox1.Text;
                    string telephone = textBox4.Text;
                    string extension = textBox5.Text;
                    string emailAddress = textBox6.Text;

                    // Step 3: Insert the new contact info into the database
                    string insertQuery = "INSERT INTO ContactInfo (ContactId, FirstName, MiddleName, LastName, Type, Telephone, Extension, EmailAddress) " +
                                         "VALUES (@ContactId, @FirstName, @MiddleName, @LastName, @Type, @Telephone, @Extension, @EmailAddress)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@ContactId", contactID);
                        insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                        insertCommand.Parameters.AddWithValue("@MiddleName", middleName);
                        insertCommand.Parameters.AddWithValue("@LastName", lastName);
                        insertCommand.Parameters.AddWithValue("@Type", type);
                        insertCommand.Parameters.AddWithValue("@Telephone", telephone);
                        insertCommand.Parameters.AddWithValue("@Extension", extension);
                        insertCommand.Parameters.AddWithValue("@EmailAddress", emailAddress);


                        // Execute the insert command
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        // Show success message after executing the command
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The contact has been saved!", "Save Successful!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1; // Clear selection
        }

        //ID TEXTBOX
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
