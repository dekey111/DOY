using DOY.dataFiles;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DOY.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для WindowAddChildren.xaml
    /// </summary>
    public partial class WindowAddChildren : Window
    {
        private static string imagePath;
        private static int idChild;
        public WindowAddChildren()
        {
            InitializeComponent();
            cmbGroup.SelectedValuePath = "ID_Group";
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();

            cmbParent.SelectedValuePath = "ID_Parent";
            cmbParent.DisplayMemberPath = "FIO";
            cmbParent.ItemsSource = ConnectHelper.entObj.Parent.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addChildren();
            addContract();
            MessageBox.Show("Новый договор успешно сформирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSelectPhotoChildren_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

                iImageChildren.Source = new BitmapImage(new Uri(dlg.FileName));
                imagePath = dlg.FileName;
                lPhotoPathChildren.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void btnChildNext_Click(object sender, RoutedEventArgs e)
        {
            if (txbSurnameChild.Text.Length == 0
                  && txbNameChild.Text.Length == 0
                  && txbMiddleChild.Text.Length == 0
                  && dpDateOfBirthChild.SelectedDate == null)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbSurnameChild.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Фамилия'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbNameChild.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Имя'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbMiddleChild.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Отчетсво'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (dpDateOfBirthChild.SelectedDate == null)
                MessageBox.Show("Заполните поле 'Дата рождения'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                spChildren.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Visible;
                spContract.Visibility = Visibility.Visible;
                txbNumberStep.Text = "Шаг 2 из 2";
                txbTitle.Text = "Формирование контракта";
            }
        }
        private void addChildren()
        {

            if (imagePath == null)
            {
                Children children = new Children()
                {
                    Surname = txbSurnameChild.Text,
                    FirstName = txbNameChild.Text,
                    MiddleName = txbMiddleChild.Text,
                    Image = null,
                    DateOfBirth = dpDateOfBirthChild.SelectedDate
                };
                ConnectHelper.entObj.Children.Add(children);
                ConnectHelper.entObj.SaveChanges();
                idChild = children.ID_Children;
            }
            else
            {
                Children children = new Children()
                {
                    Surname = txbSurnameChild.Text,
                    FirstName = txbNameChild.Text,
                    MiddleName = txbMiddleChild.Text,
                    DateOfBirth = dpDateOfBirthChild.SelectedDate,
                    Image = File.ReadAllBytes(imagePath)
                };
                ConnectHelper.entObj.Children.Add(children);
                ConnectHelper.entObj.SaveChanges();
                idChild = children.ID_Children;
            }

        }

        private void addContract()
        {
            int idGroup = Convert.ToInt32(cmbGroup.SelectedValue);
            int idParent = Convert.ToInt32(cmbParent.SelectedValue);

            if (txbPay.Text.Length == 0
                && cmbGroup.SelectedValue == null)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbPay.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Сумма'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (cmbGroup.SelectedValue == null)
                MessageBox.Show("Заполните поле 'Группа'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Contract contract = new Contract()
                {
                    id_Children = idChild,
                    id_Parent = idParent,
                    id_Group = idGroup,
                    DateContract = DateTime.Today,
                    Pay = Convert.ToInt32(txbPay.Text)
                };

                ConnectHelper.entObj.Contract.Add(contract);

                ChildrenInGroup childrenInGroup = new ChildrenInGroup()
                {
                    id_Children = idChild,
                    id_Group = idGroup
                };

                ConnectHelper.entObj.ChildrenInGroup.Add(childrenInGroup);
                ConnectHelper.entObj.SaveChanges();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (spContract.Visibility == Visibility.Visible)
            {
                spChildren.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Collapsed;
                spContract.Visibility = Visibility.Collapsed;
                txbNumberStep.Text = "Шаг 1 из 2";
                txbTitle.Text = "Добавление ребенка";
            }
        }
    }
}
