using LearnApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearnApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MakeServiceRecordWindow.xaml
    /// </summary>
    public partial class MakeServiceRecordWindow : Window
    {
        private int duration = 0;
        public Service Service { get; }
        public MakeServiceRecordWindow(Service service)
        {
            InitializeComponent();
            Service = service;
            LoadData();
        }
        private void LoadData()
        {
            NameTextBox.Text = Service.ServiceName;
            duration = Service.Duration;
            if (Service.TimeTypeId == 1)
            {
                duration = duration / 60;
            }
            DurationTextBox.Text = duration.ToString() + " минут";
            using (var db = new EntityModel())
            {
                var clients = db.Client.ToList();
                ClientCombBox.ItemsSource = clients;
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            new AdminServicesWindow().Show();
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(timeTextBox.Text)|| String.IsNullOrWhiteSpace(DatePicker.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            if (!IsTimeFormat(timeTextBox.Text))
            {
                MessageBox.Show("Неверный формат времени! \nПриведите к формату hh:mm:ss.");
                return;
            }
            if (ClientCombBox.SelectedIndex==-1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            using(var db = new EntityModel())
            {
                var serviceRecord = new ServiceRecord()
                {
                    ServiceId = Service.Id,
                    ClientId = ClientCombBox.SelectedIndex + 1,
                    ServiceStart = DateTime.Parse(DatePicker.Text + " " + timeTextBox.Text),
                    Comment = CommentTextBox.Text
                };
                db.ServiceRecord.Add(serviceRecord);
                db.SaveChanges();
            }
            new AdminServicesWindow().Show();
            this.Close();
        }
        public bool IsTimeFormat(string input)
        {
            string timeRegex = @"^([0-1]?[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
            return Regex.IsMatch(input, timeRegex);
        }
        private void timeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(IsTimeFormat((sender as TextBox).Text))
            {
                var start = TimeSpan.Parse((sender as TextBox).Text);
                var end = start.Add(TimeSpan.FromMinutes(duration));

                string timeString = end.ToString("hh\\:mm\\:ss");
                txtDuracity.Text = "Время окончания: " + timeString;
            }            
            else
                txtDuracity.Text = "";
        }
    }
}
