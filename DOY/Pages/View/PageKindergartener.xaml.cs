using DOY.dataFiles;
using DOY.Pages.Add;
using DOY.Pages.Edit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DOY.Pages.View
{
    /// <summary>
    /// Логика взаимодействия для PageKindergartener.xaml
    /// </summary>
    public partial class PageKindergartener : Page
    {
        private static int idKindergartener;
        private static Kindergartener kindObj;
        public PageKindergartener()
        {
            InitializeComponent();
            lbKindregartener.ItemsSource = ConnectHelper.entObj.Kindergartener.ToList();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elemetDel();
        }

        public static int GetIDKIndergartener()
        {
            return idKindergartener;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            kindObj = lbKindregartener.SelectedItem as Kindergartener;
            if (kindObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                idKindergartener = kindObj.ID_Kindergartener;
                WindowEditKindergartener windowEditKindergartener = new WindowEditKindergartener();
                windowEditKindergartener.ShowDialog();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddKindergartener windowAddKindergartener = new WindowAddKindergartener();
            windowAddKindergartener.ShowDialog();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbKindregartener.ItemsSource = ConnectHelper.entObj.Kindergartener.ToList();
        }

        private void miMore_Click(object sender, RoutedEventArgs e)
        {
            MoreInfo();
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbKindregartener.ItemsSource = ConnectHelper.entObj.Kindergartener.ToList();

        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elemetDel();
        }

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddKindergartener windowAddKindergartener = new WindowAddKindergartener();
            windowAddKindergartener.ShowDialog();
        }
        private void elemetDel()
        {
            kindObj = lbKindregartener.SelectedItem as Kindergartener;
            if (kindObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить:'" + kindObj.FIO + "' ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Kindergartener.Remove(kindObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbKindregartener.ItemsSource = ConnectHelper.entObj.Children.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MoreInfo()
        {
            kindObj = lbKindregartener.SelectedItem as Kindergartener;
            if (kindObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                DateTime dateOfBirth = (DateTime)kindObj.DateOfBirth;
                DateTime dateOfEmployment = (DateTime)kindObj.DateOfEmployment;

                MessageBox.Show("ФИО: " + kindObj.FIO + "\n" +
                    "Дата рождения: " + dateOfBirth.ToString("D") + "\n" +
                    "Возвраст: " + kindObj.Age + "\n" +
                    "Номер телефона: " + kindObj.Phone + "\n" +
                    "Дата устройства на работу: " + dateOfEmployment.ToString("D") + "\n" +
                    "Опыт работы " + kindObj.WorkExp + " год(а)(лет)\n" + 
                    "Должность " + kindObj.Position.Name + "\n" 
                    , "Подробная информация", MessageBoxButton.OK);
            }
        }
        private void lbKindregartener_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MoreInfo();
        }
    }
}
