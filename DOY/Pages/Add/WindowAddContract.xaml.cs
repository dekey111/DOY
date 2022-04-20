using DOY.dataFiles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace DOY.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для WindowAddContract.xaml
    /// </summary>
    public partial class WindowAddContract : Window
    {
        private static string imagePathChildren;
        private static string imagePathParent;

        private static int idChild;
        private static int idParent;
        public WindowAddContract()
        {
            InitializeComponent();
            cmbGroup.SelectedValuePath = "ID_Group";
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();
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
                spParent.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                txbNumberStep.Text = "Шаг 2 из 3";
                txbTitle.Text = "Добавление родителя";
            }
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
                imagePathChildren = dlg.FileName;
                lPhotoPathChildren.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void btnParentNext_Click(object sender, RoutedEventArgs e)
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
                spChildren.Visibility = Visibility.Collapsed;
                spParent.Visibility = Visibility.Collapsed;

                spContract.Visibility = Visibility.Visible;
                txbNumberStep.Text = "Шаг 3 из 3";
                txbTitle.Text = "Формирование договора";
            }
        }

        private void btnSelectPhotoParent_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

                iImageParent.Source = new BitmapImage(new Uri(dlg.FileName));
               // lPhotoPathParent.Content = dlg.FileName;
                imagePathParent = dlg.FileName;
                lPhotoPathParent.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(spParent.Visibility == Visibility.Visible)
            {
                spChildren.Visibility = Visibility.Visible;
                spParent.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;
                txbNumberStep.Text = "Шаг 1 из 3";
                txbTitle.Text = "Добавление ребенка";
                btnBack.Visibility = Visibility.Collapsed;
            }
            else if (spParent.Visibility == Visibility.Collapsed && spContract.Visibility == Visibility)
            {
                spChildren.Visibility = Visibility.Collapsed;
                spContract.Visibility = Visibility.Collapsed;
                spParent.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                txbNumberStep.Text = "Шаг 2 из 3";
                txbTitle.Text = "Добавление родителя";
            }

        }
        private void addChildren()
        {

                if (imagePathChildren == null)
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
                        Image = File.ReadAllBytes(imagePathChildren)
                    };
                    ConnectHelper.entObj.Children.Add(children);
                    ConnectHelper.entObj.SaveChanges();
                    idChild = children.ID_Children;
                }
            
        }


        private void addParent()
        {

            if (imagePathParent == null)
            {
                Parent parent = new Parent()
                {
                    Surname = txbSurnameParent.Text,
                    FirstName = txbNameParent.Text,
                    MiddleName = txbMiddleParent.Text,
                    Phone = txbPhoneParent.Text,
                    Image = null,
                    DateOfBirth = dpDateOfBirthParent.SelectedDate
                };
                ConnectHelper.entObj.Parent.Add(parent);
                ConnectHelper.entObj.SaveChanges();
                idParent = parent.ID_Parent;
            }
            else
            {
                Parent parent = new Parent()
                {
                    Surname = txbSurnameParent.Text,
                    FirstName = txbNameParent.Text,
                    MiddleName = txbMiddleParent.Text,
                    Phone = txbPhoneParent.Text,
                    DateOfBirth = dpDateOfBirthParent.SelectedDate,
                    Image = File.ReadAllBytes(imagePathParent)
                };
                ConnectHelper.entObj.Parent.Add(parent);
                ConnectHelper.entObj.SaveChanges();
                idParent = parent.ID_Parent;


            }
        }

        private void addContract()
        {
            int idGroup = Convert.ToInt32(cmbGroup.SelectedValue);

            if (txbPay.Text.Length == 0
                && cmbGroup.SelectedValue == null)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(txbPay.Text.Length == 0)
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addChildren();
            addParent();
            addContract();
            MessageBox.Show("Новый договор успешно сформирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
