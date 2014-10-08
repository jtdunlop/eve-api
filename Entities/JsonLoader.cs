namespace DBSoft.EVEAPI.Entities
{
    using System.IO;
    using System.Net;

    public class JsonLoader
    {
        public static string Load(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var response = req.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                if (stream == null) return null;
                using (var buffer = new BufferedStream(stream))
                {
                    var reader = new StreamReader(buffer);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}