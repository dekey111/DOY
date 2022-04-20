using DOY.dataFiles;
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
    /// Логика взаимодействия для PageKindergarten.xaml
    /// </summary>
    public partial class PageKindergarten : Page
    {
        private static int idKindergarten;
        private static Kindergarten kindObj;
        public PageKindergarten()
        {
            InitializeComponent();
            lbKindergarten.ItemsSource = ConnectHelper.entObj.Kindergarten.ToList();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            kindObj = lbKindergarten.SelectedItem as Kindergarten;
            if (kindObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                idKindergarten = kindObj.ID_Kindergarten;
                WindowEditKindergarten windowEditKindergarten = new WindowEditKindergarten();
                windowEditKindergarten.ShowDialog();
            }
        }

        private void elementDelete()
        {
            kindObj = lbKindergarten.SelectedItem as Kindergarten;
            if (kindObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить:'" + kindObj.Group.Name + "' ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Kindergarten.Remove(kindObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbKindergarten.ItemsSource = ConnectHelper.entObj.Kindergarten.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbKindergarten.ItemsSource = ConnectHelper.entObj.Kindergarten.ToList();
        }
        public static int GetIDKindergarten()
        {
            return idKindergarten;
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbKindergarten.ItemsSource = ConnectHelper.entObj.Kindergarten.ToList();
        }
    }
}
