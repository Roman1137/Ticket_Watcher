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
        
            Console.WriteLine("Ente 'from' field");
            string from = Console.ReadLine();

            Console.WriteLine("Enter 'to' field");
            string to = Console.ReadLine();

            Console.WriteLine("Enter 'date' field in format: 2018-10-19");
            string date = Console.ReadLine();

            Console.WriteLine("Enter 'time' field in format 00:00");
            string time = Console.ReadLine();

            Console.WriteLine("Enter pause interval in format: 5000");
            string pause = Console.ReadLine();

            // for debugging
            //var userInput = new {From = "2200001", To = "2218000", Date = "2019-04-19", Time = "00:00"};
            //string pause = "1000";
            //for debugging

            return new UserInput {From = from, To = to, Date = date, Time = time, Pause = Int32.Parse(pause)};
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

        public static void WaitMilliseconds(int timeMilliseconds) => Thread.Sleep(timeMilliseconds);

        public static void Clear() => Console.Clear();
    }
}
