using System;
namespace Pastime.ViewModels
{
    public class EditAccountViewModel
    {
        private User user;
        private string name;
        private string email;

        public EditAccountViewModel()
        {
            user = new User("steven@hello.com", "password1", "steveny1");
            name = user.getUsername();
            email = user.getEmail();
        }

        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { name = value; } }
    }
}
