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
    /// Логика взаимодействия для WindowEditParent.xaml
    /// </summary>
    public partial class WindowEditParent : Window
    {
        private int idParent = PageParents.GetIDParent();
        private string imagePath;
        public WindowEditParent()
        {
            InitializeComponent();
            var parent = ConnectHelper.entObj.Parent.FirstOrDefault(x => x.ID_Parent == idParent);

            txbSurnameParent.Text = parent.Surname;
            txbMiddleParent.Text = parent.MiddleName;
            txbNameParent.Text = parent.FirstName;
            dpDateOfBirthParent.SelectedDate = parent.DateOfBirth;
            txbPhoneParent.Text = parent.Phone;

            byte[] imageData = parent.Image;
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
                iImageParent.Source = image;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbSurnameParent.Text.Length == 0
&& txbNameParent.Text.Length == 0
&& txbMiddleParent.Text.Length == 0
&& dpDateOfBirthParent.SelectedDate == null
&& txbPhoneParent.Text.Length == 0)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbSurnameParent.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Фамилия'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbNameParent.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Имя'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbMiddleParent.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Отчетсво'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (dpDateOfBirthParent.SelectedDate == null)
                MessageBox.Show("Заполните поле 'Дата рождения'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbPhoneParent.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Номер телефона'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (dpDateOfBirthParent.SelectedDate >= DateTime.Parse("01.01.2005"))
                MessageBox.Show("Поле 'Дата рождения' введено не корректно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (imagePath == null)
                {
                    Parent ParentObj = ConnectHelper.entObj.Parent.FirstOrDefault(x => x.ID_Parent == idParent);
                    ParentObj.Surname = txbSurnameParent.Text;
                    ParentObj.MiddleName = txbMiddleParent.Text;
                    ParentObj.FirstName = txbNameParent.Text;
                    ParentObj.Phone = txbPhoneParent.Text;
                    ParentObj.DateOfBirth = dpDateOfBirthParent.SelectedDate;
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Parent ParentObj = ConnectHelper.entObj.Parent.FirstOrDefault(x => x.ID_Parent == idParent);
                    ParentObj.Surname = txbSurnameParent.Text;
                    ParentObj.MiddleName = txbMiddleParent.Text;
                    ParentObj.FirstName = txbNameParent.Text;
                    ParentObj.DateOfBirth = dpDateOfBirthParent.SelectedDate;
                    ParentObj.Phone = txbPhoneParent.Text;
                    ParentObj.Image = File.ReadAllBytes(imagePath);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSelectPhotoParent_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                iImageParent.Source = new BitmapImage(new Uri(dlg.FileName));
                imagePath = dlg.FileName;
                lPhotoPathParent.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }
    }
}
