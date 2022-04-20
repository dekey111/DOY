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
    /// Логика взаимодействия для WindowEditParent.xaml
    /// </summary>
    public partial class WindowEditParent : Window
    {
        private int idParent = PageParents.GetIDParent();
        public WindowEditParent()
        {
            InitializeComponent();
            var parent = ConnectHelper.entObj.Parent.FirstOrDefault(x => x.ID_Parent == idParent);

            txbSurnameParent.Text = parent.Surname;
            txbMiddleParent.Text = parent.MiddleName;
            txbNameParent.Text = parent.FirstName;
            dpDateOfBirthParent.SelectedDate = parent.DateOfBirth;
            txbPhoneParent.Text = parent.Phone;
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
                IEnumerable<Parent> parentObj = ConnectHelper.entObj.Parent.Where(x => x.ID_Parent == idParent).AsEnumerable().
             Select(x =>
             {
                 x.Surname = txbSurnameParent.Text;
                 x.MiddleName = txbMiddleParent.Text;
                 x.FirstName = txbNameParent.Text;
                 x.DateOfBirth = dpDateOfBirthParent.SelectedDate;
                 x.Phone = txbPhoneParent.Text;
                 return x;
             });
                foreach (Parent child in parentObj)
                {
                    ConnectHelper.entObj.Entry(child).State = System.Data.Entity.EntityState.Modified;
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
