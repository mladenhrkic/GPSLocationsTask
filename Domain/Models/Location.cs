using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Location
{
    [Key]
    public int Id { get; init; }
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
}