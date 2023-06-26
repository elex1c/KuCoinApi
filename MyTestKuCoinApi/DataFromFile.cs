using MyTestKuCoinApi.Api;

namespace MyTestKuCoinApi;

public static class DataFromFile
{
    public static List<KuCoinAccount> GetData(string path)
    {
        int atempts = 0;
        while (!File.Exists(path))
        {
            if (atempts == 3)
                Environment.Exit(0);
                
            Console.Write("You've sent incorrect file path.. \nPath: ");
            path = Console.ReadLine();
            atempts++;
        }
        
        string[] lines = File.ReadAllLines(path);
        List<KuCoinAccount> signingMessageArray = new List<KuCoinAccount>();

        int skippedLines = 0;
        string skippedLinesIndex = "";
        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                string[] piecesOfLine = lines[i].Split(':');

                string passphrase = piecesOfLine[0];
                string apiKey = piecesOfLine[1];
                string secret = piecesOfLine[2];
                string kuCoinUsername = piecesOfLine[3];

                signingMessageArray.Add(new KuCoinAccount() {
                    Passphraze = passphrase,
                    ApiKey = apiKey,
                    Secret = secret,
                    KuCoinUsername = kuCoinUsername
                });
            }
            catch (Exception e)
            {
                skippedLines++;
                skippedLinesIndex += $" {i + 1}";
                continue;
            }
        }

        if (skippedLines > 0)
            Console.WriteLine($"Skipped lines: {skippedLines}. Skipped line indexes:{skippedLinesIndex}");
        
        return signingMessageArray;
    }
}