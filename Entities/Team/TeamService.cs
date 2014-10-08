namespace DBSoft.EVEAPI.Entities.Team
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class TeamService : ITeamService
    {
        public IEnumerable<TeamDTO> GetTeams()
        {
            const string url = "http://public-crest.eveonline.com/industry/teams/";
            var json = JsonLoader.Load(url);
            var result = JsonConvert.DeserializeObject<RootObject>(json);
            return result.items.Select(f => new TeamDTO
            {
                TeamId = f.id,
                SolarSystemId = f.solarSystem.id,
                Salary = (decimal)f.costModifier,
                StartDate = DateTime.Parse(f.creationTime).AddDays(7),
                TeamDescription = f.name,
                Bonuses = GetBonuses(f.workers).ToList()
            }).ToList();
        }

        private static IEnumerable<BonusDTO> GetBonuses(IEnumerable<Worker> workers)
        {
            return workers.Select(g => new BonusDTO
            {
                BonusType = g.bonus.bonusType == "ME" ? BonusType.MaterialEfficiency : BonusType.TimeEfficiency,
                BonusValue = (decimal) g.bonus.value,
                SpecialtyId = g.specialization.id
            });
        }

        public List<TeamDTO> GetAuctions()
        {
            const string url = "http://public-crest.eveonline.com/industry/teams/auction/";
            var json = JsonLoader.Load(url);
            var result = JsonConvert.DeserializeObject<AuctionRoot>(json);
            return result.items.Select(f => new TeamDTO
            {
                TeamId = f.id,
                SolarSystemId = f.solarSystem.id,
                StartDate = DateTime.Parse(f.creationTime).AddDays(7),
                TeamDescription = f.name,
                Bonuses = GetBonuses(f.workers).ToList()
            }).ToList();
        }
    }
}
