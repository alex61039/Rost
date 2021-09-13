using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class UserPhoto : BaseEntity
    {
        public string Url { get; set; }
        public string Path { get; set; }
        public bool IsMain { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}