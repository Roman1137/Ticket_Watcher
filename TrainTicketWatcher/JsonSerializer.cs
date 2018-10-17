using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TrainTicketWatcher
{
    public class JsonSerializer
    {
        public static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(InputData));
        public static InputData GetDataFromFile()
        {
            try
            {
                using (FileStream stream = new FileStream("Data.json", FileMode.Open))
                {

                    return jsonSerializer.ReadObject(stream) as InputData;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("В файле ничего нету. Введите данные заново.");
                return new InputData();
            }
        }

        public static void WriteDataToFile(InputData inputData)
        {
            using (FileStream stream = new FileStream("Data.json", FileMode.Create))
            {
                try
                {
                    jsonSerializer.WriteObject(stream, inputData);
                    Console.WriteLine("Данные успешно записаны в файл Data.json");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ОШИБКА\r\nДанные не были записаны в файл Data.json. Попробуйте ещё.");
                    throw;
                }
            }
        }
    }
}
