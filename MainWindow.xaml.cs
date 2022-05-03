using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CMA_Libary;


namespace CMPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {




            GetData data = new GetData();
            var status = data.getUser(username.Text, password.Password);
            if (status == "0")
            {
                txtMessage.Text = "Username Or Password Not Found";
                await timerAsync(ErrBox);
            }
            else if (status == "1")
            {
                txtMessage.Text = "Enter All Inputs";
                await timerAsync(ErrBox);
            }
            else
            {
                BitmapImage bitimg = new BitmapImage();

                bitimg.BeginInit();
               var path=   Assembly.GetExecutingAssembly().Location;

                bitimg.UriSource = new Uri(@$"{path}\..\..\..\..\Imgs\Login\polar-lock.png", UriKind.RelativeOrAbsolute);

                bitimg.EndInit();



                Image img = new Image();

                img.Stretch = Stretch.Fill;

                img.Source = bitimg;



                // Set Button.Content
                btnimg.Background = new ImageBrush(bitimg);



                for (int i = 3; i > 0; i--)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    lbl.Content = $"You will be transferred with in 3 seconds :\n \t\t{i}'s";

                }
                await Task.Delay(TimeSpan.FromSeconds(1));




                DashWIn dw = new DashWIn();




                dw.PostNum = status;

                dw.Show();

                Close();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.MouseLeftButtonDown += delegate { DragMove(); };
            }catch (Exception) { }
            

        }





        //NoteBox 
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
                            border.Margin = new Thickness(border.Margin.Left - 5, border.Margin.Top, border.Margin.Right , border.Margin.Bottom);
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
