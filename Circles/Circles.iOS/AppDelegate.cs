using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Circles.Services;

namespace Circles.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            //Azure mobile services
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();

            //basic style bar and light styling
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(43, 132, 211);
            //bar background 
            UINavigationBar.Appearance.TintColor = UIColor.White;
            //Tint color of button items 
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f), TextColor = UIColor.White });
            //See more at: http://motzcod.es/post/125302147562/5-tips-to-properly-style-xamarinforms-apps#sthash.jRA2yYVa.dpuf

            //Let Xamarin.Forms handle Authentication
            App.Init((IAuthenticate)this); //Otherwise pass own Authenticator App.Init(IAuthenticator iOSAuth);
            //ref https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-xamarin-forms-get-started-users/

            
            LoadApplication(new App());
            

            return base.FinishedLaunching(app, options);
        }

        // Define a authenticated user.
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                if (user == null)
                {
                    user = await MobileServiceClients.AzureMobileService.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
                        MobileServiceAuthenticationProvider.Facebook);
                    if (user != null)
                    {
                        UIAlertView avAlert = new UIAlertView("Authentication", "You are now logged in " + user.UserId, null, "OK", null);
                        avAlert.Show();
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                UIAlertView avAlert = new UIAlertView("Authentication failed", ex.Message, null, "OK", null);
                avAlert.Show();
            }
            return success;
        }

        public void Logout()
        {
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
        }
    }
}
