namespace DBSoft.EVEAPI.Entities.Blueprint
{
    using System;
    using Account;
    using System.Linq;
    using Plumbing;
    using WalletTransaction;

    public class BlueprintService
	{
        public ApiLoadResponse<Blueprint> Load(ApiKeyType keyType, int apiKeyId, string vCode, int eveApiID)
        {
            var kt = keyType == ApiKeyType.Character ? "char" : "corp";
            var url = string.Format("http://api.eve-online.com/{0}/Blueprints.xml.aspx?keyID={1}&vCode={2}&characterID={3}", kt, apiKeyId, vCode, eveApiID);
            var response = EveApiLoader.Load(url);
            if (!response.Success) throw new Exception(response.ErrorMessage);
            var result = new ApiLoadResponse<Blueprint>
            {
                CachedUntil = response.CachedUntil
            };
            var element = response.Result.Element("rowset");
            if (element != null)
                result.Data.AddRange(element.Elements("row").Select(row => new Blueprint
                {
                    AssetID = long.Parse(row.Attribute("itemID").Value), 
                    BlueprintID = long.Parse(row.Attribute("typeID").Value),
                    IsCopy = int.Parse(row.Attribute("quantity").Value) == -2, 
                    MaterialEfficiency = int.Parse(row.Attribute("materialEfficiency").Value), 
                    TimeEfficiency = int.Parse(row.Attribute("timeEfficiency").Value)
                }));
            return result;
        }
	}
}