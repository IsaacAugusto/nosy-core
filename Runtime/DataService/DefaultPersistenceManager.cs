using NosyCore.EasySingleton;

namespace NosyCore.DataService
{
    public class DefaultPersistenceManager : PersistentMonoBehaviourSingleton<DefaultPersistenceManager>
    {
        private FileDataService _dataService;

        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
        }

        public void SaveData<T>(T data) where T : ISerializable
        {
            _dataService.Save(data);
        }
        
        public T LoadData<T>(T serializable) where T : ISerializable
        {
            return _dataService.Load(serializable);
        }
    }
}