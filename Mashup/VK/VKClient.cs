using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;

namespace Mashup.VK
{
    /*
   *   Model-View
   *   Класс, реализующий доступ к VK API
   *   В качестве Model используется само API
   */
    public partial class VKClient : Client
    {
        // ключи и флаги для доступа к API
        protected const string clientID = "5288508";
        protected const string permissions = "wall,friends";
        protected const string redirect_uri = "https://oauth.vk.com/blank.html";
        protected const string apiHost = "https://api.vk.com/method/";
        protected const string filters = "post";
        protected string next_from = null;

        public override event PropertyChangedEventHandler PropertyChanged;

        public VKClient() { }

        public VKClient(string access_token) : this()
        {
            this.AccessToken = access_token;
        }

        // Токен доступа к API
        public string AccessToken { get; set; }
        public int UserID { get; set; }


        public override string Name
        {
            get
            {
                return VKFeed.NameOfService;
            }
        }

        public override Uri Logo
        {
            get
            {
                return new Uri(Environment.CurrentDirectory + "\\..\\..\\Resources\\vk.png");
            }
        }

        // Уведомить View об изменении свойства
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
