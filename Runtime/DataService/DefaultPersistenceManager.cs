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

        public void SaveData<T>(T data)
        {
            _dataService.Save(data);
        }
        
        public T LoadData<T>()
        {
            return _dataService.Load<T>();
        }
    }
}