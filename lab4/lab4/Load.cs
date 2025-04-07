public class Load<T>
{
    public static List<T> LoadCSV(String path, Func<String[], T> generuj) //Func <- przyjmuje tablicę stringów i zwraca obiekt T
    {
        var objs = new List<T>();
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Plik nie istnieje!");
            return objs;
        }

        var lines = File.ReadAllLines(path).Skip(1); // pomija pierwszy wiersz (nagłówek)
        
        foreach (var line in lines)
        {
            var obj = generuj(line.Split(','));
            if (obj != null) objs.Add(obj);
        }
        
        return objs;
    }
}