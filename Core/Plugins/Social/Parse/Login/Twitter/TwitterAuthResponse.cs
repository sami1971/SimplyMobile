using System;

namespace SimplyMobile.Plugins.Parse.Login.Twitter
{
    public class TwitterAuthResponse
    {
        public string username { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string objectId { get; set; }
        public string sessionToken { get; set; }
        public AuthData authData { get; set; }
    }
}

