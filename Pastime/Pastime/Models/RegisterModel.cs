using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace Pastime.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
        }

        public bool ValidateEmail(string email, out string emailErrMsg)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                emailErrMsg = "Email cannot be empty";
                return false;
            }
            emailErrMsg = string.Empty;
            return true;
        }

        public bool ValidateUsername(string username, out string usernameErrMsg)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                usernameErrMsg = "Username cannot be empty";
                return false;
            }
            usernameErrMsg = string.Empty;
            return true;
        }

        public bool ValidatePassword(string password, out string passwordErrMsg)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                passwordErrMsg = "Password cannot be empty";
                return false;
            }
            passwordErrMsg = string.Empty;
            return true;
        }

        public bool ValidateCPassword(string password, string cPassword)
        {
            if (password == cPassword)
                return true;
            return false;
        }

        public List<string> CreateUser(string email, string username, string password, string cPassword)
        {
            List<string> results = new List<string>();

            string register_api = "https://vietnguyen.me/pastime/register.php";

            //create a client object
            var client = new RestClient(register_api);

            //create a request
            var request = new RestRequest(Method.GET);

            //add the parameters to APIs
            request.AddParameter("email", email);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("verify_password", cPassword);

            //get the JSON response
            var response = client.Execute<RegisterJSON>(request).Content;
            var status = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].status;
            results.Add(status);

            if (status == "failed")
            {
                var reason = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].reason;
                results.Add(reason);
            } else
            {
                var current_user = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].current_user;
                results.Add(current_user);
            }

            //return the status
            return results;
        }

        public List<string> SubmitRegister(string email, string username, string password, string cPassword)
        {
            //get the response by calling Validate() method
            List<string> response = CreateUser(email, username, password, cPassword);
            Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.TrueString;
            Xamarin.Forms.Application.Current.Properties["current_user"] = response[1];

            return response;

            /*
            string status = response[0];

            if (status == "success")
            {
                return status;
            }
            else
            {
                string reason = response[1];
                return reason;
            }
            */
        }
    }
}
