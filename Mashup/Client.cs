using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;

namespace Mashup
{
    // Абстрактная модель представления клиента доступа к API
    public abstract class Client : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;

        // Авторизовать
        public abstract Task Authorize();
        // Получить порцию новостей
        public abstract Task<Feed> GetFeed(int count);
        // Имя сервиса
        public abstract string Name { get; }
        // Логотип сервиса
        public abstract Uri Logo { get; }
        // Сбросить значение следующей формы списка новостей, чтобы получить свежие новости
        public abstract void ResetToGetNew();
        // Авторизован ли пользователь
        public bool Authorized { get; set; } = false;
    }
}
