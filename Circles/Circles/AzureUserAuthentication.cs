using System.Threading.Tasks;
using Circles.Data;
using FizzWare.NBuilder;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Circles
{
    public class AzureUserAuthentication : IAuthService
    {
        private readonly IMobileServiceClient _mobileService = MobileServiceClients.AzureMobileService;
        
        public AzureUserAuthentication()
        {
            ////Initialize if not already
            //Initialize();

        }


        async Task<bool> IAuthenticate.Authenticate(string username, string password)
        {
            //Azure Custom authentication
            var loginInput = new JObject { { "userName", username}, { "password", password } };

            var loginResult = await _mobileService.InvokeApiAsync("login", loginInput);

            _mobileService.CurrentUser = new MobileServiceUser((string)loginResult["user"])
            {
                MobileServiceAuthenticationToken = (string)loginResult["token"]
            };

            return false;
        }
     

        //todo make this generic for all types
        //todo add soft delete
        //todo offline sync    ref : https://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-ios-get-started-offline-data/
      
    }
}
