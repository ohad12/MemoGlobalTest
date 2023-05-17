using MemoGlobalTest.Data.Entities;

namespace MemoGlobalTest.Services.Reqres
{
    public class ReqresResponse
    {
        public ReqresUser data { get; set; }
    }

    public class ReqresListUsersResponse
    {
        public List<ReqresUser> data { get; set; }
    }

    public class ReqresPutResponse
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }

  
}
