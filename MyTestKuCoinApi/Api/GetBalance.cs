using System.Security.Cryptography;
using System.Text;

namespace MyTestKuCoinApi.Api;

public class GetBalance
{
    public async Task<string> GetAccountBalance(KuCoinAccount kuCoinAccount, string url)
    {
        try
        {
            string kcApiTimestamp = Convert.ToString(((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() * 1000);
            string kcApiSign = GetApiSign(kuCoinAccount.Secret, "GET", "/api/v1/accounts", kcApiTimestamp);
            string kcApiPassphrase = kuCoinAccount.Passphraze; 
            string kcApiKey = kuCoinAccount.ApiKey;

            string response = await GetUserId(kcApiSign, kcApiPassphrase, kcApiTimestamp, kcApiKey);

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string GetApiSign(string secret, string method, string endpoint, string timestamp)
    {
        string prehash = timestamp + method + endpoint;
        string encryptedSignature = EncryptString(prehash, secret);

        return encryptedSignature;
    }

    private string EncryptString(string prehash, string secret)
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        byte[] prehashBytes = Encoding.UTF8.GetBytes(prehash);

        string base64String = Convert.ToBase64String(HMACSHA256.HashData(secretBytes, prehashBytes));

        return base64String;
    }

    private string EncryptPhrase(string passphrase, string secret)
    {
        using HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
        string encryptedPassPhrase = Convert.ToBase64String(hashBytes);
            
        return encryptedPassPhrase;
    }

    private async Task<string> GetUserId(string kcApiSign, string kcApiPassphrase, string kcApiTimestamp, string kcApiKey)
    {
        using HttpClient _client = new HttpClient();
        
        _client.DefaultRequestHeaders.Add("KC-API-KEY", kcApiKey);
        _client.DefaultRequestHeaders.Add("KC-API-SIGN", kcApiSign);
        _client.DefaultRequestHeaders.Add("KC-API-TIMESTAMP", kcApiTimestamp);
        _client.DefaultRequestHeaders.Add("KC-API-PASSPHRASE", kcApiPassphrase);
        _client.DefaultRequestHeaders.Add("KC-API-KEY-VERSION", "1");

        string response = await _client.GetStringAsync("https://api.kucoin.com/api/v1/accounts");

        return response;
    }
}