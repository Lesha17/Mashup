/*
*   Model-View
*   В файле находится часть класса VKClient,
*   отвечающее за авторизацию пользователя в VK
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mashup.VK
{
    public partial class VKClient : Client
    {
        /*
        *   Авторизует пользователя
        */
        public override async Task Authorize()
        {
            //  Строка запроса
            string oauth_request = $"https://oauth.vk.com/authorize" +
                $"?client_id={clientID}" +
                $"&redirect_uri={redirect_uri}" +
                $"&display=page" +
                $"&scope={permissions}" +
                $"&response_type=token" +
                $"&v=5.45";

            // Создать окно браузера, направляющее пользователя на страницу авторизации
            AuthWindow auth_window = new AuthWindow(oauth_request, u => u.OriginalString.StartsWith(redirect_uri));
            if (auth_window.ShowDialog().GetValueOrDefault())
            {
                // Получить адрес, на который браузер перенаправил пользователя
                Uri redirected_uri = auth_window.RedirectedUri;
                Dictionary<string, string> response = null;

                // Разбить адрес на коллекцию ключ - значение
                response = redirected_uri.Fragment.Substring(1)
                        .Split('&')
                        .Select(i => i.Split('='))
                        .ToDictionary(i => i[0], i => WebUtility.UrlDecode(i[1]));

                // Получить токен доступа из этой коллекции
                AccessToken = response["access_token"];

                // Получить идентификатор пользователя из этой коллекции
                UserID = Convert.ToInt32(response["user_id"]);

                // Флаг, необходимый для проверки авторизации
                Authorized = true;

                // Уведомить View об успешной авторизации
                OnPropertyChanged("Authorized");
            }

        }
    }
}
