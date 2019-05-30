﻿using System;
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

            do
            {
                ConsoleHelper.WaitMilliseconds(tripDataToUse.PauseTimeout);
                ConsoleHelper.Clear();

                response = new ApliClient().PostRequest("https://booking.uz.gov.ua/ru/train_search/", tripDataToUse).Result;

            } while (response.IsUnsuccessfulResponse || !response.IsFreePlacePresentByTypes(tripDataToUse));


            BrowserHelper.OpenPageWithTickers(tripDataToUse);

            var cancellationToken = ConsoleHelper.StartSound();
            ConsoleHelper.StopSoundByUserInput(cancellationToken);

            Console.ReadLine();
        }
    }
}
