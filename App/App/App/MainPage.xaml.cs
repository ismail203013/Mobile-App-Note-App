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
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            
            this.BindingContext = this;
            this.IsBusy = false;
          //  this.Button_Login.Clicked += Button_Login.Clicked;


        }
   
     
        private async void Button_Create(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateUser());
             ErrorLabel.Text = "";
        }
        public void ShowPass(object sender, EventArgs args)
        {
            userpassword.IsPassword = userpassword.IsPassword ? false : true;
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            List<User> UserList = new List<User>();

            UserList = await App.Database.GetUsersAsync();

            foreach (User user in UserList)
            {
                if (user.name == username.Text && user.Password == userpassword.Text)
                {
                    this.IsBusy = true;
                    await Task.Delay(3000);
                    await Navigation.PushAsync(new UserNotes(user.Id , user.name));
                    ErrorLabel.Text = "";

                }
                else
                {
                    ErrorLabel.Text = "USERNAME & PASSWORD DOES NOT EXSIT ";
                }
            }


            username.Text = userpassword.Text = string.Empty;
        }
    }
}