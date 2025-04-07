using System.Text.Json.Serialization;

public class Employee
{
    [JsonPropertyName("employeeid")]
    public string EmployeeID { get; set; }

    [JsonPropertyName("lastname")]
    public string LastName { get; set; }

    [JsonPropertyName("firstname")]
    public string FirstName { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("titleofcourtesy")]
    public string TitleOfCourtesy { get; set; }

    [JsonPropertyName("birthdate")]
    public string BirthDate { get; set; }

    [JsonPropertyName("hiredate")]
    public string HireDate { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("postalcode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("homephone")]
    public string HomePhone { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }

    [JsonPropertyName("photo")]
    public string Photo { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("reportsto")]
    public string ReportsTo { get; set; }

    [JsonPropertyName("photopath")]
    public string PhotoPath { get; set; }
    
    public Employee(string employeeID, string lastName, string firstName, string title,
        string titleOfCourtesy, string birthDate, string hireDate, string address,
        string city, string region, string postalCode, string country, string homePhone,
        string extension, string photo, string notes, string reportsTo, string photoPath)
    {
        EmployeeID = employeeID;
        LastName = lastName;
        FirstName = firstName;
        Title = title;
        TitleOfCourtesy = titleOfCourtesy;
        BirthDate = birthDate;
        HireDate = hireDate;
        Address = address;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Country = country;
        HomePhone = homePhone;
        Extension = extension;
        Photo = photo;
        Notes = notes;
        ReportsTo = reportsTo;
        PhotoPath = photoPath;
    }   
}