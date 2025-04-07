/*
Laboratorium 01: Pierwsze aplikacje konsolowe C# .NET Framework Core 7.0.
Programowanie zaawansowane 2

- Maksymalna liczba punktów: 10

- Skala ocen za punkty:
    - 9-10 ~ bardzo dobry (5.0)
    - 8 ~ plus dobry (4.5)
    - 7 ~ dobry (4.0)
    - 6 ~ plus dostateczny (3.5)
    - 5 ~ dostateczny (3.0)
    - 0-4 ~ niedostateczny (2.0)

Celem laboratorium jest zapoznanie z operacjami wejścia/wyjścia języka C# i praktyki implementacji prostych algorytmów.

Niektóre programy wymagają podania z linii poleceń pewnych parametrów. Dla uproszczenia przyjmijmy, że programy nie muszą obsługiwać wyjątków spowodowanych ewentualnymi błędami konwersji oraz, że użytkownicy podają odpowiednią liczbę parametrów.

1. W programie Visual Studio Code stwórz nową aplikację konsolową technologii .NET Framework 7.0 i uruchom go (1 punkt).

> dotnet new console --framework net7.0
> dotnet run

2. Napisz program, który będzie pobierał z klawiatury napisy aż do momentu, kiedy użytkownik wpisze napis: "koniec!". 
   Program ma zapamiętywać napis (trzymać go w zmiennej), który po posortowaniu zgodnie z kolejnością leksykograficzną będzie ostatni. 
   Wszystkie wprowadzane do tej pory napisy mają być zapisywane do pliku tekstowego w nowych linijkach. 
   Zapis do pliku proszę zrobić w trybie append (2 punkty).

// Zapis linijki tekstu do pliku w trybie append
StreamWriter sw = new StreamWriter("NazwaPliku.txt", append:true);
sw.WriteLine("Jakiś napis");
sw.Close();

3. Napisz program, który w pliku tekstowym znajdzie wszystkie wystąpienia wybranego ciągu znaków. 
   Program jako parametr (linii komend) ma pobierać nazwę pliku oraz ciąg znaków, który ma zostać znaleziony. 
   Jako wynik do konsoli proszę wypisać numery linijek oraz pozycje od początku linijek, w których znaleziono szukany ciąg znaków, np. 
   "linijka: 10, pozycja: 5" (2 punkty).

// czytanie z pliku tekstowego linijka po linijce aż do końca pliku
StreamReader sr = new StreamReader("NazwaPlikuTekstowego.txt");
while (!sr.EndOfStream)
{
    String napis = sr.ReadLine();
}
sr.Close();

4. Napisz program, który do pliku tekstowego wygeneruje n losowych liczb. 
   Każda liczba powinna być zapisywana w osobnej linijce. 
   Parametry programu (linii komend) (2 punkty): 
    - nazwa pliku, 
    - liczba n, 
    - przedział wartości, z którego losowane są liczby,
    - seed,
    - czy losowe liczby mają być rzeczywiste czy całkowite

// generowanie losowej liczby
int seed = 0;
Random random = new Random(seed);
int l = random.Next(0,10); // liczba z przedziału [0,10)

5. Napisz program, który w pliku utworzonym przy pomocy programu z polecenia (4) będzie obliczał i wypisywał na ekran (3 punkty):
    - liczbę linii pliku,
    - liczbę znaków w pliku,
    - największą liczbę,
    - najmniejszą liczbę,
    - średnią liczb.

6. Napisz (a nie ściągnij z Internetu ;-) ) implementację sortowania przez łączenie 
   [link](https://pl.wikipedia.org/wiki/Sortowanie_przez_%C5%82%C4%85czenie_naturalne), 
   która będzie pracować na plikach tekstowych (całość danych ma NIE BYĆ wczytana na raz do jakiejś struktury pomocniczej: tabeli, listy itp. 
   - program ma pracować na plikach o dostępie sekwencyjnym). 
   Program ma mieć możliwość posortowania pliku tekstowego utworzonego przy pomocy programu z punktu (4). 
   (Za rozwiązanie dodatkowa ocena 5.0).

Powodzenia! :-)
*/




class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz numer zadania (2-5):");
        string wybor = Console.ReadLine();
        
        switch (wybor)
        {
            case "2": Zadanie2(); break;
            case "3": Zadanie3(); break;
            case "4": Zadanie4(); break;
            case "5": Zadanie5(); break;
            default: Console.WriteLine("Niepoprawny numer zadania."); break;
        }
    }

    static void Zadanie2()
    {
        Console.WriteLine("Podaj nazwe pliku z którego będziemy czytać słowa: ");
        string plik = Console.ReadLine();
        StreamReader sr = new StreamReader(plik);
        string max = ""; 
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            if (line == "koniec") break;
            else
            {
                int score = String.Compare(line, max);
                if (score > 0) max = line;
            }
            
        }
        sr.Close();
        Console.WriteLine("Najwiekszy leksykalnie wyraz: " + max);
    }

    static void Zadanie3()
    {
        Console.WriteLine("Podaj nazwe pliku z którego będziemy czytać słowa: ");
        string plik = Console.ReadLine();
        Console.WriteLine("Podaj słowo, którego będziemy szukać w pliku: ");
        string slowo = Console.ReadLine();
        StreamReader sr = new StreamReader(plik);

        int linijka = 0;
        while (!sr.EndOfStream)
        {
            linijka++;
            string line = sr.ReadLine();
            
            int pozycja = line.IndexOf(slowo);
            
            while (pozycja != -1)
            {
                Console.WriteLine($"Linijka: {linijka}, Pozycja: {pozycja + 1}"); 
                pozycja = line.IndexOf(slowo, pozycja + 1); 
            }
        }
        sr.Close();
    }


    static void Zadanie4()
    {
        Console.WriteLine("Podaj nazwę pliku:");
        string filePath = Console.ReadLine();
        Console.WriteLine("Podaj liczbę elementów:");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj minimalną wartość:");
        int min = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj maksymalną wartość:");
        int max = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj ziarno losowania:");
        int seed = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj typ liczb (int/double):");
        bool isInt = Console.ReadLine() == "int";
        
        Random rand = new Random(seed);

        using (StreamWriter sw = new StreamWriter(filePath))
        {
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(isInt ? rand.Next(min, max + 1).ToString() : (rand.NextDouble() * (max - min) + min).ToString("F2"));
            }
        }
    }

    static void Zadanie5()
    {
        Console.WriteLine("Podaj nazwę pliku:");
        string filePath = Console.ReadLine();

        string[] lines = File.ReadAllLines(filePath);
        int charCount = lines.Sum(line => line.Length);
        double[] numbers = lines.Select(double.Parse).ToArray();

        Console.WriteLine($"Liczba linii: {lines.Length}");
        Console.WriteLine($"Liczba znaków: {charCount}");
        Console.WriteLine($"Największa liczba: {numbers.Max()}");
        Console.WriteLine($"Najmniejsza liczba: {numbers.Min()}");
        Console.WriteLine($"Średnia liczb: {numbers.Average()}");
    }
}
