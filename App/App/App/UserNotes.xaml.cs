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
               // string x = Context.Text;

                await App.Database.SaveUserAsync(new User
                {
                    UserNotes = Context.Text,

                    // UserNotes.Add(x);

                });
                Context.Text = string.Empty;
                nList.ItemsSource = await App.Database.GetUsersAsync();



            }
        }
        User lastSelection;
        void OnMakingSelection(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as User;
            Context.Text = lastSelection.UserNotes;
          
        }

        private async void Button_LogOut (object sender, EventArgs e)
        {
           await Navigation.PushAsync(new MainPage());
        }
        private async void Delete_Btn(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeleteUserAsync(lastSelection);

                nList.ItemsSource = await App.Database.GetUsersAsync();

                Context.Text = "";
               
                /*studyMode.IsChecked = ;*/

            }
        }
        private async void Update_Btn (object sender, EventArgs e)
        {

            if (lastSelection != null)
            {
                lastSelection.UserNotes = Context.Text;
              
                await App.Database.UpdateUserAsync(lastSelection);

                nList.ItemsSource = await App.Database.GetUsersAsync();
            }
        }
    }
}