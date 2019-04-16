using OpenQA.Selenium.Chrome;

namespace TrainTicketWatcher.Helpers
{
    public class BrowserHelper
    {
        public static void OpenPageWithTickers(dynamic userInput)
        {
            var driver = new ChromeDriver();
            driver.Url =
                $"https://booking.uz.gov.ua/ru/?" +
                $"from={userInput.From}" +
                $"&to={userInput.To}" +
                $"&date={userInput.Date}" +
                $"&time={userInput.Time.Split(':').First()}" +
                $"%3A{userInput.Time.Split(':').Last()}&url=train-list";
        }
    }
}
