using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Chrome;

namespace TrainTicketWatcher.Helpers
{
    public class BrowserHelper
    {
        public static void OpenPageWithTickers(dynamic userInput)
        {
            var timeSplit = new List<string>(userInput.Time.Split(':'));

            var driver = new ChromeDriver();
            driver.Url =
                $"https://booking.uz.gov.ua/ru/?" +
                $"from={userInput.From}" +
                $"&to={userInput.To}" +
                $"&date={userInput.Date}" +
                $"&time={timeSplit.First()}" +
                $"%3A{timeSplit.Last()}&url=train-list";
        }
    }
}
