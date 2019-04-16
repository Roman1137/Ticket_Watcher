﻿using System.Linq;
using System.Net;
using TrainTicketWatcher.models.Request;

namespace TrainTicketWatcher.models.Response
{
    public class CustomResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public ResponseFullModel ResponseFullModel { get; set; }
        public bool IsUnsuccessfulResponse => StatusCode != HttpStatusCode.OK || ResponseContent == null;
        public bool IsNoPlaceLeftAtAll => ResponseContent.Contains(@"warning: ""По заданному Вами направлению мест нет""") || !ResponseContent.Contains("places");
        public bool IsKupePlaceLeft => ResponseFullModel.Data.List.Any(listItem => listItem.Types.Any(type => type.Title == "Купе"));
        public bool IsPlatscartPlaceLeft => ResponseFullModel.Data.List.Any(listItem => listItem.Types.Any(type => type.Title == "Плацкарт"));
        public bool IsLuxPlaceLeft => ResponseFullModel.Data.List.Any(listItem => listItem.Types.Any(type => type.Title == "Люкс"));

        public override string ToString()
        {
            return $"Status code: {this.StatusCode}\r\nContent: {this.ResponseContent}";
        }
    }
}