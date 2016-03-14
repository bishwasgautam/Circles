using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Circles.Data;
using Circles.Entities;
using FizzWare.NBuilder;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

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
            MobileService = new MobileServiceClient("http://mobilepoc.azurewebsites.net");

            const string path = "dunnmobilepoc.db";

            if (!MobileService.SyncContext.IsInitialized)
            {
                //setup our local sqlite store and intialize our table
                var store = new MobileServiceSQLiteStore(path);

                store.DefineTable<User>();

                //enable local sync
                await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

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
            //push all newly created and modified changes
            await MobileService.SyncContext.PushAsync();

            //pull down all latest changes and then push all users up
            key = !string.IsNullOrEmpty(key) ? string.Concat("all", key, "s") : null;
            await _userTable.PullAsync(key, _userTable.CreateQuery());

        }


        public User GetUser(Guid userId)
        {
            return GetUsers().Result.Cast<User>().First(x => x.Id == userId);
        }

        public Guid Authenticate(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public void ForgotLogin(string email, Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetCirclesByUserId(Guid userId)
        {
            return (List<User>)GetUsers().Result.Cast<User>().First(x => x.Id == userId).Circle;
        }

        public List<AddressBookPage> GetAddressBook(Guid userId)
        {
            return (List<AddressBookPage>)GetUsers().Result.Cast<User>().First(x => x.Id == userId).AddressBook;
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

    public class AzureDataService : IDataService
    {
        //private readonly MobileServiceClient MobileService = new MobileServiceClient("https://[yourservice].azure-mobile.net/", "[yourkey]");
        private readonly MobileServiceClient _mobileService = new MobileServiceClient("http://mobilepoc.azurewebsites.net");
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


        public async Task Save<T>(T pEntity)
        {
            try
            {
                //var theTable = MobileService.GetTable<T>();
                var theTable = _mobileService.GetSyncTable<T>();
                await theTable.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        //todo add logging
        private void ReportError(Exception exception)
        {

        }


        public async Task Pull<T>()

        {

            try
            {
                var theTable = _mobileService.GetSyncTable<T>();
                await theTable.PullAsync(null, theTable.CreateQuery());
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }


        public AzureDataService()
        {
            Initialise();
        }

        private async void Initialise()
        {
            if (!_mobileService.SyncContext.IsInitialized)
            {
                //local db
                var store = new MobileServiceSQLiteStore("local.db");

                //define all tables here
                //todo a namespace can be scanned and automated
                store.DefineTable<User>();
                store.DefineTable<Address>();
                store.DefineTable<AddressBook>();
                store.DefineTable<PhoneInfo>();


                await _mobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
        }

        public async Task LoadDummyData()
        {
            await LoadDummyUsers();
            //LoadDummyAddresses();
            //LoadDummyPhoneInfo();
            //LoadDummyAddressBooks();
            //LOadDummyPasswordRecovery();

        }

        private async Task LoadDummyUsers()
        {
            var adminUser = Builder<User>.CreateNew().With(x => x.AccessLevel = AccessLevel.Admin)
                .With(x => x.FirstName = Faker.Name.First())
                .With(x => x.LastName = Faker.Name.Last())
                .With(x => x.BirthDate = Faker.Date.Birthday())
                .With(x => x.Phone = new PhoneInfo()
                {
                    CellPhone = Faker.Phone.CellNumber(),
                    Home = Faker.Phone.Number()
                })
                .With(x => x.UserName = "admin")
                .With(x => x.Password = "admin2016")
                .With(x => x.PrimaryEmail = Faker.Internet.Email())
                .With(x => x.SecondaryEmail = Faker.Internet.Email())
                .Build();

            var members = Builder<User>.CreateListOfSize(10).All().With(x => x.AccessLevel = AccessLevel.Member)
                 .With(x => x.PrimaryEmail = Faker.Internet.Email())
                 .With(x => x.SecondaryEmail = Faker.Internet.Email())
                  .With(x => x.FirstName = Faker.Name.First())
                .With(x => x.LastName = Faker.Name.Last())
                .With(x => x.BirthDate = Faker.Date.Birthday())
                .With(x => x.Phone = new PhoneInfo()
                {
                    CellPhone = Faker.Phone.CellNumber(),
                    Home = Faker.Phone.Number()
                })
                 .With(x => x.Address = new Address()
                 {
                     City = Faker.Address.City(),
                     State = Faker.Address.State(),
                     StreetName = Faker.Address.StreetName(),
                     Country = Faker.Address.Country(),
                     ZipCode = Faker.Address.ZipCode(),
                     Suite = Faker.Address.BuildingNumber()
                 }).Build();

            members.Add(adminUser);

            foreach (var user in members)
            {
                var addressBook = GetDummyAddressBook(user);

                user.AddressBook = addressBook;

                await this.Save<User>(user);
            }


        }

        private IEnumerable<AddressBook> GetDummyAddressBook(User user)
        {
            return Builder<AddressBook>.CreateListOfSize(5).All()
                     .With(x => x.User = user)
                     .With(x => x.AddressName = Faker.Name.First() + " " + Faker.Name.Last())
                     .With(x => x.Address = new Address()
                     {
                         City = Faker.Address.City(),
                         State = Faker.Address.State(),
                         StreetName = Faker.Address.StreetName(),
                         Country = Faker.Address.Country(),
                         ZipCode = Faker.Address.ZipCode(),
                         Suite = Faker.Address.BuildingNumber()
                     }).Build();
        }
    }
}
