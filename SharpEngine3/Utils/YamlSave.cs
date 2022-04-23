using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SE3.Utils
{
    public class YamlSave
    {
        public static string ObjToYaml<T>(T obj) => new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Serialize(obj);
        public static void SaveObj<T>(T obj, string file) => File.WriteAllText(file, new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Serialize(obj));
        public static T? YamlToObj<T>(string json) => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Deserialize<T>(json);
        public static T? LoadObj<T>(string file) => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Deserialize<T>(File.ReadAllText(file));
    }
}
