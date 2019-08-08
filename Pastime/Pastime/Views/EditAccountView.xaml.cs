using System;
using System.Collections.Generic;
using Pastime.ViewModels;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class EditAccountView : ContentPage
    {
        public EditAccountView()
        {
            InitializeComponent();

            //Fake user data
            User user1 = new User("steven@gmail.com", "password1", "steven1");
            User user2 = new User("james@gmail.com", "password1", "steven1");
            User user3 = new User("moe@gmail.com", "password1", "steven1");
            User user4 = new User("jeff@gmail.com", "password1", "steven1");
            User user5 = new User("hello@gmail.com", "password1", "steven1");

            List<User> userList = new List<User>();
            userList.Add(user1);
            userList.Add(user2);
            userList.Add(user3);
            userList.Add(user4);
            userList.Add(user5);


            this.BindingContext = new EditAccountViewModel(userList[0]);
        }
    }
}
