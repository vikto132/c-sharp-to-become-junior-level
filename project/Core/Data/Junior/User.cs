using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

namespace Core.Data.Junior
{
    [Table(nameof(User))]
    public class User : IHasId
    {
        [Key]
        public long Id { get; set; }
        
        [StringLength(20)]
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public ICollection<Role> Roles { get; set; }
    }
}