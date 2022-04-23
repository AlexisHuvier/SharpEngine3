using System.Net;
using Newtonsoft.Json;

namespace SE3Launcher.Utils
{
    class Article
    {
        [JsonProperty("title_fr-FR")]
        public string title_fr;
        [JsonProperty("title_en-US")]
        public string title_en;
        [JsonProperty("lines_fr-FR")]
        public List<string> lines_fr;
        [JsonProperty("lines_en-US")]
        public List<string> lines_en;
    }

    class News
    {
        public List<Article> articles;
    }

    internal static class NewsGetter
    {
        public static News News = GetNews();

        public static News GetNews()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://alexishuvier.github.io/se3/news.json").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<News>(responseContent.ReadAsStringAsync().Result);
                }
            }
            return new News() { articles = new List<Article>() };
        }
    }
}
