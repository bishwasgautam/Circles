using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.OS;
using Android.Webkit;
using Circles.Services;

namespace Circles.Droid
{
    [Activity(Theme = "@android:style/Theme.Material.Light",
        Label = "Circles", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            //Azure mobile services
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            

            //Let Xamarin.Forms handle Authentication
            App.Init((IAuthenticate)this); //Otherwise pass own Authenticator App.Init(IAuthenticate droidAuth);
                                           //ref https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-xamarin-forms-get-started-users/

            UserDialogs.Init(this);

            LoadApplication(new App());
        }

        // Define a authenticated user.
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await MobileServiceClients.AzureMobileService.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook);
                CreateAndShowDialog(string.Format("you are now logged in - {0}",
                    user.UserId), "Logged in!");

                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        public void Logout()
        {
            CookieManager.Instance.RemoveAllCookie();
        }

        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}

