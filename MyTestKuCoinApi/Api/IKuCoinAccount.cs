namespace MyTestKuCoinApi.Api;

public interface IKuCoinAccount
{
    public string Passphraze { get; set; }
    public string ApiKey { get; set; }
    public string Secret { get; set; }
    public string KuCoinUsername { get; set; }
}