using DOY.dataFiles;
using DOY.Pages.View;
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

namespace DOY.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для WindowEditKindergartener.xaml
    /// </summary>
    public partial class WindowEditKindergartener : Window
    {
        private int idKind = PageKindergartener.GetIDKIndergartener();
        public WindowEditKindergartener()
        {
            InitializeComponent();
            var kindergarten = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x => x.ID_Kindergartener == idKind);

            txbSurname.Text = kindergarten.Surname;
            txbMiddle.Text = kindergarten.MiddleName;
            txbName.Text = kindergarten.FirstName;
            dpDateOfBirth.SelectedDate = kindergarten.DateOfBirth;
            txbPhone.Text = kindergarten.Phone;
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
                IEnumerable<Kindergartener> kindergartenObj = ConnectHelper.entObj.Kindergartener.Where(x => x.ID_Kindergartener == idKind).AsEnumerable().
             Select(x =>
             {
                 x.Surname = txbSurname.Text;
                 x.MiddleName = txbMiddle.Text;
                 x.FirstName = txbName.Text;
                 x.DateOfBirth = dpDateOfBirth.SelectedDate;
                 x.Phone = txbPhone.Text;
                 return x;
             });
                foreach (Kindergartener kindObj in kindergartenObj)
                {
                    ConnectHelper.entObj.Entry(kindObj).State = System.Data.Entity.EntityState.Modified;
                }

                try
                {
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
