using System;
using System.Collections.Generic;
using System.Threading;

public static class Zad1
{
    public static void Uruchom()
    {
        Queue<int> dane = new Queue<int>();
        object blokada = new object();
        bool dzialaj = true;
        int iluProducentow = 3;
        int iluKonsumentow = 2;

        Dictionary<int, Dictionary<int, int>> ileOdKogo = new Dictionary<int, Dictionary<int, int>>();
        List<Thread> watki = new List<Thread>();

        void Producent(object numer)
        {
            int id = (int)numer;
            Random los = new Random(id);
            while (dzialaj)
            {
                lock (blokada) dane.Enqueue(id);
                Thread.Sleep(los.Next(500, 1000));
            }
        }

        void Konsument(object numer)
        {
            int id = (int)numer;
            Dictionary<int, int> lokalne = new Dictionary<int, int>();
            Random los = new Random(id + 1000);
            while (dzialaj)
            {
                int? producent = null;
                lock (blokada)
                {
                    if (dane.Count > 0)
                        producent = dane.Dequeue();
                }
                if (producent != null)
                {
                    if (!lokalne.ContainsKey(producent.Value))
                        lokalne[producent.Value] = 0;
                    lokalne[producent.Value]++;
                }
                Thread.Sleep(los.Next(500, 1000));
            }
            lock (blokada) ileOdKogo[id] = lokalne;
        }

        for (int i = 0; i < iluProducentow; i++)
        {
            var t = new Thread(Producent);
            t.Start(i);
            watki.Add(t);
        }

        for (int i = 0; i < iluKonsumentow; i++)
        {
            var t = new Thread(Konsument);
            t.Start(i);
            watki.Add(t);
        }

        while (Console.ReadKey(true).KeyChar != 'q') { }
        dzialaj = false;
        foreach (var t in watki) t.Join();

        foreach (var konsument in ileOdKogo)
        {
            Console.WriteLine($"Konsument {konsument.Key}:");
            foreach (var para in konsument.Value)
            {
                Console.WriteLine($"  Producent {para.Key} -> {para.Value}");
            }
        }
    }
}
