using System;
using System.Linq;
using System.Net;
using TrainTicketWatcher.models.Request;

namespace TrainTicketWatcher.models.Response
{
    public class CustomResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public ResponseFullModel ResponseFullModel { get; set; }
        public bool IsServiceUnAvailable => ResponseContent.Contains("Сервис временно недоступен. Приносим извинения за доставленные неудобства.");
        public bool IsUnsuccessfulResponse => StatusCode != HttpStatusCode.OK || ResponseContent == null;
        public bool IsNoPlaceLeftAtAll => ResponseContent.Contains(@"warning: ""По заданному Вами направлению мест нет""") || !ResponseContent.Contains("places");

        public bool IsFreePlacePresentByTypes(UserInput.UserInput userInput)
        {
            var trainNumbers = userInput.TrainNumbers.Split(',');
            string[] desiredTypesList = userInput.DesiredPlaceTypes.Split(',');

            Func<ListItemModel, bool> trainNumberMatch = listItem => trainNumbers.Contains(listItem.Num);

            var isFreePlace = false;
            foreach (var desiredType in desiredTypesList)
            {
                Func<TypeModel, bool> seatTypeAndCountMatch = type => type.Title.ToLowerInvariant() == desiredType.Trim().ToLowerInvariant() 
                                                                      && int.Parse(type.Places) >= userInput.PlacesCount;

                isFreePlace = isFreePlace || ResponseFullModel.Data.List
                                  .Where(trainNumberMatch)
                                  .Any(listItem => listItem.Types.Any(seatTypeAndCountMatch));
            }

            return isFreePlace;
        } 

        public override string ToString()
        {
            return $"Status code: {this.StatusCode}\r\nContent: {this.ResponseContent}";
        }
    }
}
