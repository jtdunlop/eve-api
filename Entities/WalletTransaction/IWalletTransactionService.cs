namespace DBSoft.EVEAPI.Entities.WalletTransaction
{
	using Account;

	public interface IWalletTransactionService
	{
		ApiLoadResponse<WalletTransaction> Load(ApiKeyType keyType, int apiKeyId, string vCode, int apiKeyID, int maxAge, long lastTransactionId);
	}
}