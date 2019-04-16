using System;
using TrainTicketWatcher.ApiClient;
using TrainTicketWatcher.Helpers;
using TrainTicketWatcher.models.Response;

namespace TrainTicketWatcher
{
    class Program
    {
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
                ConsoleHelper.WaitMilliseconds(userInput.Pause);
                ConsoleHelper.Clear();

                response = new ApliClient().PostRequest("https://booking.uz.gov.ua/ru/train_search/", userInput).Result;

            } while (response.IsUnsuccessfulResponse || !response.IsKupePlaceLeft);


            BrowserHelper.OpenPageWithTickers(userInput);

            var cancellationToken = ConsoleHelper.StartSound();
            ConsoleHelper.StopSoundByUserInput(cancellationToken);

            Console.ReadLine();
        }
    }
}
