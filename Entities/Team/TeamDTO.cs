namespace DBSoft.EVEAPI.Entities.Team
{
    using System;
    using System.Collections.Generic;

    public class TeamDTO
    {
        public int TeamId { get; set; }
        public int SolarSystemId { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get { return StartDate.AddDays(28); } }
        public List<BonusDTO> Bonuses { get; set; }
        public string TeamDescription { get; set; }
        public List<string> Specialties { get; set; }
    }
}