using LearnApp.Models;
using LearnApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для AdminServiceRecordWindow.xaml
    /// </summary>
    public partial class AdminServiceRecordWindow : Window
    {
        bool flagNearRecords = false;
        private List<ServiceRecord> serviceRecords;
        Timer myTimer;
        public AdminServiceRecordWindow()
        {
            InitializeComponent();
            SetWindowData();
            LoadDataSet();
            LoadData();
        }

        private void LoadDataSet()
        {
            using (var db = new EntityModel())
            {
                if (flagNearRecords)
                {
                    DateTime oldTime = DateTime.Now, newTime = DateTime.Now.AddDays(1);
                    serviceRecords = db.ServiceRecord.Where(sr => oldTime <= sr.ServiceStart && sr.ServiceStart < newTime).OrderBy(sr=>sr.ServiceStart).ToList();
                    myTimer.Enabled = true;
                }
                else 
                {
                    serviceRecords = db.ServiceRecord.OrderBy(sr => sr.ServiceStart).ToList();
                    myTimer.Enabled = false;
                }
            }
        }

        private void SetWindowData()
        {
            myTimer = new Timer(30000); 
            myTimer.Elapsed += MyTimer_Elapsed;
            myTimer.AutoReset = true;
        }

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoadDataSet();
            LoadData();
        }

        private void LoadData()
        {
            var serviceRecordsList = new List<ServiceRecordObject>();
            using(var db = new EntityModel())
            {
                foreach (var sr in serviceRecords)
                {
                    var s = db.Service.Find(sr.ServiceId);
                    var c = db.Client.Find(sr.ClientId);
                    TimeSpan left;
                    Brush color = Brushes.Black;
                    if (sr.ServiceStart >= DateTime.Now)
                    {
                        left = DateTime.Now - sr.ServiceStart;
                        if(left.TotalMilliseconds < 3600000)
                        {
                            color = Brushes.Red;
                        }
                    }
                    else
                        left = new TimeSpan();

                    var sro = new ServiceRecordObject()
                    {
                        ServiceRecord = sr,
                        Service = s,
                        Client = c,
                        FIO = $"{c.LastName} {c.FirstName} {c.Patronymic}",
                        Time = $"{left.Hours} часов {left.Minutes} минут",
                        TimeColor = color
                };
                    serviceRecordsList.Add(sro);
                }

            }
            recordsGridPanel.ItemsSource = serviceRecordsList;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            new StartWindow().Show();
            this.Close();
        }

        private void ButtonServices_Click(object sender, RoutedEventArgs e)
        {
            new AdminServicesWindow().Show();
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            flagNearRecords = true;
            LoadDataSet();
            LoadData();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            flagNearRecords = false;
            LoadDataSet();
            LoadData();
        }
    }
}
