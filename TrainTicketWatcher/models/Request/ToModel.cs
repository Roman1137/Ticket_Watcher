namespace TrainTicketWatcher.models.Request
{
    public class ToModel
    {
        public string Code { get; set; }
        public string Station { get; set; }
        public string StationTrain { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SortTime { get; set; }
    }
}
