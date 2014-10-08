namespace DBSoft.EVEAPI.Entities.SolarSystem
{
    using System.Collections.Generic;

    public class Item
    {
        public List<SystemCostIndice> systemCostIndices { get; set; }
        public SolarSystem solarSystem { get; set; }
    }
}