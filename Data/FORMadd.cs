using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data
{
    public partial class FORMadd : Form
    {
        SqlConnection connection;
        string connectionString;
        public FORMadd()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Data.Properties.Settings.MaintenaceConnectionString"].ConnectionString;
        }
        //Load
        private void FORMadd_Load(object sender, EventArgs e)
        {
            PermitDrop();
            EmployeeDrop();
            Save.Enabled = false;//disables the save button
        }
        private void PermitDrop()//populates a drop down menu with the permits Title.
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Permit", connection))
            {
                DataTable Permit1 = new DataTable();
                adapter.Fill(Permit1);

                comboBox1.DisplayMember = "Title";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = Permit1;
            }
        }
        private void EmployeeDrop()//populates a drop down menu with the employees full name.
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", connection))
            {
                DataTable Employ1 = new DataTable();
                adapter.Fill(Employ1);
                Employ1.Columns.Add("ConcatenatedName", typeof(string), "Forename + '  ' +Surname");// selects the forename and surname inserting a space between them.

                comboBox2.DataSource = Employ1;
                comboBox2.ValueMember = "Id";
                comboBox2.DisplayMember = "ConcatenatedName";
            }
        }
        // Add
        private void SaveJob()
        {
            string Title = Convert.ToString(TitleBox.Text);// converts the text in the title text box to be s tring under the variable name title. the next few lines do the same thing,
            string Description = Convert.ToString(DescriptionBox.Text);
            string Date = Convert.ToString(dateTimePicker1.Value.Date);
            string Other = Convert.ToString(OtherBox.Text);
            int employee = Convert.ToInt16(comboBox2.SelectedValue);
            int perm = Convert.ToInt16(comboBox1.SelectedValue);
            string Query = ("INSERT INTO Jobs (Title,Description,DueDate,OtherDetails,StatusID,EmployeeID,PermitID) VALUES ('" + Title + "','" + Description + "','" + Date + "','" + Other + "','Unstarted','" + employee + "','" + perm + "')"); // inserts the data placed on the from into the database.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void Save_Click(object sender, EventArgs e)//save button
        {
            SaveJob();
            MessageBox.Show("Uploaded"); // shows a message box telling the user the job has been uploaded 
            this.Close(); // closes the add from and the message box.
        }
        private void TitleBox_TextChanged(object sender, EventArgs e)// runs the code in the bracket as the text changes in the text box. 
        {
            setButtonVisibility();
        }
        private void DescriptionBox_TextChanged(object sender, EventArgs e)// runs the code in the bracket as the text changes in the text box. 
        {
            setButtonVisibility();
        }
        private void OtherBox_TextChanged(object sender, EventArgs e)// runs the code in the bracket as the text changes in the text box. 
        {
            setButtonVisibility();
        }
        private void setButtonVisibility()
        {
            if ((TitleBox.Text != String.Empty) && (DescriptionBox.Text != String.Empty) && (OtherBox.Text != String.Empty)) // checks if all the text boxes have text in them.
            {
                Save.Enabled = true; // enables the save button
            }
            else
            {
                Save.Enabled = false;// disables the save button 
            }
        }
    }
}
