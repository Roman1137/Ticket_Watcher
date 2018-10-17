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
    public class CustomResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public override string ToString()
        {
            return $"Status code: {this.StatusCode}\r\nContent: {this.ResponseContent}";
        }
    }
    class Program
    {
        static async Task<CustomResponse> PostRequest(string url, dynamic userInput)
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
            Console.WriteLine("Введите поле from");
            string from = Console.ReadLine();
            Console.WriteLine("Введите поле to");
            string to = Console.ReadLine();
            Console.WriteLine("Введите поле date в формате 2018-10-19");
            string date = Console.ReadLine();
            Console.WriteLine("Введите поле time в формате 00:00");
            string time = Console.ReadLine();
            Console.WriteLine("Введите интервал паузы в формате 5000");
            string pause = Console.ReadLine();

            var userInput = new {From = from, To = to, Date = date, Time = time};
            CustomResponse response;
            do
            {

                Thread.Sleep(Int32.Parse(pause));
                Console.Clear();

                response = PostRequest("https://booking.uz.gov.ua/ru/train_search/", userInput).Result;        
            } while (response.StatusCode != HttpStatusCode.OK ||
                     response.ResponseContent == null ||
                     response.ResponseContent.Contains(@"warning: ""По заданному Вами направлению мест нет""") ||
                     !response.ResponseContent.Contains("places"));

            var driver = new ChromeDriver();
            driver.Url =
                $"https://booking.uz.gov.ua/ru/?" +
                $"from={userInput.From}" +
                $"&to={userInput.To}" +
                $"&date={userInput.Date}" +
                $"&time={userInput.Time.Split(':').First()}" +
                $"%3A{userInput.Time.Split(':').Last()}&url=train-list";


            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.Beep();
                    if (ct.IsCancellationRequested)
                    {
                        // another thread decided to cancel
                        break;
                    }
                }
            }, ct);

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
