using Domain.Models;
using MediatR;

namespace Domain.Abstractions;

public interface ILocationRepository
{
    Task<Result<List<Location>>> GetLocationsByCity(string city);
    Task<Result<List<Location>>> GetLocationsByStreet(string street);
    Task<Result<List<Location>>> GetLocationsByFavourite(string apiKey);
    Task<Result<List<Location>>> CreateLocations(ICollection<Location> locations, string apiKey);
    Task<Result<Unit>> SetLocationsFavourite(List<LocationId> locationId, string apiKey);
    Task<Result<Unit>> RemoveFavourite(List<LocationId> locationId, string apiKey);
}