using System.ComponentModel.DataAnnotations;

namespace MemoGlobalTest.Data.Entities
{
    public class ReqresUser
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string? email { get; set; }
        [StringLength(50)]
        public string? first_name { get; set; }
        [StringLength(50)]
        public string? last_name { get; set; }
        [StringLength(150)]
        public string? avatar { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }

}
