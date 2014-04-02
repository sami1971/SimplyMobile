using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;

namespace RestClientTest
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class NewUserResponseDto
    {
        public long UserId { get; set; }
    }

    public class UserService
    {
        private readonly IRestClient client;

        public UserService()
        {
            var client = new RestClient (
                new Uri ("http://servicestack.net/ServiceStack.Examples.Host.Web/ServiceStack/Json/SyncReply"),
                new JsonSerializer());

            this.client = client;
        }

        public async Task<long> CreateNewUser(User user)
        {
            var response = await this.client.PostAsync<NewUserResponseDto>("StoreNewUser", user, Format.Json);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Value.UserId;
            }

            return -1;
        }
    }
}
