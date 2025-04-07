using System.Text.Json.Serialization;

public class OrderDetail
{
    [JsonPropertyName("orderid")]
    public string OrderID { get; set; }

    [JsonPropertyName("productid")]
    public string ProductID { get; set; }

    [JsonPropertyName("unitprice")]
    public string UnitPrice { get; set; }

    [JsonPropertyName("quantity")]
    public string Quantity { get; set; }

    [JsonPropertyName("discount")]
    public string Discount { get; set; }

    public OrderDetail(string orderID, string productID, string unitPrice, string quantity, string discount)
    {
        OrderID = orderID;
        ProductID = productID;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
    }

    public decimal GetTotal()
    {
        decimal unitPrice = 0;
        decimal quantity = 0;
    
        // Sprawdzamy, czy wartości można poprawnie sparsować
        if (!decimal.TryParse(UnitPrice, out unitPrice))
        {
            Console.WriteLine($"Błąd konwersji UnitPrice: {UnitPrice}");
        }

        if (!decimal.TryParse(Quantity, out quantity))
        {
            Console.WriteLine($"Błąd konwersji Quantity: {Quantity}");
        }

        // Sprawdzamy, czy obie wartości zostały poprawnie przekonwertowane
        return unitPrice * quantity;
    }
}