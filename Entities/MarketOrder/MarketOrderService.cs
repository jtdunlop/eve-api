namespace DBSoft.EVEAPI.Entities.MarketOrder
{
	using System;
	using System.Linq;
	using Account;
	using Plumbing;
	using WalletTransaction;

    public class MarketOrderService
	{
        public ApiLoadResponse<MarketOrder> Load(ApiKeyType keyType, int apiKeyId, string vCode, int eveApiID)
        {
            var kt = keyType == ApiKeyType.Character ? "char" : "corp";
            var url = string.Format("http://api.eve-online.com/{0}/MarketOrders.xml.aspx?keyID={1}&vCode={2}&characterID={3}", kt, apiKeyId, vCode, eveApiID);
            var response = EveApiLoader.Load(url);
            if (!response.Success) throw new Exception(response.ErrorMessage);
            var result = new ApiLoadResponse<MarketOrder>
            {
                CachedUntil = response.CachedUntil
            };
            var element = response.Result.Element("rowset");
            if (element != null)
                result.Data.AddRange(element.Elements("row").Select(f => new MarketOrder
					{
						ID = (long) f.Attribute("orderID"),
						CharacterID = (int) f.Attribute("charID"),
						StationID = (int) f.Attribute("stationID"),
						VolumeEntered = (int) f.Attribute("volEntered"),
						VolumeRemaining = (int) f.Attribute("volRemaining"),
						MinimumVolume = (int) f.Attribute("minVolume"),
						OrderState = (OrderState) (int) f.Attribute("orderState"),
						ItemID = (int) f.Attribute("typeID"),
						Range = (short) f.Attribute("range"),
						AccountKey = (int) f.Attribute("accountKey"),
						Duration = (int) f.Attribute("duration"),
						Escrow = (decimal) f.Attribute("escrow"),
						Price = (decimal) f.Attribute("price"),
						OrderType = (bool) f.Attribute("bid") ? OrderType.Buy : OrderType.Sell,
						WhenIssued = (DateTime) f.Attribute("issued")
					}));
			return result;
		}
	}
}
