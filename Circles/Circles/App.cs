using System.Diagnostics;
using Circles.Services;
using Circles.ViewModels;
using Circles.Views;
using SQLitePCL;
using TinyIoC;
using Xamarin.Forms;

namespace Circles
{

    public partial class App : Application
    {
        public App()
        {
            //Start
            MainPage = new NavigationPage(new LoginPage());
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

    //Everything related to app startup config
    public partial class App
    {
        #region Dependency Injection
        public static TinyIoCContainer Container { get; set; }
        
        public static void ConfigureIOC()
        {
            //IOC
            Container = TinyIoCContainer.Current;
            
            Container.Register<IDataService, AzureDataService>();
            Container.Register<IAuthService, AzureUserAuthentication>();
            Container.Register<IUserService, UserService>();
            Container.Register<IAuthenticate, AzureUserAuthentication>();
            Container.Register<IDummyDataService, DummyDataService>();
        }
        #endregion

        #region Authentication

        public static IAuthenticate Authenticator { get; private set; }
        private static bool _authenticated;
        public static bool Authenticated
        {
            get { return _authenticated; }
            set
            {
                _authenticated = value;
                if (_authenticated)
                {
                    //display dialog
                    Debug.WriteLine($"*************Successfully Authenticated********************");

                }
            }
        }

        public static void Init(IAuthenticate authenticator)
        {
            if (authenticator != null)
            {
                Authenticator = authenticator;
            }
            else
            {
                Authenticator = ServiceLocator.DefaultAuthenticator;
            }

        }

        #endregion 

    }
}
