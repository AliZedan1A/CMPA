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
    /// Interaction logic for ControlP.xaml
    /// </summary>
    public partial class ControlP : Window
    {
        public ControlP()
        {
            InitializeComponent();
        }
        public string PostNum { get; set; }
        public string CompanyName { get; set; }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DashWIn dashWIn = new DashWIn();
            dashWIn.PostNum = PostNum;
            dashWIn.WindowState = WindowState;

            dashWIn.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            GetData g = new GetData();
            if (CompanyName == null)
            {

            }
            else
            {
                txtCompName.Text = CompanyName;
            }
            if (txtCompName.Text == String.Empty)
            {

            }
            else
            {
                var a = g.GetDataFromCompany(txtCompName.Text);
                foreach (var item in a)
                {
                    dataGrid.Items.Add(item);
                }


            }
            //Form moveing
            try
            {
                this.MouseLeftButtonDown += delegate { DragMove(); };
            }
            catch (Exception) { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            //Max-Min Window
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var companyes = data.GetTabels();
            var status = 1;
            for (int i = 0; i < companyes.Count; i++)
            {
                if (txtCompName.Text == companyes[i].Name)
                {
                    status = 0;
                }
            }
            if (status == 0)
            {
                data.addTocompany(txtCompName.Text, textboxC.Text, textboxC1.Text, textboxC2.Text, textboxC3.Text, textboxC4.Text);
                //هنا ينجح الاضافة
                txtMessage.Text = "Addition succeeded";
                await timerAsync(ErrBox);

            }
            else
            {
                txtMessage.Text = "Company name not found";
                await timerAsync(ErrBox);
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GetData g = new GetData();
            int status = 1;
            var companyes = g.GetTabels();
            for (int i = 0; i < companyes.Count; i++)
            {
                if (txtCompName.Text == companyes[i].Name)
                {
                    status = 0;
                }
            }
            if (status == 0)
            {
                dataGrid.Items.Clear();

                var a = g.GetDataFromCompany(txtCompName.Text);
                if (a.Count == 0)
                {
                    txtMessage.Text = "This Company Is Empty of Employees";
                    await timerAsync(ErrBox);
                }
                else
                {
                    foreach (var item in a)
                    {
                        dataGrid.Items.Add(item);
                    }
                }

            }
            else
            {
                txtMessage.Text = "Company name not found";
                await timerAsync(ErrBox);
            }




        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            textboxC.Text = String.Empty;
           textboxC1.Text = String.Empty;
            textboxC2.Text = String.Empty;
            textboxC3.Text = String.Empty;
            textboxC4.Text = String.Empty;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        //Erorr Box
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
                txtMessage.Text = e.Message;
                await timerAsync(ErrBox);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Close the panel
            optionPanel.Visibility = Visibility.Collapsed;
        }

        private async void btnDeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var a = data.DeleteCompany(txtCompName.Text);
            if (a == "none")
            {
                txtMessage.Text = "Company name not found";
                await timerAsync(ErrBox);

            }
            else
            {
                txtMessage.Text = "Deleted successfully";
                await timerAsync(ErrBox);

            }
        }

        private async void btnDeleteAllEmp_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var a = data.DeleteAllWorker(txtCompName.Text);
            if (a == "none")
            {
                txtMessage.Text = "Company name not found";
                await timerAsync(ErrBox);

            }
            else
            {
                txtMessage.Text = "All company workers have been removed";
                await timerAsync(ErrBox);

            }
        }

        private async void btnChangeName_Click(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var a = data.RenameCompany(txtCompName.Text, txtNewName.Text);
            if (a == "none")
            {
                txtMessage.Text = "Company name not found";
                await timerAsync(ErrBox);

            }
            else
            {
                txtMessage.Text = "Name changed successfully";
                await timerAsync(ErrBox);

            }

        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            //Close the panel
            optionPanel.Visibility = Visibility.Collapsed;
        }

        private async void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            GetData getdate = new GetData();
            var role = getdate.GetRole(PostNum);
            if (role == "User")
            {
                txtMessage.Text = "you havn't permtion";
                await timerAsync(ErrBox);


            }
            else
            {

                optionPanel.Visibility = Visibility.Visible;
            }
        }

        private void win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1400)
            {
                Width = 1200;
                e.Handled = true;
            }
            if (e.NewSize.Height < 700)
            {
                Height = 700;
                e.Handled = true;
            }
        }
    }
}
