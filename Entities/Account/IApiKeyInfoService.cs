namespace DBSoft.EVEAPI.Entities.Account
{
	using System.Collections.Generic;

	public interface IApiKeyInfoService
	{
		IEnumerable<ApiKeyInfo> Load(int keyID, string vCode);
	}
}