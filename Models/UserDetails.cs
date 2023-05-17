using System.ComponentModel.DataAnnotations;

namespace MemoGlobalTest.Modles
{
    public class UserDetails
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string avatar { get; set; }
    }
}
