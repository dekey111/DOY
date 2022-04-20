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
        public WindowMoreInfoForGroup()
        {
            InitializeComponent();

            lManInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Мальчик").Count();
            lGirlInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup && x.Children.Gender.Name == "Девочка").Count();
            lChildInGroup.Content = ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count();
        }
    }
}
