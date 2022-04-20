using DOY.dataFiles;
using DOY.Pages.View;
using DOY.Pages.View.MoreGroups;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DOY.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для WindowEditChildre.xaml
    /// </summary>
    public partial class WindowEditChildre : Window
    {
        private int idChild;
        private bool editChild;
        
        private string imagePath; 
        public WindowEditChildre()
        {
            InitializeComponent();
            editChild = WindowMoreInfoForGroup.GetChilndrenInGroup();

            if (editChild == false)
                idChild = PageChildren.GetIdChildren();
            else
            {
                idChild = WindowMoreInfoForGroup.GetIdChilndrenInGroup();
            }

            var childre = ConnectHelper.entObj.Children.FirstOrDefault(x => x.ID_Children == idChild);
            
            txbSurname.Text = childre.Surname;
            txbMiddle.Text = childre.MiddleName;
            txbName.Text = childre.FirstName;
            dpDateOfBirth.SelectedDate = childre.DateOfBirth;

            byte[] imageData = childre.Image;
            if (imageData == null)
                return;
            else
            {
                var image = new BitmapImage();

                using (var mem = new MemoryStream(imageData))
                {

                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();

                }
                image.Freeze();
                iImageChildren.Source = image;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbSurname.Text.Length == 0
                && txbName.Text.Length == 0
                && txbMiddle.Text.Length == 0
                && dpDateOfBirth.SelectedDate == null)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbSurname.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Фамилия'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbName.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Имя'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbMiddle.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Отчество'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (dpDateOfBirth.SelectedDate == null)
                MessageBox.Show("Заполните поле 'Дата рождения'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (dpDateOfBirth.SelectedDate >= DateTime.Now)
                MessageBox.Show("Поле 'Дата рождения' введено не корректно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                if(imagePath == null)
                {
                    Children childObj = ConnectHelper.entObj.Children.FirstOrDefault(x => x.ID_Children == idChild);
                    childObj.Surname = txbSurname.Text;
                    childObj.MiddleName = txbMiddle.Text;
                    childObj.FirstName = txbName.Text;
                    childObj.DateOfBirth = dpDateOfBirth.SelectedDate;
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Children childObj = ConnectHelper.entObj.Children.FirstOrDefault(x => x.ID_Children == idChild);
                    childObj.Surname = txbSurname.Text;
                    childObj.MiddleName = txbMiddle.Text;
                    childObj.FirstName = txbName.Text;
                    childObj.DateOfBirth = dpDateOfBirth.SelectedDate;
                    childObj.Image = File.ReadAllBytes(imagePath);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSelectPhotoChildren_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();
            if(result == true)
            {
                iImageChildren.Source = new BitmapImage(new Uri(dlg.FileName));
                imagePath = dlg.FileName;
                lPhotoPathChildren.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            idChild = 0;
            editChild = false;
        }
    }
}
