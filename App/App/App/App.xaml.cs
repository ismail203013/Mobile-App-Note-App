﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database

        {

            get

            {

                if (database == null)

                {

                    database = new Database(Path.Combine(Environment.GetFolderPath

                        (Environment.SpecialFolder.LocalApplicationData),

                        "app1.db3"));

                }

                return database;
            }

        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            // MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
