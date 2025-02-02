using Domain.Abstractions;
using Domain.Models;
using Infrastructure.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LocationRepository(DatabaseContext context) : ILocationRepository
{
    public async Task<Result<List<Location>>> GetLocationsByCity(string city)
    {
        var locations = await context.Locations
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.City, $"%{city}%"))
            .ToListAsync();

        return locations.Count == 0
            ? Message()
            : Result<List<Location>>.Success(locations);
    }

    public async Task<Result<List<Location>>> GetLocationsByStreet(string street)
    {
        var locations = await context.Locations
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.Street, $"%{street}%"))
            .ToListAsync();

        return locations.Count == 0
            ? Message()
            : Result<List<Location>>.Success(locations);
    }

    private static Result<List<Location>> Message()
    {
        return Result<List<Location>>.Failure(Error.NotFound("Location.NotFaund",
            "No locations were found."));
    }

    private async Task<int> GetBiggestLocationId()
    {
        var locations = await context.Locations
                .AsNoTracking()
                .ToListAsync();

        var maxId = locations.Count != 0 ? locations.Max(location => location.Id) : 0;
        return maxId;
    }

    public async Task<Result<List<Location>>> CreateLocations(ICollection<Location> locations, string apiKey)
    {
        var maxIdNumber = await GetBiggestLocationId();
        var userId = await UserId.Get(context, apiKey);

        foreach (var location in locations)
        {
            location.UserId = userId;
        }

        await context.Locations.AddRangeAsync(locations);
        await context.SaveChangesAsync();

        var result = await context.Locations
            .AsNoTracking()
            .Where(location => location.Id > maxIdNumber && location.UserId == userId)
            .ToListAsync();

        return Result<List<Location>>.Success(result);
    }

    public async Task<Result<List<Location>>> GetLocationsByFavourite(string apiKey)
    {
        var userId = await UserId.Get(context, apiKey);

        var favoriteLocations = await context.Favourites
            .AsNoTracking()
            .Where(f => f.UserId == userId)
            .Join(
                context.Locations,
                favourite => favourite.LocationId,
                location => location.Id,
                (favourite, location) => location
            )
            .ToListAsync();

        return favoriteLocations.Count == 0
            ? Message()
            : Result<List<Location>>.Success(favoriteLocations);
    }

    public async Task<Result<Unit>> SetLocationsFavourite(List<LocationId> locationId, string apiKey)
    {
        var userId = await UserId.Get(context, apiKey);

        if (userId == 0)
        {
            return Result<Unit>.Failure(Error.NotFound("Location.NotFaund",
                "Invalid API key or user not found."));
        }

        var favourites = locationId.Select(x => new Favourite
        {
            UserId = userId,
            LocationId = x.Id
        }).ToList();

        await context.Favourites.AddRangeAsync(favourites);
        await context.SaveChangesAsync();

        return Result<Unit>.Success();
    }

    public async Task<Result<Unit>> RemoveFavourite(List<LocationId> locationId, string apiKey)
    {
        var userId = await UserId.Get(context, apiKey);

        if (userId == 0)
        {
            return Result<Unit>.Failure(Error.NotFound("Location.NotFaund",
                "Invalid API key or user not found."));
        }
        var locationIds = locationId.Select(l => l.Id).ToList();

        var favouritesToRemove = await context.Favourites
            .Where(f => f.UserId == userId && locationIds.Contains(f.LocationId))
            .ToListAsync();

        if (!favouritesToRemove.Any())
        {
            return Result<Unit>.Failure(Error.NotFound("Location.NotFaund",
                "No matching favourites found.."));
        }

        context.Favourites.RemoveRange(favouritesToRemove);
        await context.SaveChangesAsync();

        return Result<Unit>.Success();
    }
}