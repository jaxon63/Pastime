﻿using System;
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
            //TODO: At the moment, when a user changes their email, the TargetInvocation exception occurs again
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

        public User User
        {
            get => user;
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

        public bool SaveNewEmail(string email, string password, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                errMsg = "Email cannot be empty";
                return false;
            }
            else
            {
                if (password != User.Password)
                {
                    errMsg = "Incorrect password";
                    return false;
                }
                else
                {
                    errMsg = string.Empty;
                    var request_api = "https://vietnguyen.me/pastime/profile.php";
                    var client = new RestClient(request_api);

                    var request = new RestRequest(Method.GET);

                    request.AddParameter("current_user", current_user);
                    request.AddParameter("action", "change_email");
                    request.AddParameter("email", email);

                    var response = client.Execute(request).Content;

                    var json_response = JObject.Parse(response);
                    JArray items = (JArray)json_response["update_profile"];
                    var item = items[0];

                    if (item["status"].ToString() == "failed")
                    {
                        errMsg = item["reason"].ToString();
                        return false;
                    }
                    else
                    {
                        User.Email = email;
                        //Once the user changes their email, they need to log in again
                        Application.Current.Properties["IsLoggedIn"] = bool.FalseString;
                        Application.Current.Properties["current_user"] = string.Empty;
                        return true;
                    }
                }
            }
        }

        public bool SaveNewPassword(string currentPassword, string newPassword, string cPassword, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(cPassword) || string.IsNullOrWhiteSpace(cPassword))
            {
                errMsg = "Please enter and confirm your current password";
                return false;
            }
            else
            {
                if (currentPassword != User.Password)
                {
                    errMsg = "Incorrect password.";
                    return false;
                }
                else
                {
                    if(newPassword != cPassword)
                    {
                        errMsg = "Passwords do not match.";
                        return false;
                    }
                    errMsg = string.Empty;
                    var request_api = "https://vietnguyen.me/pastime/profile.php";
                    var client = new RestClient(request_api);

                    var request = new RestRequest(Method.GET);

                    request.AddParameter("current_user", current_user);
                    request.AddParameter("action", "change_password");
                    request.AddParameter("current_password", currentPassword);
                    request.AddParameter("new_password", newPassword);
                    request.AddParameter("verify_password", cPassword);

                    var response = client.Execute(request).Content;

                    var json_response = JObject.Parse(response);
                    JArray items = (JArray)json_response["update_profile"];
                    var item = items[0];

                    if (item["status"].ToString() == "failed")
                    {
                        errMsg = item["reason"].ToString();
                        return false;
                    }
                    else
                    {
                        User.Password = newPassword;
                        errMsg = string.Empty;
                        return true;
                    }
                }
            }

        }
    }
}
