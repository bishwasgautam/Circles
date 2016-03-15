using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Circles.Data;
using Circles.Entities;
using FizzWare.NBuilder;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using ModernHttpClient;

namespace Circles
{
    public class AzureDataService : IDataService
    {
        //private readonly MobileServiceClient MobileService = new MobileServiceClient("https://[yourservice].azure-mobile.net/", "[yourkey]");
        private readonly MobileServiceClient _mobileService = new MobileServiceClient("https://mobilepoc.azurewebsites.net", 
            new NativeMessageHandler());
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
                var table = _mobileService.GetSyncTable<T>();
               
                //save data
                await table.InsertAsync(pEntity);

                ////pull down all latest changes
                var key = string.Concat("all", typeof(T).Name, "s");
                await table.PullAsync(key, table.CreateQuery());
                
                //save new records to cloud
                await _mobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
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

        //public async Task SaveChanges<T>()
        //{
        //    await _mobileService.SyncContext.PushAsync();
        //}


        public AzureDataService()
        {
            Initialize();
        }

        private async void Initialize()
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


                await _mobileService.SyncContext.InitializeAsync(store, new AzureDataSyncHandler());

                //push any new record to cloud
                await _mobileService.SyncContext.PushAsync();
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
            var count = 0;
            foreach (var user in members)
            {
                var addressBook = GetDummyAddressBook(user);

                user.AddressBook = addressBook;

                await this.Save<User>(user);

                Debug.WriteLine($"Saved {++count} records");
                
            }


        }

        private IEnumerable<AddressBook> GetDummyAddressBook(User user)
        {
            return Builder<AddressBook>.CreateListOfSize(5).All()
                .With(x => x.WPLHID = user.Id)
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