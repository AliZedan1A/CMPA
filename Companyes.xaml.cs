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
    /// Interaction logic for Companyes.xaml
    /// </summary>
    public partial class Companyes : Window
    {
        public Companyes()
        {
            InitializeComponent();
        }
        public string PostNum { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DashWIn dashWIn = new DashWIn();
            dashWIn.PostNum = PostNum;
            dashWIn.WindowState = WindowState;

            dashWIn.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            GetData data = new GetData();

            var companys = data.GetTabels();
            foreach (var company in companys)
            {
               dataGrid.Items.Add(company);
            }

            //Moveing Form
            try
            {
                this.MouseLeftButtonDown += delegate { DragMove(); };
            }
            catch (Exception) { }
        }

        class Root
        {
            public string? Name { get; set; }
            public string? WorkerLength { get; set; }

        }

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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1000)
            {
                Width = 1000;
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
