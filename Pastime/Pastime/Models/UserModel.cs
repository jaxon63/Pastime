using System;
using Newtonsoft.Json;
using RestSharp;

namespace Pastime.Models
{
    public class UserModel
    {
        private User user;
        private string current_user;
        public UserModel()
        {
            current_user = Application.Current.Properties["current_user"].ToString();
            user = getUser();
            Console.WriteLine("Username: " + user.Username);
        }

        private User getUser()
        {
            var request_api = "https://vietnguyen.me/pastime/retrieve_user.php";
            var client = new RestClient(request_api);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", current_user);

            var response = client.Execute<UserJson>(request).Content;

            Console.WriteLine(response);

            User returnUser = new User(JsonConvert.DeserializeObject<UserJson>(response).email, JsonConvert.DeserializeObject<UserJson>(response).username, JsonConvert.DeserializeObject<UserJson>(response).password);


            return returnUser;
        }

        public User User
        {
            get => user;
        }

    }
}
