using System.IO;
using Newtonsoft.Json;

namespace Campspot.Repositories
{
    internal class ImportTestCases
    {
        private readonly string _filePath;
        private Rootobject _cachedJObject;

        public ImportTestCases(string filePath)
        {
            _filePath = filePath;
        }

        public Rootobject GetJsonDataIntoObject()
        {
            if (_cachedJObject != null)
            {
               return _cachedJObject;
            }
            using (TextReader textReader = new StreamReader(_filePath))
            using (JsonReader reader = new JsonTextReader(textReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                _cachedJObject = serializer.Deserialize<Rootobject>(reader);
            }
            return _cachedJObject;
        }
    }
}
