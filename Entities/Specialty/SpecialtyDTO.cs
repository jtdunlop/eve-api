namespace DBSoft.EVEAPI.Entities.Specialty
{
    using System.Collections.Generic;

    public class SpecialtyDTO
    {
        public int SpecialtyId { get; set; }
        public List<int> GroupIds { get; set; }
        public string Name { get; set; }
    }
}