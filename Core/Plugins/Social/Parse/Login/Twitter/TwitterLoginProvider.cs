using System;
using SimplyMobile.Web;
using System.Threading.Tasks;
using SimplyMobile.Text;

namespace SimplyMobile.Plugins.Parse.Login.Twitter
{
    public static class TwitterLoginProvider
    {
        public static Task<ServiceResponse<TwitterAuthResponse>> Login(
            Twitter twitterInfo,
            string applicationId,
            string apiKey,
            IRestClient restClient)
        {
            var request = new TwitterAuthRequest () 
            {
                authData = new AuthData () 
                {
                    twitter = twitterInfo
                }
            };

            restClient.AddHeader ("X-Parse-Application-Id", applicationId);
            restClient.AddHeader ("X-Parse-REST-API-Key", apiKey);

            return restClient.PostAsync<TwitterAuthResponse>("https://api.parse.com/1/users", request, Format.Json);
        }
    }
}

