using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrainTicketWatcher.models.Request;
using TrainTicketWatcher.models.Response;

namespace TrainTicketWatcher.ApiClient
{
    public class ApliClient
    {
        public async Task<CustomResponse> PostRequest(string url, dynamic userInput)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent q = new FormUrlEncodedContent(GetBodyQueries(userInput));

                using (HttpResponseMessage responseMessage = await client.PostAsync(url, q))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var customResponse = new CustomResponse
                        {
                            StatusCode = responseMessage.StatusCode,
                            ResponseContent = await content.ReadAsStringAsync(),
                            ResponseFullModel = await content.ReadAsAsync<ResponseFullModel>()
                        };

                        Console.WriteLine(customResponse.ToString());

                        return customResponse;
                    }
                }
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetBodyQueries(dynamic userInput)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("from", userInput.From),
                new KeyValuePair<string, string>("to", userInput.To),
                new KeyValuePair<string, string>("date", userInput.Date),
                new KeyValuePair<string, string>("time", userInput.Time)
            };
        }
    }
}
