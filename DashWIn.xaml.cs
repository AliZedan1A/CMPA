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
using CMA_Libary;

namespace CMPA
{
    /// <summary>
    /// Interaction logic for DashWIn.xaml
    /// </summary>
    public partial class DashWIn : Window
    {
        public DashWIn()
        {
            InitializeComponent();
        }
        public string PostNum { get; set; }
        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData data = new GetData();
            var role = data.GetRole(PostNum);


            LBL_ROLE.Content = LBL_ROLE.Content + " " + role;
            var UserName = data.getUserName(PostNum);
            LBL_WLC.Content = LBL_WLC.Content + " " + UserName;

            //Moving Window
            try
            {
                this.MouseLeftButtonDown += delegate { DragMove(); };
            }
            catch (Exception) { }
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            GetData getdate = new GetData();
            var role = getdate.GetRole(PostNum);
            if(role == "User")
            {
                MessageBox.Show("you havn't permtion");

            }
            else
            {
                AddComp ac = new AddComp();
                ac.PostNum = PostNum;
                ac.WindowState = this.WindowState;
                this.Close();
                ac.Show();


            }


        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1200)
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1200)
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

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ControlP cp = new ControlP();
            cp.PostNum = PostNum;
            cp.WindowState = this.WindowState;


            Visibility = Visibility.Collapsed;
            cp.ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Companyes comps = new Companyes();
            comps.PostNum = PostNum;
            comps.WindowState = this.WindowState;
            
            comps.Show();
            Close();
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

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            GetData getdate = new GetData();
            var role = getdate.GetRole(PostNum);
            if (role == "User")
            {
                MessageBox.Show("you havn't permtion");

            }
            else
            {
                UserControl userControl = new UserControl();
                userControl.WindowState = WindowState;


                userControl.Show();
                userControl.PostNum = PostNum;

                Close();
            }
        }
    }
}
