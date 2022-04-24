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
        
        }
   
     
        private async void Button_Create(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateUser());
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
                   
                    
                    await Navigation.PushAsync(new UserNotes(user.Id , user.name));

                }
            }


            username.Text = userpassword.Text = string.Empty;
        }
    }
}