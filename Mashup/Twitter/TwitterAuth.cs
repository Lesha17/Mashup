/*
*   Model-View
*   В файле находится часть класса TwitterClient,
*   отвечающее за авторизацию пользователя в Twitter
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Web;
using System.Net;
using System.Diagnostics;

namespace Mashup.Twitter
{
    public partial class TwitterClient : Client
    {
        /*
        *   Авторизует пользователя
        *
        * Используемое SDK взято с
        * https://github.com/danielcrenna/tweetsharp
        * NuGet package: tweetsharp-unofficial
        */
        public override async Task Authorize()
        {
            // Создать объект SDK
            this.service = new TwitterService(consumer_key, consumer_secret);
            
            // Получить request_token (см. авторизацию Twitter на сайте документации)
            OAuthRequestToken request_token = await Task.Run(() => service.GetRequestToken());

            //Создать uri для запроса авторизации (см. авторизацию Twitter на сайте документации)
            Uri auth_uri = service.GetAuthorizationUri(request_token, redirect_uri);

            // Создать окно браузера, направляющее пользователя на страницу авторизации
            AuthWindow auth_window = new AuthWindow(auth_uri.ToString(), u => u.OriginalString.StartsWith(redirect_uri));

            if (auth_window.ShowDialog().GetValueOrDefault())
            {
                // Получить адрес, на который браузер перенаправил пользователя
                Uri redirected_uri = auth_window.RedirectedUri;
                NameValueCollection response = null;

                // Разбить адрес на коллекцию ключ - значение
                response = HttpUtility.ParseQueryString(redirected_uri.OriginalString.Split('?')[1]);

                // Получить токен доступа (см. авторизацию Twitter на сайте документации)
                OAuthAccessToken token = await Task.Run(() => service.GetAccessToken(request_token, response["oauth_verifier"]));

                // Задать токен для выполнения последующих запросов к API
                service.AuthenticateWith(token.Token, token.TokenSecret);

                // Флаг, необходимый для проверки авторизации
                Authorized = true;

                // Уведомить View об успешной авторизации
                OnPropertyChanged("Authorized");
            }
        }
    }

}
