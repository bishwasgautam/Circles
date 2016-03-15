using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Circles.Droid
{
    [Activity(Label = "Circles", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            //Azure mobile services
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Let Xamarin.Forms handle Authentication
            App.Init(null); //Otherwise pass own Authenticator App.Init(IAuthenticator droidAuth);
            //ref https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-xamarin-forms-get-started-users/




            LoadApplication(new App());
        }
    }
}

