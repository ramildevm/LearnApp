using LearnApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MakeEditServiceWindow.xaml
    /// </summary>
    public partial class MakeEditServiceWindow : Window
    {
        private Service Service;
        private List<ServicePhoto> addedPhotos { get; set; } = new List<ServicePhoto>();
        public MakeEditServiceWindow(Service service)
        {
            InitializeComponent();
            this.Service = service;
            SetWindowData();
            loadData();
        }
        private void SetWindowData()
        {
            using (var db = new EntityModel())
            {
                var typeList = db.TimeType.ToArray();
                TimeTypeCombBox.ItemsSource = typeList;
                TimeTypeCombBox.DisplayMemberPath = "Type";
            }
        }

        private void loadData()
        {
            if (Service == null)
            {
                labelId.Visibility = ServiceIdTextBox.Visibility = Visibility.Collapsed;
                Service = new Service();
                DataContext = Service;
            }
            else
            {
                TimeTypeCombBox.SelectedIndex = Service.TimeTypeId - 1;
                DataContext = Service;
                imgPhoto.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\" + Service.MainImage));
            }
        }
        private void ButtonLoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Услуги школы\\";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            bool? myResult;
            myResult = op.ShowDialog();
            if (myResult != null && myResult == true)
            {
                string fileName = Guid.NewGuid() + op.SafeFileName;
                string newFilePath = System.IO.Path.Combine(folderpath, fileName);

                File.Copy(op.FileName, newFilePath);

                this.Service.MainImage = "\\Услуги школы\\" + fileName;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
            MessageBox.Show("Изображение загружено!");
        }
        private void btnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EntityModel())
            {
                db.Entry(this.Service).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            new AdminServicesWindow().Show();
            this.Close();
        }
        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EntityModel())
            {                
                if (string.IsNullOrEmpty(ServiceNameTextBox.Text) || string.IsNullOrWhiteSpace(ServiceNameTextBox.Text) ||
                    string.IsNullOrEmpty(DurationTextBox.Text) || string.IsNullOrWhiteSpace(DurationTextBox.Text) ||
                    string.IsNullOrEmpty(CostTextBox.Text) || string.IsNullOrWhiteSpace(CostTextBox.Text) ||
                    string.IsNullOrEmpty(DiscountTextBox.Text) || string.IsNullOrWhiteSpace(DiscountTextBox.Text))
                {                    
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }             
                if (db.Service.FirstOrDefault(s=>s.ServiceName== ServiceNameTextBox.Text.Replace(" ","") )!=null )
                {                    
                    MessageBox.Show("Услуга с данным именем уже существует!");
                    return;
                }                
                try
                {
                    Convert.ToInt32(DurationTextBox.Text);
                    Convert.ToInt32(CostTextBox.Text);
                    Convert.ToInt32(DiscountTextBox.Text);
                }
                catch (FormatException exp)
                {
                    MessageBox.Show("Поля имеют неверный формат!");
                    return;
                }
                switch (TimeTypeCombBox.SelectedIndex)
                {
                    case 0:
                        if (Convert.ToInt32(DurationTextBox.Text) > 14400)
                        {
                            MessageBox.Show("Продолжительность не должно превышать 4 часов!");
                            return;
                        }
                        break;
                    case 1:
                        if (Convert.ToInt32(DurationTextBox.Text) > 240)
                        {
                            MessageBox.Show("Продолжительность не должно превышать 4 часов!");
                            return;
                        }
                        break;
                    case -1:
                        MessageBox.Show("Выберите единицу измерения времени");
                        return;
                }
                if (Convert.ToInt32(DurationTextBox.Text) < 0 || Convert.ToInt32(CostTextBox.Text) < 0|| Convert.ToInt32(DiscountTextBox.Text) < 0)
                {
                    MessageBox.Show("Поля не должны быть отрицательными!");
                    return;
                }
                if (Convert.ToInt32(DiscountTextBox.Text) > 100)
                {
                    MessageBox.Show("Скидка не должна превышать 100%!");
                    return;
                }
                    Service.TimeTypeId = TimeTypeCombBox.SelectedIndex + 1;
                if (Service.Id == 0)
                {
                    var service = db.Service.Add(this.Service);
                    foreach(var sp in addedPhotos)
                    {
                        sp.ServiceId = service.Id;
                        db.ServicePhoto.Add(sp);
                    }
                    db.SaveChanges();
                    MessageBox.Show("Услуга добавлена");
                    new AdminServicesWindow().Show();
                    this.Close();
                }
                else
                {
                    foreach (var sp in addedPhotos)
                    {
                        sp.ServiceId = Service.Id;
                        db.ServicePhoto.Add(sp);
                    }
                    db.Entry(Service).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Услуга обновлена");
                    new AdminServicesWindow().Show();
                    this.Close();
                }
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            new AdminServicesWindow().Show();
            this.Close();
        }
        private void btnImagesService_Click(object sender, RoutedEventArgs e)
        {
            var photosWindow = new ServicePhotosWindow(Service);
            photosWindow.ShowDialog();
            if (photosWindow.AddedPhotos.Count > 0)
            {
                this.addedPhotos = photosWindow.AddedPhotos;
            }
        }
    }
}
