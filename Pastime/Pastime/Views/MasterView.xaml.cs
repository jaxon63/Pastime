using Pastime.MenuItems;
using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : MasterDetailPage
    {
        public MasterView()
        {

            InitializeComponent();

            //This should be in a view model but I couldn't get it to work
            MenuList = new List<MasterPageItem>();
            BindingContext = this;

            //Create the data ojects for the pages listed in the menu
            var mainPage = new MasterPageItem() { Title = "Dashboard", Icon = "homeicon.png", TargetType = typeof(MainPage) };
            var loginPage = new MasterPageItem() { Title = "Logout", Icon = "logouticon.png", TargetType = typeof(LoginPage) };
            MenuList.Add(mainPage);
            MenuList.Add(loginPage);

            //Sets the detail portion of the master detail page. Defaults to the first in the list (MainPage)
            //The detail will change on MenuItem clicked
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
        }

        public List<MasterPageItem> MenuList { get; set; }

        //Click listener for the menu
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;

            //Log the user out by setting the current main page to LoginPage and setting IsLoggedIn to false
            if (item.Title == "Logout")
            {
                Application.Current.MainPage = new LoginPage();
                Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.FalseString;
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));   //Other items in the list will trigger the detail to change

                item = null;    //Sets the item to null. I don't know if this makes any difference
               
                IsPresented = false;  //Hides the menu when clicked
            }

        }
    }
}