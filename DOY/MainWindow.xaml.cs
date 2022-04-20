using DOY.dataFiles;
using DOY.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DOY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectHelper.entObj = new DOYEntities1();
            FrameApp.frmObj = FrmMain;
            FrameApp.frmObj.Navigate(new PageChildren());

            var fio = ConnectHelper.entObj.Chief.FirstOrDefault(x => x.ID_Chief == 1);
            txbFIO.Text = fio.FIO;
        }
        private void btnKindergarten_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageKindergarten());
        }

        private void btnParent_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageParents());
        }

        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageContract());
        }

        private void btnKindergartener_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageKindergartener());
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageGroup());
        }

        private void btnChild_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageChildren());
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            WindowContact windowContact = new WindowContact();
            windowContact.ShowDialog();
        }
    }
}
