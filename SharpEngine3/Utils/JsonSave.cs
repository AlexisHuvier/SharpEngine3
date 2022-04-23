using Newtonsoft.Json;

namespace SE3.Utils
{
    public class JsonSave
    {
        public static string ObjToJson<T>(T obj) => JsonConvert.SerializeObject(obj);
        public static void SaveObj<T>(T obj, string file) => File.WriteAllText(file, JsonConvert.SerializeObject(obj, Formatting.Indented));
        public static T? JsonToObj<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        public static T? LoadObj<T>(string file) => JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
    }
}
