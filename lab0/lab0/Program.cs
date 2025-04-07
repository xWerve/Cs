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

1. W programie Visual Studio Code stwórz nową aplikację konsolową technologii .NET Framework 7.0 i uruchom go. Program ma pobierać z linii komend zestaw napisów oraz jako ostatni parametr liczbę powtórzeń. Program ma wypisać na ekran wszystkie napisy tyle razy, ile wynosiła wartość ostatniego parametru (3 punkt).

cs

> dotnet new console --framework net7.0
> dotnet run

2. Napisz program, który będzie pobierał dane liczbowe klawiatury aż do momentu, kiedy użytkownik wpisze 0. Program ma sumować wpisane liczby, a na końcu wyliczyć ich średnią. Wynik zapisz do pliku (2 punkty).

cs

//Zapis linijki tekstu do pliku w trybie append
StreamWriter sw = new StreamWriter("NazwaPliku.txt", append:true);
sw.WriteLine("Jakiś napis");
sw.Close();

3. Napisz program, który w pliku tekstowym zawierającym liczby znajdzie liczbę o największej wartości. Program jako parametr (linii komend) ma pobierać nazwę pliku. Jako wynik do konsoli proszę wypisać tę liczbę oraz numery linijki, w których znaleziono liczbę, na przykład "555, linijka: 10" (2 punkty).

cs

//czytanie z pliku tekstowego linijka po linijce aż do końca pliku
StreamReader sr = new StreamReader("NazwaPlikuTekstowego.txt");
while (!sr.EndOfStream)
{
    String napis = sr.ReadLine();
}
sr.Close();

4. Napisz program, który wypisze gamę dur rozpoczynając od jednego wybranego z dwunastu dźwięków. Są następujące dźwięki:
C, C#, D, D#, E, F, F#, G, G#, A, B, H

Po dźwięku H znowu następuje dźwięk C. Pomiędzy każdym dźwiękiem jest różnica pół tonu. Gama dur tworzona jest w następujący sposób: dźwięk podstawowy, a następnie dźwięki wyższe o: 2, 2, 1, 2, 2, 2, 1 ton. Czyli gama C-dur to:

C D E F G A H C

Gama C#-dur to:

C#, D#, F, F#, G#, B, C, C#

Gama kończy się zawsze tym samym dźwiękiem, od którego się zaczynała i ma 8 dźwięków. Program ma pobierać z klawiatury nazwę dźwięku, a na ekran wypisywać gamę (3 punkty).
*/



class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz numer zadania (2-4):");
        string wybor = Console.ReadLine();
        
        switch (wybor)
        {
            case "2": Zadanie2(); break;
            case "3": Zadanie3(); break;
            case "4": Zadanie4(); break;
            default: Console.WriteLine("Niepoprawny numer zadania."); break;
        }
    }

    static void Zadanie2()
    {
        Console.WriteLine("Wprowadzaj liczby (0 kończy wpisywanie): ");
        double liczba = double.Parse(Console.ReadLine());
        double suma = liczba;
        int ile = 0;
        while (liczba != 0.0)
        {
            liczba = double.Parse(Console.ReadLine());
            suma += liczba;
            ile++;
        }
        double srednia = suma / ile;
        Console.WriteLine("Suma: " + suma);
        Console.WriteLine("Średnia: " + srednia);
        
        StreamWriter sw = new StreamWriter("zad2.txt", append:false);
        sw.WriteLine("Suma: " + suma);
        sw.WriteLine("Średnia: " + srednia);
        sw.Close();
    }

    static void Zadanie3()
    {
        Console.WriteLine("Podaj nazwe pliku z którego będziemy czytać liczby: ");
        string plik = Console.ReadLine();
        int ktora_linia = 0;
        List<int> lista_linii = new List<int>();
        double max = double.MinValue;
        
        
        StreamReader sr = new StreamReader(plik);
        
        while (!sr.EndOfStream)
        {
            ktora_linia++;
            double liczba = double.Parse(sr.ReadLine());
            if (liczba > max)
            {
                max = liczba;
                lista_linii.Clear();
                lista_linii.Add(ktora_linia);
            }
            else if (liczba == max)
            {
                lista_linii.Add(ktora_linia);    
            }
        }
        sr.Close();
        
        for (int i = 0; i < lista_linii.Count; i++)
        {
            Console.WriteLine(max + " linijka: "+ lista_linii[i]);
        }
        
    }

    static void Zadanie4()
    {
        List<string> dzwieki = new List<string>() {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "B", "H" };
        List<int> gama = new List<int>() {0, 2, 2, 1, 2, 2, 2, 1};
        Console.WriteLine("Podaj jaka gamę chcesz napisać: ");
        string dzwiek = Console.ReadLine();
        int index = dzwieki.IndexOf(dzwiek);
        
        Console.Write("Gama " + dzwiek + "-dur: " + dzwiek + " ");
        for (int i = 0; i < gama.Count; i++)
        {
            index =  (index + gama[i]) % dzwieki.Count;
            Console.WriteLine(dzwieki[index] + " ");
        }
    }
}