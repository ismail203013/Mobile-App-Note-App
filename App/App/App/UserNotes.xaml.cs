using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserNotes : ContentPage
    {
        int userid;
        string uname;
         int lan;
        int lon;
        

        List<Notes> AllNotes = new List<Notes>();

        List<Notes> UserNotesx = new List<Notes>();

      

        public static string photofilename;
        public static string imagePath;
        static SQLiteConnection db;
        Notes notes = new Notes();
   
        
        public UserNotes(int Id, string name)
        {
            InitializeComponent();

            uname = name;

            lblusername.Text = name + ",";

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
                    Location = loc.Text,
                    Pic = imagePath,
                    

                }) ;
                Context.Text = string.Empty;
                imagePath = string.Empty;
                await Navigation.PushAsync(new UserNotes(userid, uname));
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
            loc.Text = lastSelection.Location;
            imagePath = lastSelection.Pic;
            resultImage.Source = imagePath;
        //  resultImage.Source = imagePath;

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
                //resultImage = null;
                await Navigation.PushAsync(new UserNotes(userid, uname));
            }
            
        }


      
        private async void Update_Btn (object sender, EventArgs e)
        {

            if (lastSelection != null)
            {
                lastSelection.UserNotes = Context.Text;

                lastSelection.Location = loc.Text;

                lastSelection.Pic = imagePath;
              
                await App.Database.UpdateNotesAsync(lastSelection);

                await Navigation.PushAsync(new UserNotes(userid, uname));
            }
        }

        

      public  async void Button_get_loc(object sender, EventArgs e)
        {
            Location theVariable = await Geolocation.GetLocationAsync(
            new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
             lan = (int)theVariable.Latitude;
             lon = (int)theVariable.Longitude;
            loc.Text = "Latitude: " + theVariable.Latitude.ToString() + "    Longitude:" + theVariable.Longitude.ToString();
           
        }

       
         async void Button_Clicked_Map(object sender, EventArgs e)
        {
            try
            {
                if (loc.Text != null)
                {
                    await Map.OpenAsync(lan , lon);
                    //await Navigation.PushAsync(new UserNotes(userid));
                }
                else
                {
                    await Navigation.PushAsync(new UserNotes(userid, uname));
                }

            }
            catch { }


        }

        async void Button_Picture(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            
            if ( photo != null)
            {
                LoadPhotoAsync(photo);
                photofilename = photo.FileName;
                imagePath = Path.Combine(FileSystem.AppDataDirectory, photofilename);

                var stream = await photo.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(()=> stream);
            }

        }
        async void LoadPhotoAsync(FileResult photo)
        {
            // canceled
            string PhotoPath;
            if (photo == null)
            {

                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;

        }


    }
}