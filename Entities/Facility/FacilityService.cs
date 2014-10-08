namespace DBSoft.EVEAPI.Entities.Facility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json;

    public class FacilityService
    {
        public IEnumerable<FacilityDTO> GetFacilities()
        {
            const string url = "http://public-crest.eveonline.com/industry/facilities/";
            var req = (HttpWebRequest)WebRequest.Create(url);
            var response = req.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                if (stream == null) return null;
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<RootObject>(json);
                return result.items.Select(f => new FacilityDTO
                {
                    FacilityId = f.facilityID,
                    FacilityName = f.name,
                    SolarSystemId = f.solarSystem.id,
                    Tax = (decimal) f.tax
                }).ToList();
            }
        }
    }
}

