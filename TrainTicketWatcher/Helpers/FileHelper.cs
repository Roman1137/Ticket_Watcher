using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Serilog;
using Serilog.Events;
using TrainTicketWatcher.models.UserInput;

namespace TrainTicketWatcher.Helpers
{
    public class FileHelper
    {
        public static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(UserInput));
        public static UserInput GetDataFromFile()
        {
            try
            {
                using (FileStream stream = new FileStream("Data.json", FileMode.Open))
                {

                    return jsonSerializer.ReadObject(stream) as UserInput;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No info was found in the file 'Data.json'. Enter the date once again.");
                return new UserInput();
            }
        }

        public static void WriteDataToFile(UserInput inputData)
        {
            using (FileStream stream = new FileStream("Data.json", FileMode.Create))
            {
                try
                {
                    jsonSerializer.WriteObject(stream, inputData);
                    Console.WriteLine("The data was successfully written to 'Data.json' file.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR!\r\nThe data was NOT written to file 'Data.json'. Try once again.");
                    throw;
                }
            }
        }

        public static void InitializeFileLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Debug)
                .WriteTo.File("ticket_watcher.logs")
                .CreateLogger();
        }
    }
}
