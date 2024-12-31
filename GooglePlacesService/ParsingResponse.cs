using GooglePlacesService.Models;
using Location = Domain.Models.Location;

namespace GooglePlacesService;

public static class ParsingResponse
{
    public static List<Location> Action(Root root)
    {
        var locations = new List<Location>();
        foreach (var resultItem in root.results)
        {
            var location = new Location
            {
                Street = GetAddressComponent("route"),
                Number = GetAddressComponent("street_number"),
                City = GetAddressComponent("locality"),
                Country = GetAddressComponent("country")
            };

            if (!string.IsNullOrWhiteSpace(location.Street) &&
                !string.IsNullOrWhiteSpace(location.Number) &&
                !string.IsNullOrWhiteSpace(location.City) &&
                !string.IsNullOrWhiteSpace(location.Country))
            {
                locations.Add(location);
            }

            continue;

            string GetAddressComponent(string type)
            {
                var component = resultItem.address_components
                    .FirstOrDefault(ac => ac.types.Contains(type));

                return component?.long_name ?? string.Empty;
            }
        }

        locations = locations
            .GroupBy(l => new { l.Street, l.Number, l.City, l.Country })
            .Select(g => g.First())
            .ToList();
        
        return locations;
    }
}