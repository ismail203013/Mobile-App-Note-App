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
        INotificationManager notificationManager;
        int notificationNumber;

        public CreateUser()
        {
            InitializeComponent();
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }
       
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
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
                notificationNumber++;
                string title = $"Luxe Notes #{notificationNumber}";
                string message = $" Account made succesfully :) ";
                notificationManager.ScheduleNotification(title, message);

                uList.ItemsSource = await App.Database.GetUsersAsync();
               


            }
        }
    }
}