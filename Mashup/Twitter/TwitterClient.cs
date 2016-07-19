using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;

namespace Mashup.Twitter
{
    /*
    *   Model-View
    *   Класс, реализующий доступ к Twitter API
    *   В качестве Model используется само API
    */
    public partial class TwitterClient : Client
    {
        //Ключи для доступа к API
        protected const string consumer_key = "foRks8KuW5z3wwttv3AHQluiS";
        protected const string consumer_secret = "btmrz2GcQKeJa6r9CS2IJpuYjT8x0F0OJzRXs92czDjZoobqnZ";
        protected const string redirect_uri = "http://api.twitter.com/login_success.html";
        private TweetSharp.TwitterService service = new TweetSharp.TwitterService(consumer_key, consumer_secret);
        private long? since_id;

        public override event PropertyChangedEventHandler PropertyChanged;

        // Имя сервиса для View
        public override string Name
        {
            get
            {
                return "Twitter";
            }
        }

        // Имя сервиса для View
        public override Uri Logo
        {
            get
            {
                return new Uri(Environment.CurrentDirectory + "\\..\\..\\Resources\\twitter.png");
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
