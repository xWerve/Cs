using System.Text.Json.Serialization;

public class EmployeeTerritory {
    [JsonPropertyName("employeeid")]
    public string EmployeeId { get; set; }
    
    [JsonPropertyName("territoryid")]
    public string TerritoryId { get; set; }
    
    public EmployeeTerritory(string employeeID, string territoryID)
    {
        EmployeeId = employeeID;
        TerritoryId = territoryID;
    }
}