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
        private ChildrenInGroup childInGroupObj;
        private string nameMainGroup;
        public WindowMoreInfoForGroup()
        {
            InitializeComponent();

            lbNotKind.Items.Add("В этой группе не назначен воспитатель");
            lbChildrenInGroupNot.Items.Add("В этой группе нет детей");
            var nameGroup = ConnectHelper.entObj.Group.FirstOrDefault(x => x.ID_Group == idGroup);
            nameMainGroup = nameGroup.Name;
            txbnameGroup.Text = nameMainGroup;
            CountChildren();


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

            if (ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count() > 0)
            {
                lbChildrenInGroupNot.Visibility = Visibility.Collapsed;
                lbChildrenInGroup.Visibility = Visibility.Visible;
                lbChildrenInGroup.ItemsSource = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).ToList();
            }
            else
            {
                lbChildrenInGroup.Visibility = Visibility.Collapsed;
                lbChildrenInGroupNot.Visibility = Visibility.Visible;
            }

            cmbKindergartener.SelectedValuePath = "ID_Kindergartener";
            cmbKindergartener.DisplayMemberPath = "FIO";
            cmbKindergartener.ItemsSource = ConnectHelper.entObj.Kindergartener.ToList();

            cmbChild.SelectedValuePath = "ID_Children";
            cmbChild.DisplayMemberPath = "FIO";
            cmbChild.ItemsSource = ConnectHelper.entObj.Children.ToList();
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

        private void btnDelChild_Click(object sender, RoutedEventArgs e)
        {
            childInGroupObj = lbChildrenInGroup.SelectedItem as ChildrenInGroup;
            if (childInGroupObj == null)
                MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                ChildrenInGroup childrenObj = ConnectHelper.entObj.ChildrenInGroup.FirstOrDefault(x => x.id_Children == childInGroupObj.id_Children && x.id_Group == idGroup);
                ConnectHelper.entObj.ChildrenInGroup.Remove(childrenObj);
                ConnectHelper.entObj.SaveChanges();

                if (ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count() > 0)
                {
                    lbChildrenInGroupNot.Visibility = Visibility.Collapsed;
                    lbChildrenInGroup.Visibility = Visibility.Visible;
                    lbChildrenInGroup.ItemsSource = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).ToList();
                    CountChildren();
                }
                else
                {
                    lbChildrenInGroup.Visibility = Visibility.Collapsed;
                    lbChildrenInGroupNot.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnEditChild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {
            if (cmbChild.SelectedValue == null)
                MessageBox.Show("Запись не выбрана!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);

            else if (ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Children == (int)cmbChild.SelectedValue && x.id_Group == idGroup).Count() > 0)
                MessageBox.Show("Выбранный ребёнок уже в данной группе!", "Добавление воспитанника", MessageBoxButton.OK, MessageBoxImage.Warning);

            else if (ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Children == (int)cmbChild.SelectedValue).Count() > 0)
            {
                var nameChild = ConnectHelper.entObj.Children.FirstOrDefault(x => x.ID_Children == (int)cmbChild.SelectedValue);
                var idChild = ConnectHelper.entObj.ChildrenInGroup.FirstOrDefault(x => x.id_Children == (int)cmbChild.SelectedValue);
                var group = ConnectHelper.entObj.Group.FirstOrDefault(x => x.ID_Group == idChild.id_Group);
                MessageBoxResult result = MessageBox.Show("Перенаправить ребенка ребёнка: '" + nameChild.FIO + "'\nиз группы: '" + group.Name + "' в группу: '" + nameMainGroup + "'",
                    "Добавление воспитанника", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ChildrenInGroup childObj = ConnectHelper.entObj.ChildrenInGroup.FirstOrDefault(x => x.id_Group == group.ID_Group && x.id_Children == (int)cmbChild.SelectedValue);
                    childObj.id_Group = idGroup;
                    ConnectHelper.entObj.SaveChanges();

                    lbChildrenInGroupNot.Visibility = Visibility.Collapsed;
                    lbChildrenInGroup.Visibility = Visibility.Visible;
                    lbChildrenInGroup.ItemsSource = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).ToList();
                    CountChildren();
                }
            }
            else
            {
                ChildrenInGroup childrenInGroupObj = new ChildrenInGroup()
                {
                    id_Children = (int)cmbChild.SelectedValue,
                    id_Group = idGroup
                };
                ConnectHelper.entObj.ChildrenInGroup.Add(childrenInGroupObj);
                ConnectHelper.entObj.SaveChanges();

                lbChildrenInGroupNot.Visibility = Visibility.Collapsed;
                lbChildrenInGroup.Visibility = Visibility.Visible;
                lbChildrenInGroup.ItemsSource = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).ToList(); 
                CountChildren();

            }
        }

        private void CountChildren()
        {
            lManInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Мальчик").Count();
            lGirlInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Девочка").Count();
            lChildInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count() + " / 25 ";
        }
    }
}
