using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUser : ContentPage
    {
        public CreateUser()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()

        {

            base.OnAppearing();

            uList.ItemsSource = await App.Database.GetUsersAsync();

        }

        private async void Add_btn(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(userName.Text) && !string.IsNullOrWhiteSpace(password.Text))
            {
                await App.Database.SaveUserAsync(new User
                {
                    name = userName.Text,
                    Password = password.Text,

                });

                
                userName.Text = password.Text = string.Empty;

                uList.ItemsSource = await App.Database.GetUsersAsync();
               


            }
        }
    }
}