namespace DBSoft.EVEAPI.Entities.AccountBalance
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Account;
	using Plumbing;
	using WalletTransaction;

	public class AccountBalanceService : IAccountBalanceService
	{
		public ApiLoadResponse<AccountBalance> Load(ApiKeyType keyType, int keyID, string vCode, int eveApiID)
		{
			var url = string.Format("http://api.eve-online.com/{0}/AccountBalance.xml.aspx?keyID={1}&vCode={2}&characterID={3}", keyType.ServiceBase(), keyID, vCode, eveApiID);
			var result = EveApiLoader.Load(url);

			if (!result.Success)
			{
				throw new Exception(result.ErrorMessage);
			}
			var xElement = result.Result.Element("rowset");
			var response = new ApiLoadResponse<AccountBalance>
			{
				Data = new List<AccountBalance>(),
				CachedUntil = result.CachedUntil
			};
			if (xElement != null)
			{
				response.Data = xElement.Elements("row").Select(f => new AccountBalance
				{
					AccountID = (int) f.Attribute("accountID"),
					AccountKey = (int) f.Attribute("accountKey"),
					Balance = (decimal) f.Attribute("balance")
				}).ToList();
			}
			return response;
		}
	}
	public static class ApiExtensions
	{
		public static string ServiceBase(this ApiKeyType keyType)
		{
			return (keyType == ApiKeyType.Character ? "char" : "corp");
		}
	}

	public interface IAccountBalanceService
	{
		ApiLoadResponse<AccountBalance> Load(ApiKeyType corporation, int apiKeyID, string apiVerificationCode, int accountID);
	}
}
