using DOY.dataFiles;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DOY.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для WindowAddKindergartener.xaml
    /// </summary>
    public partial class WindowAddKindergartener : Window
    {
        private static string imagePath;
        public WindowAddKindergartener()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var kindergartenerObj = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x=> x.Surname.Contains(txbSurname.Text) && x.FirstName.Contains(txbName.Text) && 
            x.MiddleName.Contains(txbMiddle.Text) && x.DateOfBirth.ToString().Contains(dpDateOfBirth.SelectedDate.ToString()));

            if (txbSurname.Text.Length == 0
                && txbName.Text.Length == 0
                && txbMiddle.Text.Length == 0
                && dpDateOfBirth.SelectedDate == null
                && txbPhone.Text.Length == 0)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbSurname.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Фамилия'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbName.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Имя'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbMiddle.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Отчетсво'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (dpDateOfBirth.SelectedDate == null)
                MessageBox.Show("Заполните поле 'Дата рождения'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbPhone.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Номер телефона'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (dpDateOfBirth.SelectedDate >= DateTime.Parse("01.01.2005"))
                MessageBox.Show("Поле 'Дата рождения' введено не корректно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (kindergartenerObj != null)
                MessageBox.Show("Такой работник уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (imagePath == null)
                {
                    Kindergartener kindergartener = new Kindergartener()
                    {
                        Surname = txbSurname.Text,
                        FirstName = txbName.Text,
                        MiddleName = txbMiddle.Text,
                        Image = null,
                        DateOfBirth = dpDateOfBirth.SelectedDate,
                        Phone = txbPhone.Text
                    };
                    ConnectHelper.entObj.Kindergartener.Add(kindergartener);
                    ConnectHelper.entObj.SaveChanges();
                }
                else
                {
                    Kindergartener kindergartener = new Kindergartener()
                    {
                        Surname = txbSurname.Text,
                        FirstName = txbName.Text,
                        MiddleName = txbMiddle.Text,
                        DateOfBirth = dpDateOfBirth.SelectedDate,
                        Phone = txbPhone.Text,
                        Image = File.ReadAllBytes(imagePath)
                    };
                    ConnectHelper.entObj.Kindergartener.Add(kindergartener);
                    ConnectHelper.entObj.SaveChanges();
                }
            }
        } 

        private void btnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

                iImage.Source = new BitmapImage(new Uri(dlg.FileName));
                imagePath = dlg.FileName;
                lPhotoPath.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }
    }
}
