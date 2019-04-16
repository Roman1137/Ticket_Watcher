using System.Runtime.Serialization;

namespace TrainTicketWatcher.models.UserInput
{
    [DataContract]
    public class UserInput
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
        public int PauseTimeout { get; set; }
        [DataMember]
        public string DesiredPlaceTypes { get; set; }


        public bool IsEmpty => From == null;

        public override string ToString()
        {
            return $"From              = {this.From}\r\n" +
                   $"To                = {this.To}\r\n" +
                   $"Date              = {this.Date}\r\n" +
                   $"Time              = {this.Time}\r\n" +
                   $"PauseTimeout      = {this.PauseTimeout}\r\n" +
                   $"DesiredPlaceTypes = {this.DesiredPlaceTypes}\r\n";
        }
    }
}
