using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Circles.Constants;
using Circles.Entities;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Circles.Services
{
    public class AzureDataService : IDataService
    {
        
        private readonly IMobileServiceClient _mobileService = MobileServiceClients.AzureMobileService;

        public AzureDataService()
        {
            Initialize();
        }



        protected async void Initialize()
        {
            if (!_mobileService.SyncContext.IsInitialized)
            {
                //local db
                var store = new MobileServiceSQLiteStore(AppConstants.SQLiteDatabasePath);
                //define all tables here
                //todo a namespace can be scanned and automated
                store.DefineTable<User>();
                store.DefineTable<Address>();
                store.DefineTable<AddressBook>();
                store.DefineTable<PhoneInfo>();


                await _mobileService.SyncContext.InitializeAsync(store, new AzureDataSyncHandler());

                //push any new record to cloud
                if (App.Authenticated)
                    await _mobileService.SyncContext.PushAsync();
            }

        }
        public async Task<ObservableCollection<T>> GetAll<T>()
        {

            ObservableCollection<T> theCollection;

            try
            {
                //var theTable = MobileService.GetTable<T>();
                var theTable = _mobileService.GetSyncTable<T>();
                theCollection = await theTable.ToCollectionAsync<T>();
            }
            catch (Exception ex)
            {
                theCollection = null;
                ReportError(ex);
            }

            return theCollection;
        }

        //todo too many requests
        //change it to bulk save to cloud
        public async Task Save<T>(T pEntity)
        {
            try
            {
                //get the local table
                var table = Table<T>();
               
                //save data
                await table.InsertAsync(pEntity);

                if (App.Authenticated)
                    await Sync<T>();
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        private async Task Sync<T>()
        {
            //get the local table
            var table = Table<T>();
            ////pull down all latest changes
            var key = string.Concat("all", typeof(T).Name, "s");

            await table.PullAsync(key, table.CreateQuery());

            //save new records to cloud
            if (App.Authenticated)
                await _mobileService.SyncContext.PushAsync();
        }

        public IMobileServiceSyncTable<T> Table<T>()
        {
            return _mobileService.GetSyncTable<T>();
        }

        //todo add logging
        private void ReportError(Exception exception)
        {
            Debug.WriteLine($"Error while saving :: {exception}");
        }


        public async Task Pull<T>()

        {

            try
            {
                var theTable = _mobileService.GetSyncTable<T>();
                var qKey = $"all{typeof(T).Name}s";
                await theTable.PullAsync(qKey, theTable.CreateQuery());
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }


       
    

    }
}