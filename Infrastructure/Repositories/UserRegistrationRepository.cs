using Domain.Abstractions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRegistrationRepository(DatabaseContext context) : IUserRegistrationRepository
{
    public async Task<Result<Unit>> RegistrationAsync(User user)
    {
        var usersList = await GetAllUsers();
        var compare = usersList.FirstOrDefault(u => u.Username == user.Username);
        if (compare != null)
        {
            return Result<Unit>.Failure(Error.Conflict("User.Conflict", 
                $"User {user.Username} already exists"));
        }
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return Result<Unit>.Success();
    }
    
    private async Task<ICollection<User>> GetAllUsers()
    {
        return await context.Users.ToListAsync();
    }
}