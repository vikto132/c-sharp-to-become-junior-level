using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

namespace Core.Data.Junior;

[Table(nameof(Role))]
public class Role : IHasId
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}