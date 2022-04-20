using DOY.dataFiles;
using DOY.Pages.Edit;
using DOY.Pages.View.MoreGroups;
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
    /// Логика взаимодействия для PageGroup.xaml
    /// </summary>
    public partial class PageGroup : Page
    {
        private static int idGroup;
        private static Group groupObj;
        public PageGroup()
        {
            InitializeComponent();
            lbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            groupObj = lbGroup.SelectedItem as Group;
            if (groupObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                idGroup = groupObj.ID_Group;
                WindowEditGroup windowEditGroup = new WindowEditGroup();
                windowEditGroup.ShowDialog();
            }
        }
        public static int GetIdGroup()
        {
            return idGroup;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddGroup windowAddGroup = new WindowAddGroup();
            windowAddGroup.ShowDialog();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();
        }

        private void miMore_Click(object sender, RoutedEventArgs e)
        {
            MoreInfo();
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();
        }

        private void lbGroup_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MoreInfo();
        }
        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }


        private void MoreInfo()
        {
            groupObj = lbGroup.SelectedItem as Group;
            if (groupObj == null)
                MessageBox.Show("Запись не выбрана!", "Подробности по группе", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                idGroup = groupObj.ID_Group;

                WindowMoreInfoForGroup windowMoreInfoForGroup = new WindowMoreInfoForGroup();
                windowMoreInfoForGroup.ShowDialog();

            }

            //groupObj = lbGroup.SelectedItem as Group;
            //var countChild = ConnectHelper.entObj.ChildrenInGroup.Count(x => x.id_Group == groupObj.ID_Group);
            //var Emp = ConnectHelper.entObj.Kindergarten.FirstOrDefault(x => x.id_Group == groupObj.ID_Group);
            //var empName = ConnectHelper.entObj.Kindergartener.FirstOrDefault(x => x.ID_Kindergartener == Emp.ID_Kindergarten);

            //if (countChild == 0 && empName == null)
            //    MessageBox.Show("В этой группе нету детей и классного руководителя","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);
            //else if(countChild == 0)
            //{
            //    MessageBox.Show("В этой группе нету детей\n" + 
            //        "Классный руководитель: " + empName.FIO, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else if (empName == null)
            //{
            //    MessageBox.Show("Количество воспитанников: " + countChild + " Чел.\n" + 
            //        "В этой группе нету классного руководителя", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            //}

            //else
            //MessageBox.Show("Количество воспитанников: " + countChild + " Чел.\n" + 
            //    "Классный руководитель: " + empName.FIO);

        }


        private void elementDelete()
        {
            groupObj = lbGroup.SelectedItem as Group;
            if (groupObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить:'" + groupObj.Name + "' ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Group.Remove(groupObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
