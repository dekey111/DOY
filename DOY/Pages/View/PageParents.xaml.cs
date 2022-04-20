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
    /// Логика взаимодействия для PageParents.xaml
    /// </summary>
    public partial class PageParents : Page
    {
        private static int idParent;
        private static Parent parentObj;
        public PageParents()
        {
            InitializeComponent();
            lbParent.ItemsSource = ConnectHelper.entObj.Parent.ToList();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            parentObj = lbParent.SelectedItem as Parent;
            if (parentObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                idParent = parentObj.ID_Parent;
                WindowEditParent windowEditParent = new WindowEditParent();
                windowEditParent.ShowDialog();
                lbParent.ItemsSource = ConnectHelper.entObj.Parent.ToList();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public static int GetIDParent()
        {
            return idParent;
        }

        private void miMore_Click(object sender, RoutedEventArgs e)
        {
            MoreInfo();
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }
        private void elementDelete()
        {
            parentObj = lbParent.SelectedItem as Parent;
            if (parentObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить:'" + parentObj.FIO + "' ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Parent.Remove(parentObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbParent.ItemsSource = ConnectHelper.entObj.Parent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void refresh()
        {
            lbParent.ItemsSource = ConnectHelper.entObj.Parent.ToList();
        }

        private void lbParent_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MoreInfo();
        }
        private void MoreInfo()
        {
            parentObj = lbParent.SelectedItem as Parent;
            if (parentObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                List<string> children = new List<string>();
                string nameChildren = "";


                var parent = ConnectHelper.entObj.ParentChildren.Where(x => x.ID_Parent == parentObj.ID_Parent);

                foreach (var child in parent)
                {
                    children.Add(child.FIOChild);
                }
                for (int i = 0; i < children.Count; i++)
                {
                    nameChildren += children[i] + "\n";
                }

                DateTime dateOfBirth = (DateTime)parentObj.DateOfBirth;

                MessageBox.Show("ФИО: " + parentObj.FIO + "\n" +
                    "Дата рождения: " + dateOfBirth.ToString("D") + "\n" +
                    "Возвраст: " + parentObj.Age + "\n" +
                    "Номер телефона: " + parentObj.Phone + "\n" +
                    "Ребенок: " + nameChildren + "\n"
                    , "Подробная информация", MessageBoxButton.OK);
            }
        }
    }
}
