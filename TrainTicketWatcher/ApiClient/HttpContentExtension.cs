using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrainTicketWatcher.ApiClient
{
    public static class HttpContentExtension
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            var contentModel = JsonConvert.DeserializeObject<T>(stringContent);

            return contentModel;
        }
    }
}
