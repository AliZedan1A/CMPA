using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using CMA_Libary;

namespace CMPA
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserControl : Window
    {
        public UserControl()
        {
            InitializeComponent();
        }
        public string PostNum { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DashWIn dashWIn = new DashWIn();
            dashWIn.WindowState = WindowState;
            dashWIn.PostNum = PostNum;

            dashWIn.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Exit Panel
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (PanelDataGrid.SelectedItems.Count == -1)
                PanelDataGrid.SelectedItems[0] = null;
            txtPanelUser.Text = "";
            txtPanelPass.Text = "";
            newUserPanel.Visibility = Visibility.Collapsed;
        }

        //Exit Panel
        private void btnCansel_Click(object sender, RoutedEventArgs e)
        {
            if (PanelDataGrid.SelectedItems.Count == -1)
                PanelDataGrid.SelectedItems[0] = null;
            txtPanelUser.Text = "";
            txtPanelPass.Text = "";
            newUserPanel.Visibility = Visibility.Collapsed;
        }



        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            newUserPanel.Visibility = Visibility.Visible;
            PanelDataGrid.Items.Clear();

            List<Employee> Tablenames = new List<Employee>();
            ;


            string sqlC = "datasource=127.0.0.1;username=root;password=;database=CMA_DB;SslMode=none";
            MySqlConnection conn = new MySqlConnection(sqlC);
            conn.Open();

            MySqlConnection connection = new MySqlConnection(sqlC);

            connection.Open();
            string query = "SELECT * FROM `users`";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();


            int a = 0;

            while (reader.Read())
            {
                Employee emp = new Employee();

                emp.username = reader["Username"].ToString();
                emp.password = reader["Password"].ToString();
                emp.role = reader["Role"].ToString();
                PanelDataGrid.Items.Add(emp);

                a++;

            }






        }

        private void win_Activated(object sender, EventArgs e)
        {
            try
            {
                MouseLeftButtonDown += delegate
                {
                    DragMove();
                };
            }
            catch (Exception) { }

        }
        public string usernameA { get; set; }

        //selected change
        private void PanelDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            selectedGrid.Visibility = Visibility.Visible;
            btnPanelEnableEdit.Visibility = Visibility.Visible;
            btnPanelAdd.Visibility = Visibility.Collapsed;
            if (PanelDataGrid.SelectedItems.Count > 0)
            {
                var row = PanelDataGrid.SelectedItems[0];
                var employee = (Employee)row;
                usernameA = employee.username;
                txtPanelUser.Text = employee.username;
                txtPanelPass.Text = employee.password;
                cbxPanelRole.Text = employee.role;
            }

        }

        private async void btnChangeUsername_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (txtNewName.Text == "")
            {
                txtMessage.Text = "inputs embity!";
                await timerAsync(ErrBox);
            }
            else
            {
                GetData data = new GetData();
                data.update("username", PostNum, txtNewName.Text);
                txtMessage.Text = "Changed successfully";
                await timerAsync(ErrBox);

            }
        }


        //Error Box
        async Task timerAsync(Border border)
        {
            for (int i = 0; i < 245; i++)
            {
                try
                {
                    if (border.Margin.Left <= -5)
                    {
                        await Task.Delay(10);
                        border.Margin = new Thickness(border.Margin.Left + 5, border.Margin.Top, border.Margin.Right, border.Margin.Bottom);
                    }
                }
                catch (Exception) { }
            }
            SystemSounds.Beep.Play();
            await Task.Delay(3000);
            try
            {
                for (int i = 0; i < 245; i++)
                {
                    try
                    {
                        if (border.Margin.Left >= -245)
                        {
                            await Task.Delay(10);
                            border.Margin = new Thickness(border.Margin.Left - 5, border.Margin.Top, border.Margin.Right, border.Margin.Bottom);
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        private void btnPanelEnableEdit_Click(object sender, RoutedEventArgs e)//////////////////
        {
           
                txtPanelPass.Text = "";
                txtPanelUser.Text = "";
                cbxPanelRole.Text = "";
                selectedGrid.Visibility = Visibility.Collapsed;
                btnPanelAdd.Visibility = Visibility.Visible;
                btnPanelEnableEdit.Visibility = Visibility.Collapsed;

            

        }

        //////////////////////////////////////////////////////////////////Delete and Edit

        private void btnPanelEdit_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            data.updUser(txtPanelUser.Text, txtPanelPass.Text, cbxPanelRole.Text, usernameA);
            PanelDataGrid.Items.Clear();

            List<Employee> Tablenames = new List<Employee>();
            ;


            string sqlC = "datasource=127.0.0.1;username=root;password=;database=CMA_DB;SslMode=none";
            MySqlConnection conn = new MySqlConnection(sqlC);
            conn.Open();

            MySqlConnection connection = new MySqlConnection(sqlC);

            connection.Open();
            string query = "SELECT * FROM `users`";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();


            int a = 0;

            while (reader.Read())
            {
                Employee emp = new Employee();

                emp.username = reader["Username"].ToString();
                emp.password = reader["Password"].ToString();
                emp.role = reader["Role"].ToString();
                PanelDataGrid.Items.Add(emp);

                a++;

            }

        }

        private void btnPanelDelete_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            data.Delet(usernameA);
            PanelDataGrid.Items.Clear();

            List<Employee> Tablenames = new List<Employee>();
            ;


            string sqlC = "datasource=127.0.0.1;username=root;password=;database=CMA_DB;SslMode=none";
            MySqlConnection conn = new MySqlConnection(sqlC);
            conn.Open();

            MySqlConnection connection = new MySqlConnection(sqlC);

            connection.Open();
            string query = "SELECT * FROM `users`";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();


            int a = 0;

            while (reader.Read())
            {
                Employee emp = new Employee();

                emp.username = reader["Username"].ToString();
                emp.password = reader["Password"].ToString();
                emp.role = reader["Role"].ToString();
                PanelDataGrid.Items.Add(emp);

                a++;

            }

        }

        ////////////////////////////////////////////////////////////////// Add
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var ruslt = data.addEmplwe(txtPanelPass.Text, txtPanelUser.Text, cbxPanelRole.Text);

            MessageBox.Show(ruslt);

            if (ruslt == "0")
            {
                PanelDataGrid.Items.Clear();

                List<Employee> Tablenames = new List<Employee>();
                ;


                string sqlC = "datasource=127.0.0.1;username=root;password=;database=CMA_DB;SslMode=none";
                MySqlConnection conn = new MySqlConnection(sqlC);
                conn.Open();

                MySqlConnection connection = new MySqlConnection(sqlC);

                connection.Open();
                string query = "SELECT * FROM `users`";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();


                int a = 0;

                while (reader.Read())
                {
                    Employee emp = new Employee();

                    emp.username = reader["Username"].ToString();
                    emp.password = reader["Password"].ToString();
                    emp.role = reader["Role"].ToString();
                    PanelDataGrid.Items.Add(emp);

                    a++;

                }


            }
            else if (ruslt == "1")
            {
                txtMessage.Text = "username already used !";
                await timerAsync(ErrBox);
            }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (txtNewName.Text == "")
            {
                txtMessage.Text = "input empry!";
                await timerAsync(ErrBox);
            }
            else
            {
                GetData data = new GetData();
                data.update("password", PostNum, txtNewPass.Text);
                txtMessage.Text = "Changed successfully";
                await timerAsync(ErrBox);

            }
        }
    }

    class Employee
    {
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
    
    
}
