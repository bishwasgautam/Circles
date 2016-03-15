using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ModernHttpClient;

namespace Circles
{
    public static class MobileServiceClients
    {

        //private readonly MobileServiceClient MobileService = new MobileServiceClient("https://[yourservice].azure-mobile.net/", "[yourkey]");
        public static readonly MobileServiceClient AzureMobileService = new MobileServiceClient("https://mobilepoc.azurewebsites.net",
            new NativeMessageHandler());
    }
}
