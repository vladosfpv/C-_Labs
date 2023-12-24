using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static object locker = new();

    static void Main()
    {
        // Считываем акции из файла ticker.txt
        string[] tickers = File.ReadAllLines("ticker.txt");

        Task[] tasks = new Task[tickers.Length];

        for (int i = 0; i < tickers.Length; i++)
        {
            string ticker = tickers[i];
            tasks[i] = Task.Run(() => GetStockData(ticker));
        }

        // Дожидаемся завершения всех задач
        Task.WaitAll(tasks);

        Console.WriteLine("Готово!");
    }

    static void GetStockData(string ticker)
    {
        long startTimestamp = DateTimeOffset.UtcNow.AddYears(-1).ToUnixTimeSeconds();
        long endTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        string url = $"https://query1.finance.yahoo.com/v7/finance/download/{ticker}?period1={startTimestamp}&period2={endTimestamp}&interval=1d&events=history&includeAdjustedClose=true";

        using (WebClient client = new WebClient())
        {
            try
            {
                // Скачиваем данные
                string data = client.DownloadString(url);

                // Пропускаем заголовок
                string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                double[] prices = new double[lines.Length - 1];
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    prices[i - 1] = double.Parse(columns[5]);
                }

                double averagePrice = prices.Length > 0 ? prices.Average() : 0;

                lock (locker)
                {
                    // Записываем результаты в файл
                    using (StreamWriter writer = File.AppendText("result.txt"))
                    {
                        writer.WriteLine($"{ticker}:{averagePrice}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка при получении данных для акции {ticker}: {e.Message}");
            }
        }
    }
}