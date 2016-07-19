using System;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.VK
{
    public partial class VKClient : Client
    {
        // Получает список новостей
        public override async Task<Feed> GetFeed(int count = 50)
        {
            if (!Authorized)
            {
                throw new InvalidOperationException("Unauthorized");
            }

            Feed feed = null;

            // строка запроса к API
            string requset_string = $"https://api.vk.com/method/newsfeed.get" +
                $"?user_id={UserID}" +
                $"&count={count}" +
                $"&filters={filters}" +
                (next_from == null ? $"" : $"&start_from={next_from}") +
                $"&v=5.45" +
                $"&access_token={AccessToken}";
            
            // сформировать запрос
            HttpWebRequest requset = (HttpWebRequest)WebRequest.Create(requset_string);
            requset.Method = "GET";

            using (WebResponse response = await requset.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    await Task.Run(() =>
                    {
                        // Чтение запроса посредством Newtonsoft Json
                        // Nuget package: Newtonsoft Json
                        VKResponse vk_response = JsonConvert.DeserializeObject<VKResponse>(reader.ReadToEnd());
                        if (vk_response.response != null)
                        {
                            feed = vk_response.response.ToFeed();
                        }
                        else if (vk_response.error != null)
                        {
                            throw new VKGetFeedException("Can't get feed", vk_response.error);
                        }
                        else
                        {
                            throw new VKGetFeedException("Unsupported response");
                        }
                    });
                }
            }

            return feed;
        }

        public override void ResetToGetNew()
        {
            this.next_from = null;
        }
    }
}
