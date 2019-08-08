using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherProfilesUI : ContentPage
    {
        private String hello;
        public OtherProfilesUI()
        {
            InitializeComponent();

            User user = new User("hellO@steven.com", "password1", "steveny1");

            this.BindingContext = new OtherProfilesViewModel(user.getUsername(), "I like cricket and other stuff I guess", "Member since: 2014", "Location: Hawthorn", "Favourite Sport: Cricket" );
        }
        
    }
}