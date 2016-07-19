using System;
using System.Collections.ObjectModel;
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
    public class VKFeed
    {
        public const string NameOfService = "VK";
        public string next_from { get; set; }
        public IEnumerable<VKPublication> items { get; set; }
        public IEnumerable<Profile> profiles { get; set; }
        public IEnumerable<Group> groups { get; set; }

        public class VKPublication
        {
            public int source_id { get; set; }
            public long date { get; set; }
            public IEnumerable<Attachment> attachments { get; set; }
            public Comments comments { get; set; }
            public Likes likes { get; set; }
            public Reposts reposts { get; set; }
            public string text { get; set; }
            public IEnumerable<CopyHistory> copy_history { get; set; }

            public class Attachment
            {
                public string type { get; set; }
                public Photo photo { get; set; }
                public class Photo
                {
                    public string photo_75 { get; set; }
                    public string photo_130 { get; set; }
                    public string photo_604 { get; set; }
                    public string photo_807 { get; set; }
                    public string photo_1280 { get; set; }
                    public string photo_2560 { get; set; }

                    public override string ToString()
                    {
                        if (photo_2560 != null)
                        {
                            return photo_2560;
                        }
                        if (photo_1280 != null)
                        {
                            return photo_1280;
                        }
                        if (photo_807 != null)
                        {
                            return photo_807;
                        }
                        if (photo_604 != null)
                        {
                            return photo_604;
                        }
                        if (photo_130 != null)
                        {
                            return photo_130;
                        }
                        if (photo_75 != null)
                        {
                            return photo_75;
                        }
                        return null;
                    }
                }
                public override string ToString()
                {
                    return type == "photo" ? photo.ToString() : null;
                }
            }

            public class CopyHistory
            {
                public int owner_id { get; set; }
                public long date { get; set; }
                public IEnumerable<Attachment> attachments { get; set; }
                public string text { get; set; }

                public Publication ToPublication(IEnumerable<Profile> profiles, IEnumerable<Group> groups)
                {
                    Publication pub = new Publication();
                    pub.Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(this.date).ToLocalTime();

                    if (this.attachments != null)
                    {
                        pub.Images = new ObservableCollection<string>(this.attachments.Where(p => p.type == "photo").Select(p => p.ToString()));
                    }
                    if (this.owner_id < 0)
                    {
                        Group group = groups.Where(p => p.id == -this.owner_id).FirstOrDefault();
                        pub.NameOfAuthor = group.name;
                        pub.Logo = group.GetPhoto();
                    }
                    else
                    {
                        Profile profile = profiles.Where(p => p.id == this.owner_id).FirstOrDefault();
                        pub.NameOfAuthor = $"{profile.first_name} {profile.last_name}";
                        pub.Logo = profile.GetPhoto();
                    }
                    pub.NameOfService = NameOfService;

                    pub.Text = this.text;

                    return pub;
                }
            }
            public Publication ToPublication(IEnumerable<Profile> profiles, IEnumerable<Group> groups)
            {
                Publication pub = new Publication();
                pub.Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(this.date).ToLocalTime();
                
                if (this.attachments != null)
                {
                    pub.Images = new ObservableCollection<string>(this.attachments.Where(p => p.type == "photo").Select(p => p.ToString()));
                }
                if (this.source_id < 0)
                {
                    Group group = groups.Where(p => p.id == -this.source_id).FirstOrDefault();
                    pub.NameOfAuthor = group.name;
                    pub.Logo = group.GetPhoto();
                }
                else
                {
                    Profile profile = profiles.Where(p => p.id == this.source_id).FirstOrDefault();
                    pub.NameOfAuthor = $"{profile.first_name} {profile.last_name}";
                    pub.Logo = profile.GetPhoto();
                }
                pub.NameOfService = NameOfService;
                pub.NumberOfComments = this.comments == null ? 0 : this.comments.count;
                pub.NumberOfLikes = this.likes == null ? 0 : this.likes.count;
                pub.NumberOfReposts = this.reposts == null ? 0 : this.reposts.count;

                if (copy_history != null)
                {
                    pub.CopyFrom = copy_history.FirstOrDefault().ToPublication(profiles, groups);
                }

                pub.Text = this.text;

                return pub;
            }

            public class Comments
            {
                public int count { get; set; }
            }

            public class Likes
            {
                public int count { get; set; }
            }

            public class Reposts
            {
                public int count { get; set; }
            }
        }

        public class Profile
        {
            public int id { get; set; }
            public string photo_50 { get; set; }
            public string photo_100 { get; set; }
            public string photo_200 { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }

            public string GetPhoto()
            {
                return photo_200 != null ? photo_200 : photo_100 != null ? photo_100 : photo_50;
            }
        }

        public class Group
        {
            public int id { get; set; }
            public string photo_50 { get; set; }
            public string photo_100 { get; set; }
            public string photo_200 { get; set; }
            public string name { get; set; }

            public string GetPhoto()
            {
                return photo_200 != null ? photo_200 : photo_100 != null ? photo_100 : photo_50;
            }
        }

        public Feed ToFeed()
        {
            return new Feed(items.Select<VKPublication, Publication>(p => p.ToPublication(profiles, groups)));
        }
    }
}
