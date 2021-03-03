using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data
{
    public partial class AmendForm : Form
    {
        SqlConnection connection;
        string connectionString;
        public AmendForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Data.Properties.Settings.MaintenaceConnectionString"].ConnectionString;
        }
        private void AmendForm_Load(object sender, EventArgs e)
        {
            PermitDrop();
            EmployeeDrop();
            JobInsert();
            DescriptionInsert();
            DateInsert();
            PermInsert();
            EmployeeInsert();
            OtherInsert();
            StatusInsert();
            Save.Enabled = false;
        }
        private void PermitDrop()
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
        private void EmployeeDrop()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", connection))
            {
                DataTable Employ1 = new DataTable();
                adapter.Fill(Employ1);
                Employ1.Columns.Add("ConcatenatedField", typeof(string), "Forename + '  ' +Surname");

                comboBox2.DataSource = Employ1;
                comboBox2.ValueMember = "Id";
                comboBox2.DisplayMember = "ConcatenatedField";
            }
        }
        private void JobInsert()
        {
            int Id = MainForm.TitleSelect;
            string Query = ("SELECT Title FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Title = new DataTable();
                adapter.Fill(Title);

                TitleBox.Text = Title.Rows[0]["Title"].ToString();
            }
        }
        private void DescriptionInsert()
        {
            int Id = MainForm.TitleSelect;
            string Query = ("SELECT Description FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Description = new DataTable();
                adapter.Fill(Description);

                DescriptionBox.Text = Description.Rows[0]["Description"].ToString();
            }
        }
        private void DateInsert()
        {
            int Id = MainForm.TitleSelect;
            string Query = ("SELECT DueDate FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable CurrentDate = new DataTable();
                adapter.Fill(CurrentDate);

                dateTimePicker1.Text = CurrentDate.Rows[0]["DueDate"].ToString();
            }
        }
        private void PermInsert()
        {
            string Query = (@"SELECT Jobs.Id,Permit.Title
FROM Jobs
INNER JOIN Permit ON Jobs.PermitID = Permit.Id
WHERE Jobs.Id = @Id; ");
            int Id = MainForm.TitleSelect;
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Perm = new DataTable();
                adapter.Fill(Perm);

                comboBox1.Text = Perm.Rows[0]["Title"].ToString();
            }

        }
        private void EmployeeInsert()
        {
            string Query = (@"SELECT Jobs.Id, Employee.Forename, Employee.Surname
FROM Jobs
INNER JOIN Employee ON Jobs.EmployeeID = Employee.Id
WHERE Jobs.Id = @Id; ");
            int Id = MainForm.TitleSelect;
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Fore = new DataTable();
                adapter.Fill(Fore);
                Fore.Columns.Add("ConcatenatedField", typeof(string), "Forename + '  ' +Surname");

                comboBox2.Text = Fore.Rows[0]["ConcatenatedField"].ToString();
            }
        }
        private void OtherInsert()
        {
            int Id = MainForm.TitleSelect;
            string Query = ("SELECT OtherDetails FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Other = new DataTable();
                adapter.Fill(Other);

                OtherBox.Text = Other.Rows[0]["OtherDetails"].ToString();
            }
        }
        private void StatusInsert()
        {
            int Id = MainForm.TitleSelect;
            string Query = ("SELECT StatusID FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", Id);

                DataTable Status = new DataTable();
                adapter.Fill(Status);

                StatusBox.Text = Status.Rows[0]["StatusID"].ToString();
            }
        }
        private void SaveJob()
        {
            string Title = Convert.ToString(TitleBox.Text);
            string Description = Convert.ToString(DescriptionBox.Text);
            string Date = Convert.ToString(dateTimePicker1.Value.Date);
            string Other = Convert.ToString(OtherBox.Text);
            string Status = Convert.ToString(StatusBox.Text);
            int employee = Convert.ToInt16(comboBox2.SelectedValue);
            int perm = Convert.ToInt16(comboBox1.SelectedValue);
            int Id = MainForm.TitleSelect;
            string Query = (@"UPDATE Jobs
SET
Title = '" + Title + "',Description = '" + Description + "',DueDate = '" + Date + "',OtherDetails ='" + Other + "',StatusID ='" + Status + "',EmployeeID ='" + employee + "',PermitID ='" + perm + "' WHERE Id = @ID");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", Id);
                command.ExecuteNonQuery();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            SaveJob();
            string saved = ("Saved");
            MessageBox.Show(saved);
            this.Close();
        }
        private void TitleBox_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        private void DescriptionBox_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        private void StatusBox_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        private void OtherBox_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        private void setButtonVisibility()
        {
            if ((TitleBox.Text != String.Empty) && (DescriptionBox.Text != String.Empty) && (OtherBox.Text != String.Empty) && (StatusBox.Text != String.Empty)) // checks if all the text boxes have text in them.
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