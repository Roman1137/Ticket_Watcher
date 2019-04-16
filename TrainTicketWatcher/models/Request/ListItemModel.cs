using System.Collections.Generic;

namespace TrainTicketWatcher.models.Request
{
    public class ListItemModel
    {
        public string Num { get; set; }
        public int Category { get; set; }
        public string IsTransformer { get; set; }
        public FromModel From { get; set; }
        public ToModel To { get; set; }
        public List<TypeModel> Types { get; set; }
        public ChildModel Child { get; set; }
        public int AllowStudent { get; set; }
        public int AllowBooking { get; set; }
        public int IsCis { get; set; }
        public int IsEurope { get; set; }
        public int AllowPrivilege { get; set; }
    }
}
