using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles.Data
{
    public class ServiceLocator
    {
        public static IDataService DataService => new AzureDataService();
     
    }


    public interface IDataService
    {
        Task<System.Collections.ObjectModel.ObservableCollection<T>> GetAll<T>();
        Task Pull<T>();
        Task Save<T>(T pEntity);
        Task LoadDummyData();

    }
}
