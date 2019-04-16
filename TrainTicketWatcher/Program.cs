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
            var userInput = ConsoleHelper.GetUserInput();

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
