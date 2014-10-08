﻿namespace DBSoft.EVEAPI.Entities.WalletTransaction
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Account;
	using Annotations;
	using Plumbing;

    [UsedImplicitly]
    public class WalletTransactionService : IWalletTransactionService
	{
		public ApiLoadResponse<WalletTransaction> Load(ApiKeyType keyType, int apiKeyId, string vCode, int eveApiID, int maxAge, long lastTransactionId)
		{
			var done = false;
			long? before = null;
			var result = new ApiLoadResponse<WalletTransaction>
			{
				Data = new List<WalletTransaction>(),
			};
			var enough = false;
			while ( !done )
			{
				var kt = keyType == ApiKeyType.Character ? "char" : "corp";
				var service = string.Format("http://api.eve-online.com/{0}/WalletTransactions.xml.aspx?keyID={1}&vCode={2}&characterID={3}", kt, apiKeyId, vCode, eveApiID);
				if ( before != null )
				{
					service += string.Format("&beforeTransID={0}", before);
				}

				var response = EveApiLoader.Load(service);
				if (!response.Success)
				{
					throw new Exception(response.ErrorMessage);
				}
				var xElement = response.Result.Element("rowset");
				if (xElement != null)
				{
					result.CachedUntil = response.CachedUntil;
					if (keyType == ApiKeyType.Character)
					{
						result.Data.AddRange(
							xElement
							.Elements("row")
							.Where(f => f.Attribute("transactionFor").Value == "personal")
							.Select(f => new WalletTransaction
							{
								ID = long.Parse(f.Attribute("transactionID").Value),
								DateTime = DateTime.Parse(f.Attribute("transactionDateTime").Value),
								Quantity = int.Parse(f.Attribute("quantity").Value),
								TypeID = int.Parse(f.Attribute("typeID").Value),
								Type = f.Attribute("transactionType").Value,
								Price = Decimal.Parse(f.Attribute("price").Value)
							}));
					}
					else
					{
						result.Data.AddRange(
							xElement
							.Elements("row")
							.Select(f => new WalletTransaction
							{
								ID = long.Parse(f.Attribute("transactionID").Value),
								DateTime = DateTime.Parse(f.Attribute("transactionDateTime").Value),
								Quantity = int.Parse(f.Attribute("quantity").Value),
								TypeID = int.Parse(f.Attribute("typeID").Value),
								Type = f.Attribute("transactionType").Value,
								Price = Decimal.Parse(f.Attribute("price").Value)
							}));
					}
					if ( xElement.Elements("row").Any() )
					{
						var first = xElement.Elements("row").Min(f => long.Parse(f.Attribute("transactionID").Value));
						enough = xElement.Elements("row").Min(f => DateTime.Parse(f.Attribute("transactionDateTime").Value)) < DateTime.Now.AddDays(-maxAge);
						before = first;
					}
					else
					{
						enough = true;
					}
				}
				done = enough || result.Data.Count == 0 || result.Data[0].ID <= lastTransactionId;
			}
			return result;
		}
	}
}