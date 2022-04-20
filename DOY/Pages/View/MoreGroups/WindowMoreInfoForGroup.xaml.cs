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
using DOY.dataFiles;

namespace DOY.Pages.View.MoreGroups
{
    /// <summary>
    /// Логика взаимодействия для WindowMoreInfoForGroup.xaml
    /// </summary>
    public partial class WindowMoreInfoForGroup : Window
    {
        private int idGroup = PageGroup.GetIdGroup();
        private Kindergarten kindergartenObj;
        public WindowMoreInfoForGroup()
        {
            InitializeComponent();
            lbNotKind.Items.Add("В этой группе не назначен воспитатель");

            lManInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Мальчик").Count();
            lGirlInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Девочка").Count();
            lChildInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count() + " / 25 ";
            var nameGroup = ConnectHelper.entObj.Group.FirstOrDefault(x => x.ID_Group == idGroup);
            txbnameGroup.Text = nameGroup.Name;

            if (ConnectHelper.entObj.Kindergarten.Where(x => x.id_Group == idGroup && x.id_Kindergartener == null).Count() > 0)
            {
                lbKindergartener.Visibility = Visibility.Collapsed;
                lbNotKind.Visibility = Visibility.Visible;
            }
            else
            {
                lbNotKind.Visibility = Visibility.Collapsed;
                lbKindergartener.Visibility = Visibility.Visible;
                lbKindergartener.ItemsSource = ConnectHelper.entObj.Kindergarten.Where(x => x.id_Group == idGroup).ToList();
            }

            cmbKindergartener.SelectedValuePath = "ID_Kindergartener";
            cmbKindergartener.DisplayMemberPath = "FIO";
            cmbKindergartener.ItemsSource = ConnectHelper.entObj.Kindergartener.ToList();
        }

        private void btnDelKind_Click(object sender, RoutedEventArgs e)
        {
             kindergartenObj = lbKindergartener.SelectedItem as Kindergarten;
            if (kindergartenObj == null)
                MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Kindergarten kindObj = ConnectHelper.entObj.Kindergarten.FirstOrDefault(x => x.id_Kindergartener == kindergartenObj.id_Kindergartener);
                kindObj.id_Kindergartener = null;
                ConnectHelper.entObj.SaveChanges();

                lbKindergartener.Visibility = Visibility.Collapsed;
                lbNotKind.Visibility = Visibility.Visible;
            }
        }

        private void btnAddKind_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKindergartener.SelectedValue == null)
                MessageBox.Show("Запись не выбрана!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {

                try
                {
                    var selectedKind = Convert.ToInt32(cmbKindergartener.SelectedValue);
                    Kindergarten kindObj = ConnectHelper.entObj.Kindergarten.FirstOrDefault(x => x.id_Group == idGroup);
                    kindObj.id_Kindergartener = selectedKind;
                    ConnectHelper.entObj.SaveChanges();

                    lbNotKind.Visibility = Visibility.Collapsed;
                    lbKindergartener.Visibility = Visibility.Visible;
                    lbKindergartener.ItemsSource = ConnectHelper.entObj.Kindergarten.Where(x => x.id_Group == idGroup ).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла критическая ошибка при добавлении воспитателя\n" + ex.Message,"Добавление воспитателя", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }

        }
    }
}
