using UnityEngine;

namespace NosyCore.DataService
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }

        public T Deserialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}