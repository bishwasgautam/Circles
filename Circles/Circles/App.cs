using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Circles.Data;
using Xamarin.Forms;

namespace Circles
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
            LoadData();

            MainPage = new NavigationPage(new LoginPage());
            

        }

        private static async void LoadData()
        {
             await ServiceLocator.DataService.LoadDummyData();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
