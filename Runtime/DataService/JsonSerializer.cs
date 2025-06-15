using UnityEngine;

namespace NosyCore.DataService
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj) where T : ISerializable
        {
            return JsonUtility.ToJson(obj, true);
        }

        public T Deserialize<T>(string data) where T : ISerializable
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}