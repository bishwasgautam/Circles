using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace Circles.Services
{
    public class AzureDataSyncHandler : IMobileServiceSyncHandler
    {
        public Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            var message = $"Executing operation{operation.Kind} for table {operation.Table.TableName}";
            Debug.WriteLine(message);
            return operation.ExecuteAsync();
        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            var message = $"Push result {result.Status}";
            Debug.WriteLine(message);
          
            foreach (var error in result.Errors)
            {
                message = $"Push error  : {error.Status}";
                Debug.WriteLine(message);
            }

            return Task.FromResult(0);
        }
    }
}