namespace DBSoft.EVEAPI.Entities.Team
{
    using System.Collections.Generic;

    public class Item
    {
        public SolarSystem solarSystem { get; set; }
        public string name { get; set; }
        public string creationTime { get; set; }
        public List<Worker> workers { get; set; }
        public string expiryTime { get; set; }
        public double costModifier { get; set; }
        public string id_str { get; set; }
        public int activity { get; set; }
        public string activity_str { get; set; }
        public int id { get; set; }
        public Specialization2 specialization { get; set; }
    }
}