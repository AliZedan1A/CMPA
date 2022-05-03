using CMA_Libary;
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
//using CMA_Libary;

namespace CMPA
{
    /// <summary>
    /// Interaction logic for AddComp.xaml
    /// </summary>
    public partial class AddComp : Window
    {
        public AddComp()
        {
            InitializeComponent();
        }

        public string PostNum { get; set; }

        //Colse the form
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DashWIn dashWIn = new DashWIn();
            dashWIn.PostNum = PostNum;
            dashWIn.WindowState = WindowState;
            dashWIn.Show();
            dashWIn.Focus();
            this.Close();
        }

        //Max-Min Form
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

        //Form Down
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



            //Form moveing
            try
            {
                this.MouseLeftButtonDown += delegate { DragMove(); };
            }
            catch (Exception)
            {
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

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (textboxC.Text == String.Empty)
            {
                txtMessage.Text = "Fill The Input!";
                await timerAsync(ErrBox);
            }
            else
            {

                GetData data = new GetData();
                var t = data.GetTabels();
                int statusC = 1;

                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].Name == textboxC.Text)
                    {
                        statusC = 0;
                    }

                }
                if (statusC == 1)
                {
                    BTNcreat.Visibility = Visibility.Hidden;

                    data.CreatCompany(textboxC.Text);
                    lblName.Content = "the Company created! ";
                    var g = " you well send in 3 Seconds to Controler";
                    for (int i = 3; i > 0; i--)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        lblName.Content = g + $"\n \t\t{i}'s";

                    }
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    ControlP controlP = new ControlP();
                    controlP.PostNum = PostNum;
                    controlP.CompanyName = textboxC.Text;
                    controlP.WindowState = WindowState;

                    controlP.Show();
                    this.Close();

                }
                else
                {
                    txtMessage.Text = "Company Name Already Exist";
                    await timerAsync(ErrBox);

                }

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
                txtMessage.Text = e.Message;
                await timerAsync(ErrBox);
            }
        }
    }
}
