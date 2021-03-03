using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data
{
    public partial class Complete : Form
    {
        SqlConnection connection;
        string connectionString;
        public Complete()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Data.Properties.Settings.MaintenaceConnectionString"].ConnectionString;
        }

        private void Complete_Load(object sender, EventArgs e)
        {
            JobInsert();
            DescriptionInsert();
            DateInsert();
            PermInsert();
            EmployeeInsert();
            OtherInsert();
            StatusInsert();
            Save.Enabled = false;
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

                CurentTitle.DisplayMember = "Title";
                CurentTitle.ValueMember = "Id";
                CurentTitle.DataSource = Title;
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

                CurrentDescription.DisplayMember = "Description";
                CurrentDescription.ValueMember = "Id";
                CurrentDescription.DataSource = Description;
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

                CurrentDueDate.DisplayMember = "DueDate";
                CurrentDueDate.ValueMember = "Id";
                CurrentDueDate.DataSource = CurrentDate;
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

                CurrentPermit.DisplayMember = "Title";
                CurrentPermit.ValueMember = "Id";
                CurrentPermit.DataSource = Perm;
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

                CurrentEmployee.DisplayMember = "ConcatenatedField";
                CurrentEmployee.ValueMember = "Id";
                CurrentEmployee.DataSource = Fore;
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

                CurrentOther.DisplayMember = "OtherDetails";
                CurrentOther.ValueMember = "OtherDetails";
                CurrentOther.DataSource = Other;
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

                CurrentStatus.DisplayMember = "StatusID";
                CurrentStatus.ValueMember = "Id";
                CurrentStatus.DataSource = Status;
            }
        }
        private void SaveJob()
        {
            string Other = Convert.ToString(OtherBox.Text);
            string OtherOther = Convert.ToString(CurrentOther.SelectedValue);
            int Id = MainForm.TitleSelect;
            string Query = (@"UPDATE Jobs
SET
OtherDetails ='" + Other + "//" + OtherOther + "',StatusID ='Complete' WHERE Id = @ID");
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
        private void setButtonVisibility()
        {
            if ((OtherBox.Text != String.Empty)) // checks if all the text boxes have text in them.
            {
                Save.Enabled = true; // enables the save button
            }
            else
            {
                Save.Enabled = false;// disables the save button
            }
        }
        private void OtherBox_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
    }
}
