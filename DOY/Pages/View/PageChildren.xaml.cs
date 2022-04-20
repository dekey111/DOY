using DOY.dataFiles;
using DOY.Pages.Add;
using DOY.Pages.Edit;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DOY.Pages.View
{
    /// <summary>
    /// Логика взаимодействия для PageChildren.xaml
    /// </summary>
    public partial class PageChildren : Page
    {
        private static int idChild;
        private static Children childObj;
        public PageChildren()
        {
            InitializeComponent();
            cmbFilt.Items.Add("По фамилии");
            cmbFilt.Items.Add("По возврасту");
            cmbFilt.Items.Add("По дате рождению");
            lbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
        }

        private void elementDelete()
        {
            childObj = lbChild.SelectedItem as Children;
            if (childObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить:'" + childObj.FIO + "' ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Children.Remove(childObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            childObj = lbChild.SelectedItem as Children;
            if (childObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                idChild = childObj.ID_Children;
                WindowEditChildre windowEditChildre = new WindowEditChildre();
                windowEditChildre.ShowDialog();
                lbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
            }

        }
        private void Refresh()
        {
            dpDateStart.SelectedDate = null;

            cmbFilt.SelectedIndex = -1;
            cmbFilt.Text = null;

            txbSearch.Text = null;
            lbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        public static int GetIdChildren()
        {
            return idChild;
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            cmbFilt.SelectedIndex = -1;
            cmbFilt.Text = null;

            dpDateStart.SelectedDate = null;
            lbChild.ItemsSource = ConnectHelper.entObj.Children.Where(x => x.FIO.Contains(txbSearch.Text) || x.DateOfBirth.ToString().Contains(txbSearch.Text) ||
            x.Age.ToString().Contains(txbSearch.Text)).ToList();
        }

        private void cmbFilt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbFilt.SelectedIndex == 0)
            {
                txbSearch.Text = null;
                dpDateStart.SelectedDate = null;
                lbChild.ItemsSource = ConnectHelper.entObj.Children.OrderBy(x => x.FIO).ToList();
            }

            else if (cmbFilt.SelectedIndex == 1)
            {
                txbSearch.Text = null;
                dpDateStart.SelectedDate = null;
                lbChild.ItemsSource = ConnectHelper.entObj.Children.OrderBy(x => x.Age).ToList();
            }
            else if (cmbFilt.SelectedIndex == 2)
            {
                txbSearch.Text = null;
                dpDateStart.SelectedDate = null;
                lbChild.ItemsSource = ConnectHelper.entObj.Children.OrderBy(x => x.DateOfBirth).ToList();
            }
        }

        private void dpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbChild.ItemsSource = ConnectHelper.entObj.Children.Where(x => x.DateOfBirth >= dpDateStart.SelectedDate).ToList();
        }

        private void miMore_Click(object sender, RoutedEventArgs e)
        {
            MoreInfoChildren();
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void lbChild_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MoreInfoChildren();
        }
        private void MoreInfoChildren()
        {
            childObj = lbChild.SelectedItem as Children;
            if (childObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var parent = ConnectHelper.entObj.ParentChildren.FirstOrDefault(x => x.ID_Children == childObj.ID_Children);
                var group = ConnectHelper.entObj.ChildrenInGroup.FirstOrDefault(x => x.id_Children == childObj.ID_Children);
                if (group == null)
                    MessageBox.Show("Родитель: " + parent.FIOPar + "\n" + "Группа: ребёнок не числится в группе ", "Подробная информация", MessageBoxButton.OK);
                else
                    MessageBox.Show("Родитель: " + parent.FIOPar + "\n" + "Группа: " + group.Group.Name, "Подробная информация", MessageBoxButton.OK);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddChildren windowAddChildren = new WindowAddChildren();
            windowAddChildren.ShowDialog();
            lbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
        }
    }
}
