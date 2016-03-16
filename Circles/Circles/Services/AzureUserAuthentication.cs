using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Circles.Services
{
    public class AzureUserAuthentication : IAuthService
    {
        private readonly IMobileServiceClient _mobileService = MobileServiceClients.AzureMobileService;

        public async Task<bool> Authenticate()
        {
            //Azure Custom authentication
            var loginInput = new JObject { { "userName", "admin" }, { "password", "admin2016" } };

            var loginResult = await _mobileService.InvokeApiAsync("login", loginInput);

            _mobileService.CurrentUser = new MobileServiceUser((string)loginResult["user"])
            {
                MobileServiceAuthenticationToken = (string)loginResult["token"]
            };

            return true;
        }

        public void Logout()
        {
            #if __IOS__
             foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                        {
                            NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                        }
            #endif

            #if __ANDROID__
             CookieManager.Instance.RemoveAllCookie();
            #endif

        }


        //todo make this generic for all types
        //todo add soft delete
        //todo offline sync    ref : https://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-ios-get-started-offline-data/



        Task<bool> IAuthService.Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
