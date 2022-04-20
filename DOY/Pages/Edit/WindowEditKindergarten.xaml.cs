using DOY.dataFiles;
using DOY.Pages.View;
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

namespace DOY.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для WindowEditKindergarten.xaml
    /// </summary>
    public partial class WindowEditKindergarten : Window
    {
        private int idKind = PageKindergarten.GetIDKindergarten();
        public WindowEditKindergarten()
        {
            InitializeComponent();
            cmbGroup.SelectedValuePath = "ID_Group";
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.ItemsSource = ConnectHelper.entObj.Group.ToList();

            var KindObj = ConnectHelper.entObj.Kindergarten.FirstOrDefault(x => x.ID_Kindergarten == idKind);

            cmbGroup.Text = KindObj.Group.Name;
            txbCab.Text = KindObj.Cabinet;
            txbfloor.Text = KindObj.Floor;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int selectedKind = Convert.ToInt32(cmbGroup.SelectedValue);
            if (cmbGroup.SelectedValue == null
                && txbCab.Text.Length == 0
                && txbfloor.Text.Length == 0)
                MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (cmbGroup.SelectedValue == null)
                MessageBox.Show("Заполните поле 'Группа'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbCab.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Кабинет'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txbfloor.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Этаж'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {


                IEnumerable<Kindergarten> kindergarten = ConnectHelper.entObj.Kindergarten.Where(x => x.ID_Kindergarten == idKind).AsEnumerable().
        Select(x =>
        {
            x.id_Group = selectedKind;
            x.Cabinet = txbCab.Text;
            x.Floor = txbfloor.Text;
            return x;
        });
                foreach (Kindergarten kind in kindergarten)
                {
                    ConnectHelper.entObj.Entry(kind).State = System.Data.Entity.EntityState.Modified;
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
