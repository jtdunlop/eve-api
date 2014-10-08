namespace DBSoft.EVEAPI.Entities.Team
{
    using System.Collections.Generic;

    public interface ITeamService
    {
        IEnumerable<TeamDTO> GetTeams();
        List<TeamDTO> GetAuctions();
    }
}