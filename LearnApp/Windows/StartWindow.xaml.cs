using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LearnApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtCode.Text == "0000")
            {
                this.Hide();
                new AdminServicesWindow().Show();
                this.Close();
            }
        }
        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new ClientServicesWindow().ShowDialog();
            this.Close();
        }
    }
}
