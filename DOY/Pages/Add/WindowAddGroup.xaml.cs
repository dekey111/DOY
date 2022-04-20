using DOY.dataFiles;
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
    /// Логика взаимодействия для WindowAddGroup.xaml
    /// </summary>
    public partial class WindowAddGroup : Window
    {
        public WindowAddGroup()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var groupObj = ConnectHelper.entObj.Group.FirstOrDefault(x => x.Name.Contains(txbName.Text));

           
            if (txbName.Text.Length == 0)
                MessageBox.Show("Заполните название группы!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (groupObj != null)
                MessageBox.Show("Такая группа уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Group group = new Group()
                {
                    Name = txbName.Text
                };
                ConnectHelper.entObj.Group.Add(group);
                ConnectHelper.entObj.SaveChanges();


                Kindergarten kindergarten = new Kindergarten()
                {
                    id_Group = group.ID_Group,
                    Cabinet = txbCab.Text,
                    Floor = txbfloor.Text
                };
                ConnectHelper.entObj.Kindergarten.Add(kindergarten);
                ConnectHelper.entObj.SaveChanges();


                MessageBox.Show("Новая группа добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
