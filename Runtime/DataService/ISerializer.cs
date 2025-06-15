namespace NosyCore.DataService
{
    public interface ISerializer 
    {
        string Serialize<T>(T obj) where T : ISerializable;
        T Deserialize<T>(string data) where T : ISerializable;
    }

    public interface ISerializable
    {
        public string FileName { get; private set }
        public string SerializeData();
        public bool DeserializeData(string data);
    }
}