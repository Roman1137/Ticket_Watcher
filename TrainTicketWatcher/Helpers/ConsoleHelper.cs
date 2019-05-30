using System;
using System.Threading;
using System.Threading.Tasks;
using TrainTicketWatcher.models.UserInput;

namespace TrainTicketWatcher.Helpers
{
    public class ConsoleHelper
    {
        public static UserInput GetUserInput()
        {

            Console.WriteLine("Enter 'from' field");
            string from = Console.ReadLine();

            Console.WriteLine("Enter 'to' field");
            string to = Console.ReadLine();

            Console.WriteLine("Enter 'date' field in format: 2018-10-19");
            string date = Console.ReadLine();

            Console.WriteLine("Enter 'time' field in format 00:00");
            string time = Console.ReadLine();

            Console.WriteLine("Enter pause interval in format: 5000");
            string pause = Console.ReadLine();

            Console.WriteLine("Enter desired types of places. For example: плацкарт,купе,люкс");
            string placeTypes = Console.ReadLine();

            Console.WriteLine("Enter train numbers. For example: 002Д,705K,715К - using Russian symbols");
            string trainNumber = Console.ReadLine();

            Console.WriteLine("Enter count of places");
            int placesCount = int.Parse(Console.ReadLine());

            return new UserInput
            {
                From = from,
                To = to,
                Date = date,
                Time = time,
                PauseTimeout = Int32.Parse(pause),
                DesiredPlaceTypes = placeTypes,
                TrainNumbers = trainNumber,
                PlacesCount = placesCount
            };
        }

        public static CancellationTokenSource StartSound()
        {
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

            return ts;
        }

        public static void StopSoundByUserInput(CancellationTokenSource ts)
        {
            Console.WriteLine("Для остановки звукового сигнала НАЖМИТЕ ПРОБЛЕЛ и ENTER");
            var stop = Console.ReadLine();
            if (stop != null)
            {
                ts.Cancel();
            }
        }

        public static bool AskUserToUseDataFromFile(UserInput dataFromFile)
        {
            Console.WriteLine($"Use data from file 'Data.json'?\r\n{dataFromFile.ToString()}\r\nOr use another data?\r\n\n" +
                               $"Enter 'yes' or 'no' WITHOUT Quotation mark.\r\n" +
                               $"'yes' means using data from 'Data.json' file.\r\n" +
                               $"'no' means using new data and possibility to write it to file.\r\n");
            var useDataFromFile = Console.ReadLine();

            return useDataFromFile.ToLowerInvariant() == "yes";
        }

        public static bool AskUserToWriteEnteredDataToFile()
        {
            Console.WriteLine("Write new data to file?\r\nEnter 'yes' or 'no' WITHOUT Quotation mark.");
            var writeDateToFile = Console.ReadLine();

            return writeDateToFile.ToLowerInvariant() == "yes";
        }

        public static void WaitMilliseconds(int timeMilliseconds) => Thread.Sleep(timeMilliseconds);

        public static void Clear() => Console.Clear();

        public static void SetWindowSize(int width, int height) => Console.SetWindowSize(120, 35);
    }
}
