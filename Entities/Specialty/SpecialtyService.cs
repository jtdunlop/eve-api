namespace DBSoft.EVEAPI.Entities.Specialty
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class SpecialtyService
    {
        public static List<SpecialtyDTO> GetSpecialties()
        {
            const string url = "http://public-crest.eveonline.com/industry/specialities/";
            var json = JsonLoader.Load(url);
            var result = JsonConvert.DeserializeObject<RootObject>(json);
            return result.items.Select(f => new SpecialtyDTO
            {
                SpecialtyId = f.id,
                Name = f.name,
                GroupIds = f.groups.Select(g => g.id).ToList()
            }).ToList();
        }
    }
}
