using System;
using System.Collections.Generic;
using mvvmHelpersDemo.ViewModel;
using Xamarin.Forms;

namespace mvvmHelpersDemo.View
{
    public partial class MonkeyPage : ContentPage
    {

        MonkeyViewModel vm;

        public MonkeyPage()
        {
            InitializeComponent();

            BindingContext = vm = new MonkeyViewModel();
            Ready();
            buttonGetMonkeys.Clicked += ButtonGetMonkeys_Clicked;
            buttonClearMonkeys.Clicked += ButtonClearMonkeys_Clicked;;
        }

        private void Ready()
        {
            buttonGetMonkeys.IsEnabled = true;
            activityIndicatorMonkeys.IsVisible = false;
            activityIndicatorMonkeys.IsRunning = false;
        }

        private void Busy()
        {
            buttonGetMonkeys.IsEnabled = false;
            activityIndicatorMonkeys.IsVisible = true;
            activityIndicatorMonkeys.IsRunning = true;
        }

        private async void ButtonGetMonkeys_Clicked(object sender, EventArgs e)
        {
            Busy();
            await vm.GetMonkeys();
            Ready();
        }

        void ButtonClearMonkeys_Clicked(object sender, EventArgs e)
        {
            Busy();
            vm.ClearMonkeys();
            Ready();
        }

    }
}
