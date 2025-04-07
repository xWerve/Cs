using System;

/*
    Laboratorium 05: Programowanie współbieżne i synchronizacja (Thread)
    Przedmiot: Programowanie Zaawansowane 2
    Maksymalna liczba punktów: 10

    Skala ocen:
        - 9–10 pkt: bardzo dobry (5.0)
        - 8 pkt: plus dobry (4.5)
        - 7 pkt: dobry (4.0)
        - 6 pkt: plus dostateczny (3.5)
        - 5 pkt: dostateczny (3.0)
        - 0–4 pkt: niedostateczny (2.0)

    Cel laboratorium:
    Zapoznanie się z programowaniem współbieżnym i synchronizacją procesów za pomocą klasy Thread.
    Uwaga: Nie używamy wątków tła (Thread.IsBackground == false). Należy zadbać o sekcje krytyczne.

    Zadania do wykonania:

    1. [4 punkty] Problem producenta i konsumenta
       - Uruchom n wątków produkujących dane i m wątków konsumujących.
       - Każdy wątek ma swój numer ID.
       - Producenci generują dane w losowych odstępach i dodają je do wspólnej struktury (np. listy).
       - Konsumenci pobierają pierwszy element, zapisują ID producenta.
       - Program kończy działanie po naciśnięciu 'q' i wypisuje statystyki (ile danych od którego producenta).

    2. [2 punkty] Monitorowanie katalogu
       - Program śledzi w osobnym wątku zmiany w katalogu (tworzenie/usuwanie plików, bez podkatalogów).
       - Na dodanie pliku wypisuje "dodano plik [nazwa]".
       - Na usunięcie pliku wypisuje "usunięto plik [nazwa]".
       - Program kończy działanie po wciśnięciu 'q' (klawisz oczekiwany w innym wątku niż monitorujący).

    3. [2 punkty] Wyszukiwanie plików po fragmencie nazwy
       - Program przeszukuje katalog oraz jego podkatalogi, szukając plików zawierających podany fragment nazwy.
       - Wyszukiwanie odbywa się w osobnym wątku.
       - Gdy wątek znajdzie pasujący plik, przekazuje nazwę do wątku głównego, który ją wypisuje.

    4. [2 punkty] Oczekiwanie na start wszystkich wątków
       - Uruchom n wątków i poczekaj, aż wszystkie rozpoczną pracę (czyli wykonają co najmniej jedną instrukcję).
       - Po starcie wszystkich, główny wątek wypisuje komunikat i rozpoczyna zamykanie wątków.
       - Gdy wszystkie się zakończą, program kończy działanie z odpowiednią informacją.

    Powodzenia!
*/


class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz numer zadania do uruchomienia:");
        Console.WriteLine("1 - Producent/Konsument");
        Console.WriteLine("2 - Monitor folderu");
        Console.WriteLine("3 - Szukaj plików");
        Console.WriteLine("4 - Startuj wątki i poczekaj");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Zad1.Uruchom();
                break;
            case "2":
                Zad2.Uruchom();
                break;
            case "3":
                Zad3.Uruchom();
                break;
            case "4":
                Zad4.Uruchom();
                break;
            default:
                Console.WriteLine("Nie ma takiego zadania");
                break;
        }
    }
}