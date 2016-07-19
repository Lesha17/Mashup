using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup
{
    // Модель представления одной новости
    public class Publication
    {
        // Имя сервиса - источника новости
        public string NameOfService { get; set; }
        // Имя автора новости
        public string NameOfAuthor { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        // Новость, репостом которой является эта новость. 
        public Publication CopyFrom { get; set; }
        // Аватар автора новости
        public string Logo { get; set; }
        // Прикреплённые изображения
        public ObservableCollection<string> Images{ get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReposts { get; set; }
    }
}
