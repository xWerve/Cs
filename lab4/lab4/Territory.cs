using System.Text.Json.Serialization;

public class Territory
{
    [JsonPropertyName("territoryid")]
    public string TerritoryId { get; set; }
    
    [JsonPropertyName("territorydescription")]
    public string TerritoryDescription { get; set; }
    
    [JsonPropertyName("regionid")]
    public string RegionId { get; set; }
    
    public Territory(string territoryID, string territoryDescription, string regionID)
    {
        TerritoryId = territoryID;
        TerritoryDescription = territoryDescription;
        RegionId = regionID;
    }
}