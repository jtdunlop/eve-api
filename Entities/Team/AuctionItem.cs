namespace DBSoft.EVEAPI.Entities.Team
{
    using System.Collections.Generic;

    public class AuctionItem
    {
        public SolarSystem solarSystem { get; set; }
        public Specialization specialization { get; set; }
        public string creationTime { get; set; }
        public List<Worker> workers { get; set; }
        public string expiryTime { get; set; }
        public string activity_str { get; set; }
        public List<object> solarSystemBids { get; set; }
        public string auctionExpiryTime { get; set; }
        public string id_str { get; set; }
        public int activity { get; set; }
        public double costModifier { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
