using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TwitterSample
{
    public class Pagination
    {
        public string next_url { get; set; }
        public string next_max_id { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
        public List<string> data { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
        public List<User> data { get; set; }
    }

    public class Picture
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Images
    {
        public Picture low_resolution { get; set; }
        public Picture thumbnail { get; set; }
        public Picture standard_resolution { get; set; }
    }

    public class Caption
    {
        public string created_time { get; set; }
        public string text { get; set; }
        public User from { get; set; }
        public string id { get; set; }
    }

    public class User
    {
        public string username { get; set; }
        public string website { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
        public string bio { get; set; }
        public string id { get; set; }
    }

    public class LowResolution2
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class StandardResolution2
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Videos
    {
        public LowResolution2 low_resolution { get; set; }
        public StandardResolution2 standard_resolution { get; set; }
    }

    public class Datum
    {
        //public object attribution { get; set; }
        public List<string> tags { get; set; }
        public string type { get; set; }
        //public object location { get; set; }
        public Comments comments { get; set; }
        public string filter { get; set; }
        public string created_time { get; set; }
        public string link { get; set; }
        public Likes likes { get; set; }
        public Images images { get; set; }
        //public List<object> users_in_photo { get; set; }
        public Caption caption { get; set; }
        public bool user_has_liked { get; set; }
        public string id { get; set; }
        public User user { get; set; }
        public Videos videos { get; set; }

        public override string ToString()
        {
            if (user == null)
            {
                return "User is null";
            }
            return user.full_name;
        }
    }

    public class TwitterResponse
    {
        public Pagination pagination { get; set; }
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }
}