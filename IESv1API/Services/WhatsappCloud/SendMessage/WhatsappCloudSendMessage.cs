using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace IESv1API.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage: IWhatsappCloudSendMessage
    {

        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();

            //var json = "{ \"messaging_product\": \"whatsapp\", \"to\": \"51944043423\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";

            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            //var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));

            using (var content = new ByteArrayContent(byteData))
            {
                string endpoint = "https://graph.facebook.com";
                string phoneNumberId = "460729810460560";
                string accesToken = "EAANmy6A9NrgBO39rRjOb61YgMUzwuZA8fWl20FvkJdg9ChlrjHWlxKKQw3UVAkzZC2quUSWSnl9C2HsFKhKFxJjALG08x7acsK06LT9ZCBZATa8STLv5VZC7uEgK3HYwOSowlSaEGmtJ8yXtQXjVv0sXQFqpCz8uj1I7tCxZAE7TkbUQKf9pq0F1f5bSgPYyOllN3VNwzBXsjjzrN59NFP8PesXCsZD";
                //string accesToken = "AARONTMP";
                string uri = $"{endpoint}/v21.0/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accesToken}");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;

            }



        }
    }
}
