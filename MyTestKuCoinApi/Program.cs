using MyTestKuCoinApi.Api;
using MyTestKuCoinApi.Api;

namespace MyTestKuCoinApi
{
    static class MyTestKuCoinApi 
    {         
        static async Task Main(string[] args)
        {
            GetBalance getBalance = new GetBalance();

            Console.Write("Path (.txt file in format passphrase:api_key:secret:kucoin_username): ");
            string path = Console.ReadLine();

            List<KuCoinAccount> accounts = DataFromFile.GetData(path);
            
            foreach (var account in accounts)
            {
                string response = 
                    await getBalance.GetAccountBalance(account, "https://api.kucoin.com/api/v1/accounts");

                Console.WriteLine("\nAccount name: " + account.KuCoinUsername + "\n");
                
                AccountData.GetData(response);
            }

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}