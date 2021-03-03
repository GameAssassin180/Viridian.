using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data
{
    public partial class MainForm : Form
    {
        SqlConnection connection; // this represents the connection to the database and is given the name connection.
        public static int TitleSelect; //  a public integer to transfer a selected value to anothe form.
        string connectionString; // decleration of the connection string.
        public MainForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Data.Properties.Settings.MaintenaceConnectionString"].ConnectionString; // the connection string.
        }
        // Main Load
        private void MainForm_Load(object sender, EventArgs e) // runs the code inside when the main form is loaded.
        {
            InitTimer();// starts the timer 
            JobLoad();// loads the job titles.
            StatusCheck();// checks the status is correct.
            comboBox1.SelectedIndex = 0;// sets the drop down menu to its first value.

            button5.Enabled = false;// turns the start button off
            button6.Enabled = false;// turns the complete button off
        }
        private void JobLoad()// declares a public void called Jobload.
        {
            using (connection = new SqlConnection(connectionString)) // declares a new connection using the connection string from earlier in the code. 
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Jobs", connection)) // declares an SQLadapter called adapter which allows the run query to be displayed as a data table.
            {
                DataTable job = new DataTable(); // declares a new datatable called jobs 
                adapter.Fill(job); // fills the new data table with the infromation grabed from the ran query

                JobBox.DisplayMember = "Title";// sets the listbox jobbox to dispaly the title column from the data table.  
                JobBox.ValueMember = "Id";// sets the listbox jobbox to have the value of the Id column from the data table.
                JobBox.DataSource = job;// sets the listbox jobboxs' datasourse to the data table jobs declared erlier.
            }
        }
        private void DescriptionLoad()// declares a public void called Descriptionload, this will load the description that mattches the id from the jobbox.
        {
            string Query = ("SELECT Description FROM Jobs WHERE Id = @Id"); // declars the query string, this one selects the description from the jobs table where the id is equal to the selected value in the job box.
            using (connection = new SqlConnection(connectionString))// declares a new connection using the connection string from earlier in the code.
            using (SqlCommand command = new SqlCommand(Query, connection)) // creates a new SQL command called command that runs the query and connection.
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))// declares a new SQL adapter called adapter that runs the above command.
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);// sets the @Id in the query to the selected Id value from the Job box.

                DataTable descr = new DataTable();// declares a new datatable called descr
                adapter.Fill(descr);// fills the new data table with the infromation grabed from the ran query

                DescriptionBox.DisplayMember = "Description";// sets the listbox Descriptionbox to dispaly the description column from the data table.
                DescriptionBox.ValueMember = "Id";// sets the listbox Descriptionbox to have the value of the Id column from the data table.
                DescriptionBox.DataSource = descr;// sets the listbox Descriptionboxs' datasourse to the data table descr declared erlier.
            }
        }
        // the next functions(Dateload,Detaiload, and Statload) are coppies of the function DescriptionLoad with exseptions such as varable names and displaymebers.
        private void DateLoad()
        {
            string Query = ("SELECT DueDate FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable date = new DataTable();
                adapter.Fill(date);

                DateBox.DisplayMember = "DueDate";
                DateBox.ValueMember = "DueDate";
                DateBox.DataSource = date;
            }
        }
        private void DetailLoad()
        {
            string Query = ("SELECT OtherDetails FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable detail = new DataTable();
                adapter.Fill(detail);

                DetailBox.DisplayMember = "OtherDetails";
                DetailBox.ValueMember = "OtherDetails";
                DetailBox.DataSource = detail;
            }

        }
        private void StatLoad()
        {
            string Query = ("SELECT StatusID FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable stat = new DataTable();
                adapter.Fill(stat);

                StatBox.DisplayMember = "StatusID";
                StatBox.ValueMember = "StatusID";
                StatBox.DataSource = stat;
            }
        }
        private void PermLoad()
        {
            string Query = (@"SELECT Jobs.Id,Permit.Title
FROM Jobs
INNER JOIN Permit ON Jobs.PermitID = Permit.Id
WHERE Jobs.Id = @Id; "); // this SQL query selects the id column from the jobs table and the title from the permit table and joins them together where the permitID field and the permit id column are equal.
            // the rest of the code is the same as the description load function.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable Perm = new DataTable();
                adapter.Fill(Perm);

                PermBox.DisplayMember = "Title";
                PermBox.ValueMember = "Id";
                PermBox.DataSource = Perm;
            }

        }
        private void ForeLoad()
        {
            string Query = (@"SELECT Jobs.Id, Employee.Forename
FROM Jobs
INNER JOIN Employee ON Jobs.EmployeeID = Employee.Id
WHERE Jobs.Id = @Id; ");// this SQL query selects the id column from the jobs table and the forename from the employee table and joins the two together where the employee id and the employeeID field are equal.
            // the rest of the code is the same as the description load function.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable Fore = new DataTable();
                adapter.Fill(Fore);

                ForeBox.DisplayMember = "Forename";
                ForeBox.ValueMember = "Id";
                ForeBox.DataSource = Fore;
            }

        }
        private void SurLoad()
        {
            string Query = (@"SELECT Jobs.Id, Employee.Surname
FROM Jobs
INNER JOIN Employee ON Jobs.EmployeeID = Employee.Id
WHERE Jobs.Id = @Id; ");// this SQL query selects the id column from the jobs table and the surname from the employee table and joins the two together where the employee id and the employeeID field are equal
            // the rest of the code is the same as the description load function.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable Sur = new DataTable();
                adapter.Fill(Sur);

                SurBox.DisplayMember = "Surname";
                SurBox.ValueMember = "Id";
                SurBox.DataSource = Sur;
            }

        }
        private void ProfLoad()
        {
            string Query = (@" SELECT Jobs.Id, Employee.ProfessionID, Profession.Title
        FROM((Jobs
        INNER JOIN Employee ON Jobs.EmployeeID = Employee.Id)
INNER JOIN Profession on Employee.ProfessionID = Profession.Id)
WHERE Jobs.Id = @Id;"); // this SQL query uses a double inner join as i want to display the employees profesion that links to the seleced job.
            // the rest of the code is the same as the description load function.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);

                DataTable Prof = new DataTable();
                adapter.Fill(Prof);

                ProfBox.DisplayMember = "Title";
                ProfBox.ValueMember = "Id";
                ProfBox.DataSource = Prof;
            }

        }
        private void JobBox_SelectedIndexChanged(object sender, EventArgs e) // runs the code inside the {} when the selected item in the listbox job box is changed.
        {
            DescriptionLoad(); // runs the DescriptionLoad function.
            DateLoad(); // runs the DateLoad function.
            DetailLoad(); // runs the DetailLoad function.
            ForeLoad(); // runs the ForeLoad function.
            SurLoad(); // runs the SurLoad function.
            ProfLoad(); // runs the ProfLoad function.
            PermLoad(); // runs the PermLoad function.
            StatLoad(); // runs the StatLoad function.
            StatusCheck(); // runs StatusCheck the function.
            StatusColour(); // runs the colour changing function
            checking(); // runs a button locking function
        }
        // Add
        private void button1_Click(object sender, EventArgs e)// add button.
        {
            FORMadd Load = new FORMadd(); // creates a new add form.
            Load.ShowDialog(); // shows the new form to the user.
        }
        // Delete
        private void button2_Click(object sender, EventArgs e)//delet button.
        {
            JobGone(); // runs the jobgone function.
        }
        private void JobGone()
        {
            string Query = ("DELETE FROM Jobs WHERE Id = @Id");
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                connection.Open(); // opens the connection to the database.
                command.Parameters.AddWithValue("@Id", JobBox.SelectedValue);
                command.ExecuteScalar(); // runs the command as an adapter isnt present.
                connection.Close(); // closes the connection to the database.
            }
            JobLoad(); // by running the job load it gives the impression of the job instatly disapearing.
        }
        private void button4_Click(object sender, EventArgs e)// refresh button. 
        {
            JobLoad();
            StatusCheck();
            comboBox1.SelectedIndex = 0;
        }
        //Amend
        private void button3_Click(object sender, EventArgs e)// amend button.
        {
            TitleSelect = Convert.ToInt16(JobBox.SelectedValue);// sets the integer Titleselects to the id of the selected job.
            AmendForm load = new AmendForm(); // creates a new amend job form.
            load.ShowDialog(); // shows the user the new form.
        }
        //Status Change
        private void StatusCheck()
        {
            DateTime Status = Convert.ToDateTime(DateBox.SelectedValue); // sets the value of the variable status to the date box.
            DateTime Today = Convert.ToDateTime(DateTime.Now.Date);// sets the value of the variable today to the current date.
            string status = Convert.ToString(StatBox.SelectedValue);// sets the value of the status bar to a string.
            if (Status <= Today && status == "Unstarted") // if statment to check if the two variables are the same.
            {
                string Query = (@"UPDATE Jobs SET StatusID ='Overdue' WHERE Id = " + JobBox.SelectedValue + ""); // changes the status to overdue where the datbox and todayts date are the same.
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        private void StatusColour()//changes the colour of the status box depending on the status.
        {
            string StatusC = Convert.ToString(StatBox.SelectedValue);
            if (StatusC == "Complete")
            {
                StatBox.BackColor = System.Drawing.Color.Green;
            }
            else if (StatusC == "OverDue" || StatusC == "Overdue")
            {
                StatBox.BackColor = System.Drawing.Color.Red;
            }
            else if (StatusC == "Started")
            {
                StatBox.BackColor = System.Drawing.Color.Blue;
            }
            else if (StatusC == "Unstarted" || StatusC == "UnStarted")
            {
                StatBox.BackColor = System.Drawing.Color.Purple;
            }
        }
        //completing and starting.
        private void checking()
        {
            string StatusBut = Convert.ToString(StatBox.SelectedValue);
            if (StatusBut == "Unstarted" || StatusBut == "UnStarted" || StatusBut == "Overdue" || StatusBut == "OverDue")
            {
                button5.Enabled = true;
                button6.Enabled = false;
            }
            else if (StatusBut == "Started")
            {
                button5.Enabled = false;
                button6.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
                button6.Enabled = false;
            }
        }
        private void button5_Click(object sender, EventArgs e)// start button.
        {
            string Query = (@"UPDATE Jobs SET StatusID ='Started' WHERE Id = " + JobBox.SelectedValue + "");// changes the status to started where the where the id is equal to the selected value.
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void button6_Click(object sender, EventArgs e)// Complete button
        {
            TitleSelect = Convert.ToInt16(JobBox.SelectedValue);
            Complete load = new Complete();
            load.ShowDialog();
        }
        //filter/order by
        private void FilteredStatus()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Jobs ORDER BY StatusID DESC", connection))//filters the jobs by the status in decending order showing unstarted first started second and over due third.
            {
                DataTable job = new DataTable();
                adapter.Fill(job);

                JobBox.DisplayMember = "Title";
                JobBox.ValueMember = "Id";
                JobBox.DataSource = job;
            }
        }
        private void FilteredTitle()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Jobs ORDER BY Title", connection)) //filters the jobs by the title in alphabetical order.
            {
                DataTable job = new DataTable();
                adapter.Fill(job);

                JobBox.DisplayMember = "Title";
                JobBox.ValueMember = "Id";
                JobBox.DataSource = job;
            }
        }
        private void FilteredId()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Jobs ORDER BY Id", connection))//filters the jobs by the ID in acending order.
            {
                DataTable job = new DataTable();
                adapter.Fill(job);

                JobBox.DisplayMember = "Title";
                JobBox.ValueMember = "Id";
                JobBox.DataSource = job;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)// runs the code in the {} brakets when the selected value in the drop down menu is changed.
        {
            // if statments to check the selected value of a drop down menu.
            if (comboBox1.Text == "Status")
            {
                FilteredStatus();
            }
            else if (comboBox1.Text == "Title")
            {
                FilteredTitle();
            }
            else if (comboBox1.Text == "Id")
            {
                FilteredId();
            }
        }
        //timer for 30 min refresh
        public void InitTimer()// decalres a public void called inittimer for initialization of the timer.
        {
            timer1 = new Timer(); // declares a new timer.
            timer1.Tick += new EventHandler(timer1_Tick); // run the private void timer1_tick after the declared time has elapsed. 
            timer1.Interval = 1800000; // declares the time interval to 30 mins in milliseconds.
            timer1.Start(); // starts the timer.
        }
        private void timer1_Tick(object sender, EventArgs e)// decalres a private void called timer1_tick.
        {
            JobLoad(); // runs the jobload function.
            StatusCheck();
            comboBox1.SelectedItem = 0;
        }
    }
}
