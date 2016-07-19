/*
*   Model-View
*   В файле находится часть класса TwitterClient,
*   отвечающее за получение новостного потока с Twitter
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Mashup.Twitter
{
    public partial class TwitterClient : Client
    {
        /*
        *   Получает новостную ленту
        *
        * Используемое SDK взято с
        * https://github.com/danielcrenna/tweetsharp
        * NuGet package: tweetsharp-unofficial
        */
        public override async Task<Feed> GetFeed(int count)
        {
            if (!Authorized)
            {
                throw new InvalidOperationException("Unauthorized");
            }

            // Параметры запроса
            ListTweetsOnHomeTimelineOptions options = new ListTweetsOnHomeTimelineOptions()
            {
                Count = count,
                SinceId = since_id
            };

            // Получение новостной ленты через SDK
            IEnumerable<TwitterStatus> timeline =
             await Task<IEnumerable<TwitterStatus>>.Run(() => service.ListTweetsOnHomeTimeline(options));

            // Необходимо для получения следующей порции новостей
            this.since_id = options.SinceId;

            if (timeline == null)
            {
                throw new TwitterGetFeedException("Cannot get timeline");
            }

            return new Feed(
                timeline.Select<TwitterStatus, Publication>(p => new Publication()
                {
                    NameOfService = Name,
                    Date = p.CreatedDate.ToLocalTime(),
                    NameOfAuthor = p.Author.ScreenName,
                    Logo = p.Author.ProfileImageUrl,
                    Text = p.Text,
                    NumberOfReposts = p.RetweetCount
                }));
        }

        /*
        *   Сбрасывает since_api для получения свежих новостей
        */
        public override void ResetToGetNew()
        {
            this.since_id = null;
        }
    }
}
