using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;

namespace Pastime.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
        }

        public List<string> Validate(string email, string password)
        {
            List<string> results = new List<string>();

            //declare your related api's address here
            //mine is Login, yours maybe Register etc.
            string login_api = "https://vietnguyen.me/pastime/login.php";

            //create a client object
            var client = new RestClient(login_api);

            //create a request
            //for Login I used GET method, but for Register or Profile update
            //you'll need to use POST method
            var request = new RestRequest(Method.GET);

            //add the parameters to your APIs
            //note: profile update API needs to have action parameter
            //please check the with me for correct parameter declarations to use the APIs.
            request.AddParameter("email", email);
            request.AddParameter("password", password);

            //get the JSON response
            var response = client.Execute<LoginJSON>(request).Content;
            //my JSON response only have 1 JSON object
            //that's why I used .login[0] (index 0 object)
            var status = JsonConvert.DeserializeObject<LoginJSON>(response).login[0].status;
            results.Add(status);

            var current_user = JsonConvert.DeserializeObject<LoginJSON>(response).login[0].current_user;
            results.Add(current_user);

            //return the status
            return results;
        }

        //Viet's LogMeIn function with some of its functionality moved to the view model
        public bool LogMeIn(string email, string password, out string current_user)
        {
            List<string> response = Validate(email, password);

            string status = response[0];
            current_user = response[1];

            if (status == "success")
            {
                //This should store the users logged in status, so when the app is closed,
                //the next time they reopen it, they are still logged in
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                return true;
            }
            return false;
        }

      


    }
}
