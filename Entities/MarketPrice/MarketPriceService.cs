
namespace DBSoft.EVEAPI.Entities.MarketPrice
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json;

    public class MarketPriceService
    {
        public IEnumerable<MarketPriceDTO> GetMarketPrices()
        {
            const string url = "http://public-crest.eveonline.com/market/prices/";
            var req = (HttpWebRequest)WebRequest.Create(url);
            var response = req.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                if (stream == null) return null;
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<RootObject>(json);
                return result.items.Select(f => new MarketPriceDTO
                {
                    ItemId = f.type.id,
                    ItemName = f.type.name,
                    AdjustedPrice = (decimal)f.adjustedPrice
                }).ToList();
            }
        }

    }
}
