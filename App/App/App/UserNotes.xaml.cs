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
        int userid;

        List<Notes> AllNotes = new List<Notes>();

        List<Notes> UserNotesx = new List<Notes>();

         
    public UserNotes(int Id)
        {
            InitializeComponent();
            //lblusername.Text = uname;
            userid = Id;
        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();

            AllNotes = await App.Database.GetNotesAsync();

            foreach (Notes n in AllNotes)
            {
                if (userid == n.UserId)
                {
                    UserNotesx.Add(n);

                }
            }

             nList.ItemsSource = UserNotesx;


        }


        private async void Button_Add (object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Context.Text))
            {
            
                await App.Database.SaveNotesAsync(new Notes
                {
                    UserId = userid,
                    UserNotes = Context.Text,
                                    

                }) ;
                Context.Text = string.Empty;
                await Navigation.PushAsync(new UserNotes(userid));
                /* AllNotes.Clear();
                 UserNotesx.Clear();
                 AllNotes = await App.Database.GetNotesAsync();
                 foreach (Notes n in AllNotes)
                 {
                     if (userid == n.UserId)
                     {
                         UserNotesx.Add(n);

                     }
                 }
                 nList.ItemsSource = UserNotesx;*/

            }
        }
        Notes lastSelection;
        void OnMakingSelection(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Notes;
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
                await App.Database.DeleteNotesAsync(lastSelection);

                

                Context.Text = "";
                await Navigation.PushAsync(new UserNotes(userid));
            }
        }
        private async void Update_Btn (object sender, EventArgs e)
        {

            if (lastSelection != null)
            {
                lastSelection.UserNotes = Context.Text;
              
                await App.Database.UpdateNotesAsync(lastSelection);

                await Navigation.PushAsync(new UserNotes(userid));
            }
        }
    }
}