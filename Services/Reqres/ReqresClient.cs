using MemoGlobalTest.Interface;
using MemoGlobalTest.Modles;
using Newtonsoft.Json;
using System.Text;

namespace MemoGlobalTest.Services.Reqres
{
    public class ReqresClient : IHttpClientService
    {
        private readonly string _reqresUrl;
        private readonly HttpClient _httpClient;

        public ReqresClient()
        {
            _httpClient = new HttpClient();
            _reqresUrl = Startup.Configuration["reqresUrl"]; 
        }


        public async Task<HttpResponseMessage> Get(string endpoint)
        {
            string apiUrl = $"{_reqresUrl}/{endpoint}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            return response;
        }

        public async Task<HttpResponseMessage> Post(string endpoint, UserDetails data)
        {
            string apiUrl = $"{_reqresUrl}/{endpoint}";

            string json = JsonConvert.SerializeObject(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(string endpoint)
        {
            string apiUrl = $"{_reqresUrl}/{endpoint}";

            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            return response;
        }


        public async Task<HttpResponseMessage> Put(string endpoint, UserDetails user)
        {
            string apiUrl = $"{_reqresUrl}/{endpoint}";

            string json = JsonConvert.SerializeObject(user);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);

            return response;
        }
    }
}
