using System;
using System.Runtime.Serialization;

namespace TrainTicketWatcher
{
    [DataContract]
    public class InputData
    {
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string PauseTimeout { get; set; }

        public bool IsEmpty => From == null;

        public override string ToString()
        {
            return $"From         = {this.From}\r\n" +
                   $"To           = {this.To}\r\n" +
                   $"Date         = {this.Date}\r\n" +
                   $"Time         = {this.Time}\r\n" +
                   $"PauseTimeout = {this.PauseTimeout}\r\n";
        }

        public static InputData GetDataFromUser()
        {
            Console.WriteLine("Введите поле from");
            var from = Console.ReadLine();
            Console.WriteLine("Введите поле to");
            var to = Console.ReadLine();
            Console.WriteLine("Введите поле date в формате 2018-10-19");
            var date = Console.ReadLine();
            Console.WriteLine("Введите поле time в формате 00:00");
            var time = Console.ReadLine();
            Console.WriteLine("Введите интервал паузы в формате 5000");
            var pause = Console.ReadLine();

            return new InputData { From = from, To = to, Date = date, Time = time, PauseTimeout = pause};
        }
    }
}
