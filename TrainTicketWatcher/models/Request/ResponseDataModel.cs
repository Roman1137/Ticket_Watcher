using System.Collections.Generic;

namespace TrainTicketWatcher.models.Request
{
    public class ResponseDataModel
    {
        public List<ListItemModel> List { get; set; }
        public string Warning { get; set; }
    }
}
