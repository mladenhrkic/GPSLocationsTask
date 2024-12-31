namespace GooglePlacesService.Models;

public class Root
{
    public PlusCode plus_code { get; set; }
    public List<Result> results { get; set; }
    public string status { get; set; }
}