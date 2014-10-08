namespace DBSoft.EVEAPI.Entities.Specialty
{
    using System.Collections.Generic;

    public class Item
    {
        public string id_str { get; set; }
        public int id { get; set; }
        public List<Group> groups { get; set; }
        public string name { get; set; }
    }
}