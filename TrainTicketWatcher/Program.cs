using System;
using Serilog;
using TrainTicketWatcher.ApiClient;
using TrainTicketWatcher.Helpers;
using TrainTicketWatcher.models.Response;
using TrainTicketWatcher.models.UserInput;

namespace TrainTicketWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHelper.InitializeFileLogger();
            ConsoleHelper.SetWindowSize(120,35);
            UserInput tripDataToUse = null;

            UserInput tripDataFromFile = FileHelper.GetDataFromFile();
            if (tripDataFromFile.IsEmpty)
            {
                var tripDataFromUser = ConsoleHelper.GetUserInput();
                tripDataToUse = tripDataFromUser;
                
                var writeEnteredDataToFile = ConsoleHelper.AskUserToWriteEnteredDataToFile();
                if (writeEnteredDataToFile)
                {
                    FileHelper.WriteDataToFile(tripDataFromUser);
                }
            }
            else
            {
                var useDataFromFile = ConsoleHelper.AskUserToUseDataFromFile(tripDataFromFile);
                if (useDataFromFile)
                {
                    tripDataToUse = tripDataFromFile;
                }
                else
                {
                    var dataFromUser = ConsoleHelper.GetUserInput();
                    tripDataToUse = dataFromUser;

                    var writeEnteredDataToFile = ConsoleHelper.AskUserToWriteEnteredDataToFile();
                    if (writeEnteredDataToFile)
                    {
                        FileHelper.WriteDataToFile(dataFromUser);
                    }
                }
            }
            
            CustomResponse response;

            try
            {
                do
                {
                    ConsoleHelper.WaitMilliseconds(tripDataToUse.PauseTimeout);
                    ConsoleHelper.Clear();

                    response = new ApliClient().PostRequest("https://booking.uz.gov.ua/ru/train_search/", tripDataToUse).Result;

                } while (response.IsUnsuccessfulResponse || !response.IsFreePlacePresentByTypes(tripDataToUse.DesiredPlaceTypes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Debug(e.Message);
                throw;
            }

            BrowserHelper.OpenPageWithTickers(tripDataToUse);

            var cancellationToken = ConsoleHelper.StartSound();
            ConsoleHelper.StopSoundByUserInput(cancellationToken);

            Console.ReadLine();
        }
    }
}
