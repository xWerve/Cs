using System;
using System.IO;
using System.Threading;

public static class Zad2
{
    public static void Uruchom()
    {
        bool dzialaj = true;

        Console.WriteLine("Podaj ścieżkę do katalogu:");
        string sciezka = Console.ReadLine();

        Thread monitor = new Thread(() =>
        {
            FileSystemWatcher watcher = new FileSystemWatcher(sciezka);
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = false;

            watcher.Created += (s, e) =>
            {
                if (dzialaj)
                    Console.WriteLine($"dodano plik {e.Name}");
            };

            watcher.Deleted += (s, e) =>
            {
                if (dzialaj)
                    Console.WriteLine($"usunięto plik {e.Name}");
            };

            while (dzialaj)
                Thread.Sleep(100);
        });

        monitor.Start();

        Console.WriteLine("Naciśnij 'q', żeby zakończyć...");
        while (Console.ReadKey(true).KeyChar != 'q') { }
        dzialaj = false;

        monitor.Join();
    }
}