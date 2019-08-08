using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class User
    {
        private String email;
        private String password;
        private String username;

        public User(string email, string password, string username)
        {
            this.email = email;
            this.password = password;
            this.username = username;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public String getEmail()
        {
            return email;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }

        public String getPassword()
        {
            return password;
        }

        public void setUsername(String username)
        {
            this.username = username;
        }

        public String getUsername()
        {
            return username;
        }

        //TODO
        public bool validateEmail()
        {
            return false;
        }

        //TODO
        public bool validatePassword()
        {
            return false;
        }
    }
}
