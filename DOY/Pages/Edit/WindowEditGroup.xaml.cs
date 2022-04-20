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
    /// Логика взаимодействия для WindowEditGroup.xaml
    /// </summary>
    public partial class WindowEditGroup : Window
    {
        private int idGroup = PageGroup.GetIdGroup();
        public WindowEditGroup()
        {
            InitializeComponent();
            var group = ConnectHelper.entObj.Group.FirstOrDefault(x => x.ID_Group == idGroup);
            txbName.Text = group.Name;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbName.Text.Length == 0)
                MessageBox.Show("Заполните поле 'Группа'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                IEnumerable<Group> groups = ConnectHelper.entObj.Group.Where(x => x.ID_Group == idGroup).AsEnumerable().
                Select(x =>
                {
                    x.Name = txbName.Text;
                    return x;
                });
                foreach (Group gr in groups)
                {
                    ConnectHelper.entObj.Entry(gr).State = System.Data.Entity.EntityState.Modified;
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
