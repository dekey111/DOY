using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace DOY.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowContact.xaml
    /// </summary>
    public partial class WindowContact : Window
    {
        public WindowContact()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void iInstagram_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.instagram.com/detskiisad16veselaialopan/") { UseShellExecute = true });

        }

        private void iTelegram_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/public58691843") { UseShellExecute = true });
        }

        private void iVk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/public58691843") { UseShellExecute = true });
        }

        private void iWhatsapp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/public58691843") { UseShellExecute = true });

        }
    }
}
