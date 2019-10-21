using Pastime.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeEmailPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ChangeEmailPopup()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<EditAccountViewModel>(this, "updated", async (sender) =>
            {
                await PopupNavigation.Instance.PopAsync();

            });

        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            MessagingCenter.Send(Application.Current, "navigateBack");
        }


    }
}