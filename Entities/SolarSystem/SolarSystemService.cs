namespace DBSoft.EVEAPI.Entities.SolarSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json;

    public class SolarSystemService
    {
        public IEnumerable<SolarSystemDTO> GetSolarSystems()
        {
            ServicePointManager.UseNagleAlgorithm = false;
            const string url = "http://public-crest.eveonline.com/industry/systems/";
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy = null;
            var response = req.GetResponse();
            using ( var stream = response.GetResponseStream() )
            {
                if ( stream == null ) return null;
                using (var buffer = new BufferedStream(stream))
                {
                    var reader = new StreamReader(buffer);
                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<RootObject>(json);
                    return result.items.Select(f =>
                    {
                        var systemCostIndice = f.systemCostIndices.SingleOrDefault(g => g.activityID == 1);
                        return new SolarSystemDTO
                        {
                            Name = f.solarSystem.name,
                            Id = f.solarSystem.id,
                            ManufacturingCost = systemCostIndice == null ? (decimal?)null : (decimal)systemCostIndice.costIndex
                        };
                    }).ToList();
                }
            }
        }
    }
}
