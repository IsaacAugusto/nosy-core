using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NosyCore.DataService
{
    public class FileDataService : IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            _dataPath = Application.persistentDataPath;
            _fileExtension = "NosyData";
            _serializer = serializer;
        }
        
        public void Save<T>(T data, bool overwrite = true)
        {
            var typeName = typeof(T).Name;
            var fileLoc = GetPathToFile(typeName);
            
            if (!overwrite && File.Exists(fileLoc))
            {
                throw new IOException($"File already exists at {fileLoc}");
            }
            
            File.WriteAllText(fileLoc, _serializer.Serialize(data));
        }

        public T Load<T>()
        {
            var typeName = typeof(T).Name;
            var fileLoc = GetPathToFile(typeName);
            
            if (!File.Exists(fileLoc))
            {
                throw new FileNotFoundException($"File not found at {fileLoc}");
            }

            return _serializer.Deserialize<T>(File.ReadAllText(fileLoc));
        }

        public void Delete<T>(T name)
        {
            var typeName = typeof(T).Name;
            var fileLoc = GetPathToFile(typeName);
            
            if (!File.Exists(fileLoc))
            {
                throw new FileNotFoundException($"File not found at {fileLoc}");
            }
            
            File.Delete(fileLoc);
        }

        public void DeleteAll()
        {
            foreach (var file in Directory.GetFiles(_dataPath))
            {
                File.Delete(file);
            }
        }

        public IEnumerable<string> ListAllSaves()
        {
            foreach (var path in Directory.EnumerateFiles(_dataPath))
            {
                if (Path.GetExtension(path) == _fileExtension)
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        private string GetPathToFile(string name)
        {
            return Path.Combine(_dataPath, string.Concat(name, ".", _fileExtension));
        }
    }
}