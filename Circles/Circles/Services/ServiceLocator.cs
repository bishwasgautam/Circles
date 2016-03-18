using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Circles.Entities;
using FizzWare.NBuilder;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Circles.Services
{
    public static class ServiceLocator
    {
        public static IDataService DataService = new AzureDataService();
        public static IDummyDataService DummyDataService = new DummyDataService(DataService);
        public static IUserService UserService = new UserService(DataService);

        public static IAuthenticate DefaultAuthenticator = new AzureUserAuthentication();


    }

    public interface IDummyDataService
    {
        Task LoadDummyData();
        User LoadDummyAdmin();
        AddressBook GetRandomAddressBook();
    }

    public class DummyDataService : IDummyDataService
    {
        private static int count = 0;
        private readonly IDataService _dataService;
        public DummyDataService(IDataService ds)
        {
            _dataService = ds;
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
            var adminUser = LoadDummyAdmin();

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
                if (user.AddressBook == null)
                {
                    var addressBook = GetDummyAddressBook(user);

                    user.AddressBook = addressBook;
                }

                await _dataService.Save<User>(user);

                Debug.WriteLine($"Saved {++count} records");

            }

            
                await _dataService.Sync<User>();


        }

        public User LoadDummyAdmin()
        {
            var user = Builder<User>.CreateNew().With(x => x.AccessLevel = AccessLevel.Admin)
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

            var addressBook = GetDummyAddressBook(user);

            user.AddressBook = addressBook;
            return user;
        }

        public AddressBook GetRandomAddressBook()
        {
            return Builder<AddressBook>.CreateNew()
                .With(x => x.AddressName = "Testing PTR " + ++count)
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

        public IEnumerable<AddressBook> GetDummyAddressBook(User user)
        {
            return Builder<AddressBook>.CreateListOfSize(15).All()
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

    


    public interface IDataService
    {
        Task<System.Collections.ObjectModel.ObservableCollection<T>> GetAll<T>();
        Task Pull<T>();
        Task Save<T>(T pEntity);
        Task Sync<T>();

        IMobileServiceSyncTable<T> Table<T>();
        Task UpdateAsync<T>(T user);
    }

    public interface IAuthService : IAuthenticate
    {
        Task<bool> Authenticate(string username, string password);
    }

    public interface IAuthenticate
    {
        Task<bool> Authenticate();
        void Logout();
    }
}
