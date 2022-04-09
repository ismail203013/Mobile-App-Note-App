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
    public partial class UserNotes : ContentPage
    {
      
        public UserNotes(int uname)
        {
            InitializeComponent();
            //lblusername.Text = uname;
        }
        protected override async void OnAppearing()

        {

            base.OnAppearing();
            

            nList.ItemsSource = await App.Database.GetUsersAsync();

        }


        private async void Button_Add (object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Context.Text))
            {
                string x = Context.Text;

                await App.Database.SaveUserAsync(new User
                {

            //    UserNotes.Add(x);

            });
                Context.Text = string.Empty;
                nList.ItemsSource = await App.Database.GetUsersAsync();



            }
        }
        User lastSelection;
        void OnMakingSelection(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as User;
          //  Context.Text = lastSelection.UserNotes;
          
        }

        private async void Button_LogOut (object sender, EventArgs e)
        {
           await Navigation.PushAsync(new MainPage());
        }
        private  void Delete_Btn(object sender, EventArgs e)
        {
           
        }
        private  void Update_Btn (object sender, EventArgs e)
        {
           
        }
    }
}