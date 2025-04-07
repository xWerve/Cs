using System.Text.Json.Serialization;

public class Region
{
    [JsonPropertyName("regionid")]
    public string RegionId { get; set; }
    
    [JsonPropertyName("regiondescription")]
    public string RegionDescription { get; set; }
    
    public Region(string regionID, string regionDescription)
    {
        RegionId = regionID;
        RegionDescription = regionDescription;
    }
}