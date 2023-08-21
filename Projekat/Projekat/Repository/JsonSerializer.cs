using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Repository
{
    public class JsonSerializer<T>
    {
        public void SaveDataToJson(object data, string path)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter streamWriter = new StreamWriter(path))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))

            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonSerializer.Serialize(jsonWriter, data);

            }
        }


        public object LoadDataFromJson(string path)
        {
            List<T> dataList;

            using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
            using (StreamReader sReader = new StreamReader(stream))
            using (JsonReader jReader = new JsonTextReader(sReader))

            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                dataList = jsonSerializer.Deserialize<List<T>>(jReader);
            }

            if (dataList == null)
            {
                dataList = new List<T>();
            }
            return dataList;
        }
    }
}
