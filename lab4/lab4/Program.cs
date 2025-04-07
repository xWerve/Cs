public class Program
{
    static void Main(string[] args)
    {
        var regions = Load<Region>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\regions.csv", x => new Region(x[0], x[1]));
        var territories = Load<Territory>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\territories.csv", x => new Territory(x[0], x[1], x[2]));
        var employeeTerritories = Load<EmployeeTerritory>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\employee_territories.csv", x => new EmployeeTerritory(x[0], x[1]));
        var employees = Load<Employee>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\employees.csv", x => new Employee(
            x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9],
            x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]
        ));

        
        Console.WriteLine("Zadanie 2: ");
        foreach (var employee in employees)
        {
            Console.WriteLine(employee.LastName);
        }
        
        var wynik = from et in employeeTerritories
            join emp in employees on et.EmployeeId equals emp.EmployeeID
            join t in territories on et.TerritoryId equals t.TerritoryId
            join r in regions on t.RegionId equals r.RegionId
            select new
            {
                Nazwisko = emp.LastName,
                Region = r.RegionDescription,
                Terytorium = t.TerritoryDescription
            };

        Console.WriteLine("Zadanie 3:");
        foreach (var item in wynik)
        {
            Console.WriteLine($"{item.Nazwisko} | {item.Region} | {item.Terytorium}");
        }
        
        
        var grupy = from r in regions
            join t in territories on r.RegionId equals t.RegionId into rt
            from t in rt
            join et in employeeTerritories on t.TerritoryId equals et.TerritoryId into tett
            from et in tett
            join e in employees on et.EmployeeId equals e.EmployeeID into ete
            from e in ete
            group e by r.RegionDescription into g
            select new
            {
                Region = g.Key,
                Pracownicy = g.Select(x => x.LastName).Distinct()
            };

        Console.WriteLine("Zadanie 4:");
        foreach (var region in grupy)
        {
            Console.WriteLine($"Region: {region.Region}");
            foreach (var nazwisko in region.Pracownicy)
            {
                Console.WriteLine($"  - {nazwisko}");
            }
        }

        
        var regionCounts = from r in regions
            join t in territories on r.RegionId equals t.RegionId
            join et in employeeTerritories on t.TerritoryId equals et.TerritoryId
            join e in employees on et.EmployeeId equals e.EmployeeID
            group e by r.RegionDescription into g
            select new
            {
                Region = g.Key,
                LiczbaPracownikow = g.Select(x => x.EmployeeID).Distinct().Count()
            };

        Console.WriteLine("Zadanie 5:");
        foreach (var item in regionCounts)
        {
            Console.WriteLine($"{item.Region}: {item.LiczbaPracownikow} pracowników");
        }
        
        var orders = Load<Order>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\orders.csv", x => new Order(
            x[0],  // orderid
            x[1],  // customerid
            x[2],  // employeeid
            x[3],  // orderdate
            x[4],  // requireddate
            x[5],  // shippeddate
            x[6],  // shipvia
            x[7],  // freight
            x[8],  // shipname
            x[9],  // shipaddress
            x[10], // shipcity
            x[11], // shipregion
            x[12], // shippostalcode
            x[13]  // shipcountry
        ));


        
        var orderDetails = Load<OrderDetail>.LoadCSV("C:\\Users\\mkrol\\RiderProjects\\lab4\\lab4\\orders_details.csv", x => new OrderDetail(
            x[0], // orderid
            x[1], // productid
            x[2], // unitprice
            x[3], // quantity
            x[4] // discount
        ));
        
        var statystyki = from emp in employees
            join ord in orders on emp.EmployeeID equals ord.EmployeeID into empOrders
            select new
            {
                Pracownik = emp.LastName,
                LiczbaZamowien = empOrders.Count(),
                SredniaWartosc = empOrders
                    .Select(o => orderDetails
                        .Where(od => od.OrderID == o.OrderID)
                        .Sum(od => od.GetTotal()))
                    .DefaultIfEmpty(0).Average(),
                MaksymalnaWartosc = empOrders
                    .Select(o => orderDetails
                        .Where(od => od.OrderID == o.OrderID)
                        .Sum(od => od.GetTotal()))
                    .DefaultIfEmpty(0).Max()
            };

        Console.WriteLine("Zadanie 6:");
        foreach (var s in statystyki)
        {
            Console.WriteLine($"{s.Pracownik}: {s.LiczbaZamowien} zamówień | Śr: {s.SredniaWartosc:F2} | Max: {s.MaksymalnaWartosc:F2}");
        }

    }
}