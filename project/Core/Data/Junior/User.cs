using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Junior
{
    [Table(nameof(User))]
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [StringLength(20)]
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}