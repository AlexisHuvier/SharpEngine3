using Newtonsoft.Json;

namespace SE3Launcher.Utils
{
    class SE3Version
    {
        public string version;
        public string link;
    }

    class Versions
    {
        public List<SE3Version> se3Versions;
    }

    internal static class SE3Getter
    {
        public static Versions se3Versions = GetVersions();

        public static async Task DownloadVersion(string version)
        {
            using(var client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(se3Versions.se3Versions.First(e => e.version == version).link))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var file = File.OpenWrite(Path.Join("SE3Versions", version, "file.zip")))
                            stream.CopyTo(file);
                    }
                }
            }
        }

        public static Versions GetVersions()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://alexishuvier.github.io/se3/se3version.json").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<Versions>(responseContent.ReadAsStringAsync().Result);
                }
            }
            return new Versions() { se3Versions = new List<SE3Version>() };
        }
    }
}
