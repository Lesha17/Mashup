using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.VK
{
   /*
   * Класс необходим для десериализации списка новостей посредством Newtonsoft Json
   * И преобразования его к формату представления
   */
    public class VKResponse
    {
        public VKFeed response { get; set; }
        public VKError error { get; set; }
    }
}
