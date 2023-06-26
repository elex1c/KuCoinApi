using Newtonsoft.Json;

namespace MyTestKuCoinApi;

public static class AccountData
{
    public static void GetData(string response)
    {
        dynamic d = JsonConvert.DeserializeObject(response);

        int i = 0;
        foreach (var data in d.data)
        {
            string currency = d.data[i].currency;
            string balance = d.data[i].balance;

            double floatCurrency = Convert.ToDouble(balance.Replace('.', ','));

            if (floatCurrency > 0.0001)
                Console.WriteLine($"\t{currency}: {@floatCurrency}");
            
            i++;
        }
    }
}