using System;
using System.Collections.Generic;
using Pastime.ViewModels;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class CreateEventView : ContentPage
    {
        public CreateEventView()
        {
            InitializeComponent();
            this.BindingContext =  new CreateEventViewModel();

        }
    }
}
