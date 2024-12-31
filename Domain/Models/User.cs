using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
    [Key] 
    public int Id { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}