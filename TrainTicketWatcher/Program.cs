using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace TrainTicketWatcher
{
    class Program
    {
        static async Task<CustomResponse> PostRequest(string url, InputData userInput)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("from", userInput.From),
                new KeyValuePair<string, string>("to", userInput.To),
                new KeyValuePair<string, string>("date", userInput.Date),
                new KeyValuePair<string, string>("time", userInput.Time)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.PostAsync(url, q))
                {
                    
                    using (HttpContent content = responseMessage.Content)
                    {
                        var customResponse = new CustomResponse
                        {
                            StatusCode = responseMessage.StatusCode,
                            ResponseContent = await content.ReadAsStringAsync()
                        };

                        Console.WriteLine(customResponse.ToString());

                        return customResponse;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(120,35);
            InputData data = JsonSerializer.GetDataFromFile();

            if (data.IsEmpty)
            {
                data = InputData.GetDataFromUser();
                JsonSerializer.WriteDataToFile(data);
            }
            else
            {
                Console.WriteLine($"Использовать данные из файла?\r\n{data.ToString()}\r\nИли записать новые?\r\n" +
                                  $"Введите yes - Использовать данные из файла, no - Записать новые данные");
                var answer = Console.ReadLine();
                if (answer == "no")
                {
                    data = InputData.GetDataFromUser();
                    Console.WriteLine("Записать новые данные в файл?\r\n Введите yes- да, no - нет.");
                    var writeDateToFile = Console.ReadLine();
                    if (writeDateToFile == "yes")
                    {
                        JsonSerializer.WriteDataToFile(data);
                    }
                }
            }
            
            CustomResponse response;
            do
            {

                Thread.Sleep(Int32.Parse(data.PauseTimeout));
                Console.Clear();

                Console.WriteLine(data.ToString());
                response = PostRequest("https://booking.uz.gov.ua/ru/train_search/", data).Result;        
            } while (response.StatusCode != HttpStatusCode.OK ||
                     response.ResponseContent == null ||
                     response.ResponseContent.Contains(@"warning: ""По заданному Вами направлению мест нет""") ||
                     !response.ResponseContent.Contains("places"));

            var driver = new ChromeDriver();
            driver.Url =
                $"https://booking.uz.gov.ua/ru/?" +
                $"from={data.From}" +
                $"&to={data.To}" +
                $"&date={data.Date}" +
                $"&time={data.Time.Split(':').First()}" +
                $"%3A{data.Time.Split(':').Last()}&url=train-list";


            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.Beep();
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, ct);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Для остановки звукового сигнала НАЖМИТЕ ПРОБЛЕЛ и ENTER");
            var stop = Console.ReadLine();
            if (stop != null)
            {
                ts.Cancel();
            }

            Console.ReadLine();
        }
    }
}
