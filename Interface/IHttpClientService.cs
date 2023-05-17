using MemoGlobalTest.Modles;

namespace MemoGlobalTest.Interface
{
    public interface IHttpClientService
    {
        public Task<HttpResponseMessage> Get(string endpoint);
        public Task<HttpResponseMessage> Post(string endpoint, UserDetails data);
        public Task<HttpResponseMessage> Delete(string endpoint);
        public Task<HttpResponseMessage> Put(string endpoint, UserDetails user);
    }
}
