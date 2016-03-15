using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles.Data
{
    public class ServiceLocator
    {
        public static IDataService DataService => new AzureDataService(App.Authenticated);
        public static IUserService UserService => new UserService(App.Authenticated);
        public static IAuthenticate DefaultAuthenticator => new AzureUserAuthentication();
    }


    public interface IDataService
    {
        Task<System.Collections.ObjectModel.ObservableCollection<T>> GetAll<T>();
        Task Pull<T>();
        Task Save<T>(T pEntity);
        Task LoadDummyData();

    }

    public interface IAuthService : IAuthenticate
    {
      
    }

    public interface IAuthenticate
    {
        Task<bool> Authenticate(string username, string password);
    }
}
