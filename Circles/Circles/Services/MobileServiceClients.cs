using Microsoft.WindowsAzure.MobileServices;
using ModernHttpClient;

namespace Circles.Services
{
    public static class MobileServiceClients
    {

        //private readonly MobileServiceClient MobileService = new MobileServiceClient("https://[yourservice].azure-mobile.net/", "[yourkey]");
        public static readonly MobileServiceClient AzureMobileService = new MobileServiceClient("https://mobilepoc.azurewebsites.net",
            new NativeMessageHandler());
    }
}
