using LearnApp.Models;
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
using System.Windows.Shapes;

namespace LearnApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminServicesWindow.xaml
    /// </summary>
    public partial class AdminServicesWindow : Window
    {
        private List<ServiceObject> serviceObjectsList;
        private List<Service> serviceList = new List<Service>();
        private string searchFilter = "";
        private int discountMark = 6;
        int sortMark = 0;
        string[] sorts = new string[] {
            "Все",
            "Цена (по уб.)",
            "Цена (по воз.)"};

        public AdminServicesWindow()
        {
            InitializeComponent();
            comboBoxSort.ItemsSource = sorts;
            LoadData();
            LoadServices();
        }

        private void LoadServices()
        {
            serviceObjectsList = new List<ServiceObject>();
            using (var db = new EntityModel())
            {
                string folderPath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\";
                foreach (var service in serviceList)
                {
                    double price = service.Cost * (double)(1 - (double)service.Discount / 100);
                    int time = service.Duration;
                    if (service.TimeTypeId == 1)
                        time = time / 60;
                    string discountText = "", panelColor = "#fff";
                    Visibility visibility = Visibility.Visible;
                    if (service.Discount != 0)
                    {
                        discountText = $"*скидка {service.Discount}%";
                        panelColor = "#E7FABF";
                    }
                    else
                        visibility = Visibility.Collapsed;
                    var serviceObject = new ServiceObject()
                    {
                        PanelColor = panelColor,
                        Service = service,
                        PriceText = $" {price} рублей за {time} минут",
                        DiscountText = discountText,
                        OldPriceVisibility = visibility,
                        MainImage = folderPath + service.MainImage
                    };
                    Console.WriteLine(folderPath + service.MainImage);
                    serviceObjectsList.Add(serviceObject);
                    db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                }
            }
            servicesPanel.ItemsSource = serviceObjectsList;
        }

        private void LoadData()
        {
            using (var db = new EntityModel())
            {
                serviceList.Clear();
                serviceList = db.Service.ToList();
                if (searchFilter != "")
                    serviceList = serviceList.Where(s => s.ServiceName.Contains(searchFilter)).ToList();

                switch (discountMark)
                {
                    case 0:
                        serviceList = serviceList.Where(s => s.Discount < 5).ToList();
                        break;
                    case 1:
                        serviceList = serviceList.Where(s => s.Discount >= 5 && s.Discount < 15).ToList();
                        break;
                    case 2:
                        serviceList = serviceList.Where(s => s.Discount >= 15 && s.Discount < 30).ToList();
                        break;
                    case 3:
                        serviceList = serviceList.Where(s => s.Discount >= 30 && s.Discount < 70).ToList();
                        break;
                    case 4:
                        serviceList = serviceList.Where(s => s.Discount >= 70 && s.Discount <= 100).ToList();
                        break;
                }
                switch (sortMark)
                {
                    case 1:
                        serviceList = serviceList.OrderByDescending(s => s.Cost * (double)(1 - (double)s.Discount / 100)).ToList();
                        break;
                    case 2:
                        serviceList = serviceList.OrderBy(s => s.Cost * (double)(1 - (double)s.Discount / 100)).ToList();
                        break;
                }
                txtCount.Text = $"Выведено услуг: {serviceList.Count()} из {db.Service.Count()}.";
            }
        }


        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            new StartWindow().Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchFilter = (sender as TextBox).Text;
            LoadData();
            LoadServices();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var service = ((((sender as Button).Parent as StackPanel).Parent as Grid).DataContext as ServiceObject).Service;
            using (var db = new EntityModel())
            {
                foreach(var servicePhoto in db.ServicePhoto.Where(sp=>sp.ServiceId == service.Id).ToList())
                {
                    db.Entry(servicePhoto).State = System.Data.Entity.EntityState.Deleted;
                }
                if(db.ServiceRecord.FirstOrDefault(sr=>sr.ServiceId == service.Id) != null)
                {
                    MessageBox.Show("Невозможно удалить услугу. В базе данных есть записи ссылающие на услугу");
                    return;
                }
                db.Entry(service).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            LoadData();
            LoadServices();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var service = ((((sender as Button).Parent as StackPanel).Parent as Grid).DataContext as ServiceObject).Service;
            new MakeEditServiceWindow(service).Show();
            this.Close();
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            discountMark = (sender as ComboBox).SelectedIndex;
            LoadData();
            LoadServices();
        }

        private void comboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortMark = (sender as ComboBox).SelectedIndex;
            LoadData();
            LoadServices();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new MakeEditServiceWindow(null).Show();
            this.Close();
        }
        private void ButtonRecords_Click(object sender, RoutedEventArgs e)
        {
            new AdminServiceRecordWindow().Show();
            this.Close();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new MakeServiceRecordWindow((sender as MenuItem).Tag as Service).Show();
            this.Close();
        }
    }
}
