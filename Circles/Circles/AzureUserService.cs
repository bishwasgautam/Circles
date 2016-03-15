using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circles.Entities;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using ModernHttpClient;

namespace Circles
{
    public class AzureUserService
    {
        public MobileServiceClient MobileService { get; set; }
        private IMobileServiceSyncTable<User> _userTable;


        //todo make this generic for all types
        //todo add soft delete
        public async Task Initialize()
        {
            //Create our client
            //todo move url to app settings
            MobileService = new MobileServiceClient("https://mobilepoc.azurewebsites.net", new NativeMessageHandler());

            const string path = "dunnmobilepoc.db";

            if (!MobileService.SyncContext.IsInitialized)
            {
                //setup our local sqlite store and intialize our table
                var store = new MobileServiceSQLiteStore(path);

                store.DefineTable<User>();

                //enable local sync
                //await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
                await MobileService.SyncContext.InitializeAsync(store, new AzureDataSyncHandler()); 

                //Get our sync table that will call out to azure
                _userTable = MobileService.GetSyncTable<User>();

            }
        }

        public async Task<IEnumerable> GetUsers()
        {
            await SyncUser("User");
            return await _userTable.OrderBy(c => c.BirthDate).ToEnumerableAsync();
        }

        public async Task AddUser(User user)
        {
            await _userTable.InsertAsync(user);

            //Synchronize user
            await SyncUser();
        }

        //ref : https://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-ios-get-started-offline-data/
        public async Task SyncUser(string key = "", bool isIncremental = true)
        {
           //pull down all latest changes and then push all users up
            key = !string.IsNullOrEmpty(key) ? string.Concat("all", key, "s") : null;
            await _userTable.PullAsync(key, _userTable.CreateQuery());

            //push all newly created and modified changes
            await MobileService.SyncContext.PushAsync();
        }


        public User GetUser(string userId)
        {
            return GetUsers().Result.Cast<User>().First(x => string.Equals(x.Id,userId, StringComparison.CurrentCulture));
        }

        public Guid Authenticate(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public void ForgotLogin(string email, Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetCirclesByUserId(string userId)
        {
            return (List<User>)GetUsers().Result.Cast<User>().First(x => string.Equals(x.Id, userId, StringComparison.CurrentCulture)).Circle;
        }

        public List<AddressBookPage> GetAddressBook(string userId)
        {
            return (List<AddressBookPage>)GetUsers().Result.Cast<User>().First(x => string.Equals(x.Id, userId, StringComparison.CurrentCulture)).AddressBook;
        }

        public bool AddToCircle(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddToCircle(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCircle(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCircle(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            _userTable.UpdateAsync(user);
            return true;
        }

        public bool RemoveUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    //To log and handle data sync errors
}
