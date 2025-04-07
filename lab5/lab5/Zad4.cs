using System;
using System.Threading;

public static class Zad4
{
    public static void Uruchom()
    {
        int n = 5;

        CountdownEvent czekajNaStart = new CountdownEvent(n);
        CountdownEvent czekajNaKoniec = new CountdownEvent(n);

        for (int i = 0; i < n; i++)
        {
            Thread t = new Thread(() =>
            {
                czekajNaStart.Signal();
                czekajNaStart.Wait();
                Thread.Sleep(1000);
                czekajNaKoniec.Signal();
            });

            t.Start();
        }

        czekajNaStart.Wait();
        Console.WriteLine("Wszystkie wątki rozpoczęły działanie");

        czekajNaKoniec.Wait();
        Console.WriteLine("Wszystkie wątki zakończyły działanie");
    }
}