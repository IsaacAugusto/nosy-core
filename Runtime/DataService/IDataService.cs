using System.Collections.Generic;

namespace NosyCore.DataService
{
    public interface IDataService
    {
        void Save<T>(T data, bool overwrite = true);
        T Load<T>();
        void Delete<T>(T name);
        void DeleteAll();
        IEnumerable<string> ListAllSaves();
    }
}