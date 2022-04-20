using DOY.dataFiles;
using DOY.Pages.View;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DOY.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для WindowEditKindergartener.xaml
    /// </summary>
    public partial class WindowEditKindergartener : Window
    {
        private int idKind = PageKindergartener.GetIDKIndergartener();
        private string imagePath;
        public WindowEditKindergartener()
        {
            InitializeComponent();
            var kindergarten = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x => x.ID_Kindergartener == idKind);

            txbSurname.Text = kindergarten.Surname;
            txbMiddle.Text = kindergarten.MiddleName;
            txbName.Text = kindergarten.FirstName;
            dpDateOfBirth.SelectedDate = kindergarten.DateOfBirth;
            txbPhone.Text = kindergarten.Phone;

            byte[] imageData = kindergarten.Image;
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
                iImageKind.Source = image;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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
            else
            {
                if (imagePath == null)
                {
                    Kindergartener KindObj = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x => x.ID_Kindergartener == idKind);
                    KindObj.Surname = txbSurname.Text;
                    KindObj.MiddleName = txbMiddle.Text;
                    KindObj.FirstName = txbName.Text;
                    KindObj.Phone = txbPhone.Text;
                    KindObj.DateOfBirth = dpDateOfBirth.SelectedDate;
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Kindergartener KindObj = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x => x.ID_Kindergartener == idKind);
                    KindObj.Surname = txbSurname.Text;
                    KindObj.MiddleName = txbMiddle.Text;
                    KindObj.FirstName = txbName.Text;
                    KindObj.Phone = txbPhone.Text;
                    KindObj.DateOfBirth = dpDateOfBirth.SelectedDate;
                    KindObj.Image = File.ReadAllBytes(imagePath);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSelectPhotoKind_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                iImageKind.Source = new BitmapImage(new Uri(dlg.FileName));
                imagePath = dlg.FileName;
                lPhotoPathKind.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }
    }
}
