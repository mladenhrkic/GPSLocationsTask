using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace Domain.Models;

public class Favourite
{
    [Key]
    public int Id { get; init; }
    
    [ForeignKey(nameof(Location))]
    public int LocationId { get; init; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
}