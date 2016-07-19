using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup
{
    // Модель представления списка новостей
    public class Feed : INotifyPropertyChanged
    {
        // Коллекция источников новостей
        private ObservableCollection<Publication> items = new ObservableCollection<Publication>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Feed() { }

        public Feed(IEnumerable<Publication> publications)
        {
            this.items = new ObservableCollection<Publication>(publications);
        }

        // Список новостей
        public ObservableCollection<Publication> News
        {
            get
            {
                return items;
            }
        }

        // Список источников новостей
        public List<Client> Sources { get; set; } = new List<Client>();

        // Получить свежие новости
        public async Task Update()
        {
            foreach (Client client in Sources)
            {
                if (client.Authorized)
                {
                    client.ResetToGetNew();
                    Add(await client.GetFeed(10));
                }
            }
        }

        // Получить следующую порцию  новостей
        public async Task GetNext()
        {
            foreach (Client client in Sources)
            {
                if (client.Authorized)
                {
                    Add(await client.GetFeed(10));
                }
            }
        }

        // Добавить источник и отсортировать список новостей по дате
        public void Add(Feed feed)
        {
            items = new ObservableCollection<Publication>(
                items.Concat(feed.News)
                .OrderBy<Publication, DateTime>(p => p.Date)
                );
            OnPropertyChanged("News");
        }

        // Уведомить View об изменении свойства с именем name
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
