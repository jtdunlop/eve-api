namespace DBSoft.EVEAPI.Entities.Team
{
    using System.Collections.Generic;

    public class AuctionRoot
    {
        public string totalCount_str { get; set; }
        public List<AuctionItem> items { get; set; }
        public int pageCount { get; set; }
        public string pageCount_str { get; set; }
        public int totalCount { get; set; }
    }
}
