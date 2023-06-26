namespace MyTestKuCoinApi.Api;

public class KuCoinAccount : IKuCoinAccount
{
    public string Passphraze { get; set; }
    public string ApiKey { get; set; }
    public string Secret { get; set; }
    public string KuCoinUsername { get; set; }
}