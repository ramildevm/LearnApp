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
    /// Логика взаимодействия для ServicePhotosWindow.xaml
    /// </summary>
    public partial class ServicePhotosWindow : Window
    {
        public List<ServicePhoto> AddedPhotos = new List<ServicePhoto>();
        public ServicePhotosWindow(Service service)
        {
            InitializeComponent();
            this.Service = service;
            LoadData();
        }
        private void LoadData()
        {
            if (Service.Id != 0)
            {
                string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Фото услуг\\";
                using (var db = new EntityModel())
                {
                    foreach(var photo in db.ServicePhoto.Where(sp => sp.ServiceId == Service.Id).ToList())
                    {
                        makeServicePhotoPanel(folderpath, photo);
                    }
                }
            }
        }
        private void makeServicePhotoPanel(string folderpath, ServicePhoto photo)
        {
            var imagePanel = new Grid();
            var img = new Image() { Source = new BitmapImage(new Uri(folderpath + photo.PhotoPath)),Margin=new Thickness(10) };
            var delButton = new Button() { Content = "Удалить", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(10) };
            delButton.Tag = photo;
            delButton.Click += DelButton_Click;
            imagePanel.Children.Add(img);
            imagePanel.Children.Add(delButton);
            imagesPanel.Children.Add(imagePanel);
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var sp = ((sender as Button).Tag) as ServicePhoto;
            if (AddedPhotos.FirstOrDefault(s => s.ServiceId == sp.ServiceId) != null)
            {
                AddedPhotos.Remove(sp);
                imagesPanel.Children.Remove((sender as Button).Parent as Grid);
            }
            else
            {
                using (var db = new EntityModel())
                {
                    db.Entry(sp).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                imagesPanel.Children.Remove((sender as Button).Parent as Grid);
            }
        }
        public Service Service { get; }
        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Фото услуг\\";
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
                var sp = new ServicePhoto() { PhotoPath = fileName, ServiceId = Service.Id };
                AddedPhotos.Add(sp);
                makeServicePhotoPanel(folderpath, sp);
            }
            MessageBox.Show("Изображение загружено!");
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
