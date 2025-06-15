using System.Collections.Generic;

namespace NosyCore.DataService
{
    public interface IDataService
    {
        void Save<T>(T data, bool overwrite = true) where T : ISerializable;
        T Load<T>(T serializable) where T : ISerializable;
        void Delete<T>(T name) where T : ISerializable;
        void DeleteAll();
        IEnumerable<string> ListAllSaves();
    }
}