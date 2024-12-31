using System.Text.Json;
using Domain.Models;
using GooglePlacesService.Models;
using Microsoft.Extensions.Configuration;

namespace GooglePlacesService;

public class HttpRequest(IHttpClientFactory factory, IConfiguration configuration)
{
    private readonly HttpClient _factory = factory.CreateClient();

    public async Task<Root> Send(GeoCoordinate coordinate)
    {
        var apiKey = configuration.GetValue<string>("GooglePlaceApiKey");
        var url = $"{configuration.GetValue<string>("GooglePlaceIPAddress")}{coordinate.Latitude},{coordinate.Longitude}&key={apiKey}";
        try
        {
            var response = await _factory.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Root>(responseData) ?? new Root();
            }

            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return new Root();
    }
}