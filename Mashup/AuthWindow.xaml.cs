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

namespace Mashup
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public delegate bool HasToken(Uri redirected_uri);

        private HasToken has_token;

        public AuthWindow(string request, HasToken has_token)
        {
            InitializeComponent();
            this.has_token = has_token;

            AuthBrowser.Navigated += AuthBrowser_Navigated;
            AuthBrowser.Navigate(request);
        }

        public AuthWindow(string request, HasToken has_token, string service_name) : this(request, has_token)
        {
            this.Title = $"Авторизация в {service_name}";
        }

        private void AuthBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Console.WriteLine(e.Uri.OriginalString);
            if (has_token(e.Uri))
            {
                this.RedirectedUri = e.Uri;
                DialogResult = true;
                Close();
            }
        }

        public Uri RedirectedUri { get; private set; }


    }
}
