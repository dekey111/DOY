using DOY.dataFiles;
using DOY.Pages.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

namespace DOY.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для WindowEditChildre.xaml
    /// </summary>
    public partial class WindowEditChildre : Window
    {
        private int idChild = PageChildren.GetIdChildren();
        public WindowEditChildre()
        {
            InitializeComponent();
            var childre = ConnectHelper.entObj.Children.FirstOrDefault(x => x.ID_Children == idChild);

            txbSurname.Text = childre.Surname;
            txbMiddle.Text = childre.MiddleName;
            txbName.Text = childre.FirstName;
            dpDateOfBirth.SelectedDate = childre.DateOfBirth;
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
                IEnumerable<Children> childrens = ConnectHelper.entObj.Children.Where(x => x.ID_Children == idChild).AsEnumerable().
             Select(x =>
             {
                 x.Surname = txbSurname.Text;
                 x.MiddleName = txbMiddle.Text;
                 x.FirstName = txbName.Text;
                 x.DateOfBirth = dpDateOfBirth.SelectedDate;
                 return x;
             });
                foreach (Children child in childrens)
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
