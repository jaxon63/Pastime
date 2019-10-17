using System;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Pastime.Models
{
    public class UserModel
    {
        private User user;
        private string current_user;
        public UserModel()
        {
            current_user = Application.Current.Properties["current_user"].ToString();

            if (getUser() != null)
            {
                user = getUser();
            }
            else
            {
                user = new User("Unknown", "Unknown", "Unknown");
            }
        }

        private User getUser()
        {
            var request_api = "https://vietnguyen.me/pastime/retrieve_user.php";
            var client = new RestClient(request_api);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", current_user);


            //get the JSON response
            var response = client.Execute<UserJson>(request).Content;
            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["User"];
            var item = (JObject)items[0];

            string username = (string)item["username"];
            string email = (string)item["email"];
            string password = (string)item["password"];

            User returnUser = new User(email, password, username);

            if (returnUser != null)
            {
                return returnUser;
            }
            return null;
        }

        public User User
        {
            get => user;
        }

    }
}
