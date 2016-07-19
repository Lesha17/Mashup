using System;
using System.Collections.ObjectModel;
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

namespace Mashup
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Feed feed = new Feed();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = feed;
            init();
        }

        private async void init()
        {
            VK.VKClient vk = new VK.VKClient();

            Twitter.TwitterClient tw = new Twitter.TwitterClient();

            feed.Sources.Add(vk);
            feed.Sources.Add(tw);
        }

        private async void ListBox_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)(e.OriginalSource as Button).DataContext;
            try
            {
                await client.Authorize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private async void Recent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await feed.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await feed.GetNext();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
