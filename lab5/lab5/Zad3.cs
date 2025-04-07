using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

public static class Zad3
{
    public static void Uruchom()
    {
        Console.WriteLine("Podaj katalog startowy:");
        string katalog = Console.ReadLine();
        Console.WriteLine("Podaj fragment nazwy pliku do znalezienia:");
        string wzorzec = Console.ReadLine();

        BlockingCollection<string> znalezionaSciezka = new BlockingCollection<string>();

        Thread szukacz = new Thread(() =>
        {
            foreach (var plik in Directory.EnumerateFiles(katalog, "*", SearchOption.AllDirectories))
            {
                if (plik.Contains(wzorzec))
                    znalezionaSciezka.Add(plik);
            }

            znalezionaSciezka.CompleteAdding();
        });

        szukacz.Start();

        foreach (var plik in znalezionaSciezka.GetConsumingEnumerable())
        {
            Console.WriteLine(plik);
        }

        szukacz.Join();
    }
}