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
            user = new User("Unknown", "Unknown", "Unknown");

            getUser();

        }

        private bool getUser()
        {
            var request_api = "https://vietnguyen.me/pastime/retrieve_user.php";
            var client = new RestClient(request_api);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", current_user);

            var response = client.Execute<UserJson>(request).Content;

            //This causes the crash
            var something = JsonConvert.DeserializeObject<UserJson>(response).user_json[0].email;

            Console.WriteLine(response);

            return true;
        }

        public User User
        {
            get => user;
        }

    }
}
