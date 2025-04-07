using System.Text.Json.Serialization;

public class Order
{
    [JsonPropertyName("orderid")]
    public string OrderID { get; set; }

    [JsonPropertyName("customerid")]
    public string CustomerID { get; set; }

    [JsonPropertyName("employeeid")]
    public string EmployeeID { get; set; }

    [JsonPropertyName("orderdate")]
    public string OrderDate { get; set; }

    [JsonPropertyName("requireddate")]
    public string RequiredDate { get; set; }

    [JsonPropertyName("shippeddate")]
    public string ShippedDate { get; set; }

    [JsonPropertyName("shipvia")]
    public string ShipVia { get; set; }

    [JsonPropertyName("freight")]
    public string Freight { get; set; }

    [JsonPropertyName("shipname")]
    public string ShipName { get; set; }

    [JsonPropertyName("shipaddress")]
    public string ShipAddress { get; set; }

    [JsonPropertyName("shipcity")]
    public string ShipCity { get; set; }

    [JsonPropertyName("shipregion")]
    public string ShipRegion { get; set; }

    [JsonPropertyName("shippostalcode")]
    public string ShipPostalCode { get; set; }

    [JsonPropertyName("shipcountry")]
    public string ShipCountry { get; set; }

    public Order(string orderID, string customerID, string employeeID, string orderDate, string requiredDate, string shippedDate, string shipVia, string freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
    {
        OrderID = orderID;
        CustomerID = customerID;
        EmployeeID = employeeID;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        ShipVia = shipVia;
        Freight = freight;
        ShipName = shipName;
        ShipAddress = shipAddress;
        ShipCity = shipCity;
        ShipRegion = shipRegion;
        ShipPostalCode = shipPostalCode;
        ShipCountry = shipCountry;
    }
}