namespace DBSoft.EVEAPI.Entities.Asset
{
	using System;
	using System.Collections.Generic;
	using System.Xml.Linq;
	using Account;
	using Plumbing;
	using WalletTransaction;

    public class AssetService
	{
        public ApiLoadResponse<Asset> Load(ApiKeyType keyType, int apiKeyId, string vCode, int eveApiID)
		{
            var kt = keyType == ApiKeyType.Character ? "char" : "corp";
            var service = string.Format("http://api.eve-online.com/{0}/AssetList.xml.aspx?keyID={1}&vCode={2}&characterID={3}", kt, apiKeyId, vCode, eveApiID);
            var response = EveApiLoader.Load(service);
		    if (!response.Success) throw new Exception(response.ErrorMessage);
            var result = new ApiLoadResponse<Asset>
			{
				Data = new List<Asset>(),
                CachedUntil = response.CachedUntil
			};
		    RecursiveLoad(result.Data, response.Result.Element("rowset"), (int?)response.Result.Attribute("locationID"));
		    return result;
		}

		// Load flattened list of assets
		private static void RecursiveLoad(ICollection<Asset> assets, XContainer element, int? parentLocation)
		{
			if ( element == null )
			{
				return;
			}
			foreach ( var row in element.Elements("row") )
			{
				var locationID = (int?)row.Attribute("locationID");
				if ( !locationID.HasValue )
				{
					locationID = parentLocation;
				}
				var asset = new Asset
				{
					ID = (long)row.Attribute("itemID"),
					ItemID = (int)row.Attribute("typeID"),
					Quantity = (int)row.Attribute("quantity"),
					LocationID = locationID,
					IsPackaged = (int)row.Attribute("singleton") == 0
				};
				RecursiveLoad(assets, row.Element("rowset"), locationID);
				assets.Add(asset);
			}
		}
	}
}