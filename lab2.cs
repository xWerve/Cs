// Laboratorium 02: Programowanie obiektowe w języku C# .NET Framework Core 7.0.
// Programowanie zaawansowane 2

// - Maksymalna liczba punktów: 10

// - Skala ocen za punkty:
//     - 9-10 ~ bardzo dobry (5.0)
//     - 8 ~ plus dobry (4.5)
//     - 7 ~ dobry (4.0)
//     - 6 ~ plus dostateczny (3.5)
//     - 5 ~ dostateczny (3.0)
//     - 0-4 ~ niedostateczny (2.0)

// Celem laboratorium jest zapoznanie z metodami programowania obiektowego w języku C#. W tym celu zbudujemy uproszczony model obsługujący transakcje bankowe. Model będzie składał się z rachunków bankowych, których właścicielami będą osoby fizyczne lub/i osoby prawne. Każdy rachunek będzie musiał mieć co najmniej jednego właściciela. Transakcje bankowe będą mogły odbywać się pomiędzy rachunkami (bezgotówkowe przelewy) lub będą wpłatą lub wypłatą funduszy z rachunku.

// 1. [1 punkt] Stwórz publiczną, abstrakcyjną klasę PosiadaczRachunku, w której będzie znajdować się przeciążenie metody String ToString() z klasy object. Metoda przeciążająca ma być również abstrakcyjna.

// 2. [1 punkt] Stwórz klasę OsobaFizyczna, która będzie dziedziczyć z klasy PosiadaczRachunku. Klasa ma zawierać pola: imie, nazwisko, drugieImie, PESEL, numerPaszportu. Pola mają być prywatne, a dostęp do nich będzie odbywał się przez publiczne właściwości (properties). Metoda String ToString() ma zwracać napis informujący, że jest to osoba fizyczna oraz imię i nazwisko.

// 3. [1 punkt] Stwórz konstruktor do klasy OsobaFizyczna, który będzie pobierał wartości do wszystkich pól tej klasy. Jeżeli w konstruktorze równocześnie podane zostanie, że PESEL oraz numerPaszportu są równe null, ma zostać rzucony wyjątek.

// przykład rzucenia wyjątku:
// throw new Exception("PESEL albo numer paszportu muszą być nie null");

// 4. [1 punkt] Stwórz klasę OsobaPrawna, która będzie dziedziczyć z klasy PosiadaczRachunku. Klasa ma zawierać pola: nazwa, siedziba. Pola mają być prywatne, a dostęp do nich będzie odbywał się przez publiczne właściwości (properties). Właściwości mają pozwalać jedynie na odczyt wartości (a nie na ich zmianę). Metoda String ToString() ma zwracać napis informujący, że jest to osoba prawna oraz nazwa i siedziba.

// 5. [1 punkt] Stwórz konstruktor do klasy OsobaPrawna, który będzie pobierał wartości do wszystkich pól tej klasy.

// 6. [1 punkt] Stwórz klasę RachunekBankowy, która będzie miała pola: numer (typu String), stanRachunku (typu Decimal), czyDozwolonyDebet (typu bool), listę posiadaczyRachunku:

// List<PosiadaczRachunku> PosiadaczeRachunku = new List<PosiadaczRachunku>();

// Podstawowe operacje na liście (przydadzą się w dalszej części laboratorium):

// dodawanie:
// lo.Add(o);

// sprawdzenie, czy lista ma element:
// if (lo.Contains(o))
//     // usuwanie
//     lo.Remove(o);

// liczba elementów na liście:
// Console.WriteLine(lo.Count);

// Pola mają być prywatne, a dostęp do nich będzie odbywał się przez publiczne właściwości (properties).

// 7. [1 punkt] Stwórz konstruktor do klasy RachunekBankowy, który będzie pobierał wartości do wszystkich pól tej klasy. Konstruktor musi sprawdzać, czy lista posiadaczy rachunku zawiera co najmniej jedną pozycję (jeśli nie, rzucać wyjątek).

// 8. [1 punkt] Stwórz klasę Transakcja, która będzie miała pola: rachunekZrodlowy oraz rachunekDocelowy (typu RachunekBankowy), kwota (typu Decimal), opis (typu string). Pola mają być prywatne, a dostęp do nich będzie odbywał się przez publiczne właściwości (properties).

// 9. [1 punkt] Stwórz konstruktor do klasy Transakcja, który będzie pobierał wartości do wszystkich pól tej klasy. Konstruktor musi sprawdzać, czy rachunek docelowy i źródłowy są różne od null (jeśli nie, ma rzucać wyjątek).

// 10. [1 punkt] Do klasy RachunekBankowy dodaj listę transakcji:

// List<Transakcja> Transakcje = new List<Transakcja>();

// Do klasy RachunekBankowy dodaj publiczną statyczną metodę DokonajTransakcji. Metoda ma pobierać jako parametry rachunek źródłowy, rachunek docelowy, kwotę oraz opis. Jeżeli:
// - kwota jest ujemna lub,
// - oba rachunki są równe null lub,
// - rachunek źródłowy nie pozwala na debet (czyDozwolonyDebet == false), a kwota transakcji przekroczy stanRachunku,

// metoda ma rzucić wyjątek.

// - jeżeli rachunek źródłowy jest równy null, to zakładamy, że jest to wpłata gotówkowa. Do stanu rachunku rachunku docelowego dodajemy kwotę transakcji i tworzymy nowy obiekt klasy Transakcja, do którego przekazujemy parametry wywołania metody DokonajTransakcji. Tak stworzony obiekt klasy Transakcja dodajemy do listy Transakcje obiektu rachunku docelowego.

// - jeżeli rachunek docelowy jest równy null, to zakładamy, że jest to wypłata gotówkowa. Od stanu rachunku rachunku źródłowego odejmujemy kwotę transakcji i tworzymy nowy obiekt klasy Transakcja, do którego przekazujemy parametry wywołania metody DokonajTransakcji. Tak stworzony obiekt klasy Transakcja dodajemy do listy Transakcje obiektu rachunku źródłowego.

// - jeżeli żadne z poprzednich nie zachodzi, to zakładamy, że jest to przelew. Od stanu rachunku rachunku źródł


public abstract class PosiadaczRachunku
{
    public abstract override string ToString();
}

public class OsobaFizyczna : PosiadaczRachunku
{
    private string imie;
    private string nazwisko;
    private string drugieImie;
    private string pesel;
    private string numerPaszportu;

    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }

    public string Pesel
    {
        get { return pesel; }
        set
        {
            if (value.Length != 11)
                throw new Exception("PESEL ma złą długość!");
            pesel = value;
        }
    }

    public string NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }

    public override string ToString()
    {
        return $"Jest to osoba fizyczna\nImię: {imie}\nNazwisko: {nazwisko}\n";
    }

    public OsobaFizyczna(string _imie, string _nazwisko, string _drugieImie, string _pesel, string _numerPaszportu)
    {
        if (_pesel == null && _numerPaszportu == null)
        {
            throw new Exception("PESEL albo numer paszportu muszą być nie null");
        }

        if (_pesel == null || _pesel.Length != 11)
        {
            throw new Exception("PESEL ma złą długość!");
        }
        imie = _imie;
        nazwisko = _nazwisko;
        drugieImie = _drugieImie;
        numerPaszportu = _numerPaszportu;
        pesel = _pesel;
    }
}

public class OsobaPrawna : PosiadaczRachunku
{
    private string nazwa;
    private string siedziba;

    public string Nazwa
    {
        get { return nazwa; }
    }

    public string Siedziba
    {
        get { return siedziba; }
    }
    
    public override string ToString()
    {
        return $"Jest to osoba prawna\nNazwa: {nazwa}\nSiedziba: {siedziba}\n";
    }

    public OsobaPrawna(string _nazwa, string _siedziba)
    {
        nazwa = _nazwa;
        siedziba = _siedziba;
    }
}

public class Transakcja
{
    private RachunekBankowy rachunekZrodlowy;
    private RachunekBankowy rachunekDocelowy;
    private decimal kwota;
    private string opis;

    public RachunekBankowy RachunekZrodlowy
    {
        get { return rachunekZrodlowy; }
        set { rachunekZrodlowy = value; }
    }

    public RachunekBankowy RachunekDocelowy
    {
        get { return rachunekDocelowy; }
        set { rachunekDocelowy = value; }
    }

    public decimal Kwota
    {
        get { return kwota; }
        set { kwota = value; }
    }
    public string Opis
    {
        get { return opis; }
        set { opis = value; }
    }

    public Transakcja(RachunekBankowy _rachunekZrodlowy, RachunekBankowy _rachunekDocelowy, decimal _kwota,
        string _opis)
    {
        if (_rachunekZrodlowy == null || _rachunekDocelowy == null)
        {
            throw new Exception("Rachunek nie może być nullem");
        }
        rachunekZrodlowy = _rachunekZrodlowy;
        rachunekDocelowy = _rachunekDocelowy;
        kwota = _kwota;
        opis = _opis;
    }

    public override string ToString()
    {
        return $"Nr rachunku źródłowego: {rachunekZrodlowy}\nNr rachunku docelowego: {rachunekDocelowy}\nKwota {kwota}\nOpis: {opis}";
    }
    
}

public class RachunekBankowy
{
    private string numer;
    private decimal stanRachunku;
    private bool czyDozwolonyDebet;
    private List<PosiadaczRachunku> posiadaczeRachunku = new List<PosiadaczRachunku>();

    public string Numer
    {
        get { return numer; }
        set { numer = value; }
    }

    public decimal StanRachunku
    {
        get { return stanRachunku; }
        set { stanRachunku = value; }
    }

    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
        set { czyDozwolonyDebet = value; }
    }

    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return posiadaczeRachunku; }
        set { posiadaczeRachunku = value; }
    }

    public RachunekBankowy(string _numer, decimal _stanRachunku, bool _czyDozwolonyDebet,
        List<PosiadaczRachunku> _posiadaczeRachunku)
    {
        if (_posiadaczeRachunku.Count < 1)
        {
            throw new Exception("Lista posiadaczy rachunku musi zawierać przynajmniej jeden rekord");
        }
        numer = _numer;
        stanRachunku = _stanRachunku;
        czyDozwolonyDebet = _czyDozwolonyDebet;
        posiadaczeRachunku = _posiadaczeRachunku;
    }
    public List<Transakcja> _Transakcje = new List<Transakcja>();

    public static void DokonajTransakcji(RachunekBankowy rach_zr, RachunekBankowy rach_doc, decimal kwota, string opis)
    {
        if (kwota < 0) throw new Exception("Kwota nie może być ujemna");
        if (rach_zr == null && rach_doc == null) throw new Exception("Oba rachunki nie mogą być nullami");
    
        if (rach_zr != null && !rach_zr.CzyDozwolonyDebet && kwota > rach_zr.StanRachunku)
            throw new Exception("Nie pozwolono na debet i kwota przekroczyła stan rachunku");

        if (rach_zr == null)
        {
            rach_doc.StanRachunku += kwota;
            Transakcja t = new Transakcja(null, rach_doc, kwota, opis);
            rach_doc._Transakcje.Add(t);
        }
        else if (rach_doc == null)
        {
            rach_zr.StanRachunku -= kwota;
            Transakcja t = new Transakcja(rach_zr, null, kwota, opis);
            rach_zr._Transakcje.Add(t);
        }
        else
        {
            rach_zr.StanRachunku -= kwota;
            rach_doc.StanRachunku += kwota;
            Transakcja t = new Transakcja(rach_zr, rach_doc, kwota, opis);
            rach_zr._Transakcje.Add(t);
            rach_doc._Transakcje.Add(t);
        }
    }

    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku nowyPosiadacz)
    {
        if (rachunek.posiadaczeRachunku.Contains(nowyPosiadacz))
            throw new Exception("Posiadacz rachunku jest już na liście!");
        rachunek.posiadaczeRachunku.Add(nowyPosiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku nowyPosiadacz)
    {
        if (!rachunek.posiadaczeRachunku.Contains(nowyPosiadacz))
            throw new Exception("Posiadacza rachunku nie ma na liście!");
        if (rachunek.posiadaczeRachunku.Count == 1)
        {
            throw new Exception("Na liście musi być przynajmniej jeden posiadacz rachunku.");
        }
        rachunek.posiadaczeRachunku.Remove(nowyPosiadacz);
        return rachunek;
    }
    
    public override string ToString()
    {
        string result = $"Numer rachunku: {numer}\n";
        result += $"Stan rachunku: {stanRachunku}\n"; 
        
        result += "Posiadacze rachunku:\n";
        if (PosiadaczeRachunku.Count == 0)
        {
            result += "Brak posiadaczy rachunku.\n";
        }
        else
        {
            foreach (var posiadacz in posiadaczeRachunku)
            {
                result += posiadacz.ToString() + "\n";
            }
        }
        
        result += "Transakcje:\n";
        if (_Transakcje.Count == 0)
        {
            result += "Brak transakcji.\n";
        }
        else
        {
            foreach (var transakcja in _Transakcje)
            {
                result += transakcja.ToString() + "\n";
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        OsobaFizyczna osoba = new OsobaFizyczna("Jan", "Kowalski", "Piotr", "12345678901", null);
        OsobaFizyczna osoba1 = new OsobaFizyczna("Katarzyna", "Kowalska", "Anna", "33333333333", null);
        OsobaPrawna firma = new OsobaPrawna("XYZ Corp", "Warszawa");

        Console.WriteLine(osoba);
        Console.WriteLine(osoba1);
        Console.WriteLine(firma);

        RachunekBankowy rachunek1 = new RachunekBankowy("123-456", 1000m, true, new List<PosiadaczRachunku> { osoba });
        RachunekBankowy rachunek2 = new RachunekBankowy("789-012", 500m, false, new List<PosiadaczRachunku> { osoba1 });

        Console.WriteLine(rachunek1);
        Console.WriteLine(rachunek2);
        
        RachunekBankowy.DokonajTransakcji(rachunek1, rachunek2, 200m, "Przelew dla firmy");
        Console.WriteLine("Transakcja wykonana");
        
        Console.WriteLine("\nDodawanie i usuwanie posiadaczy rachunku:");
        rachunek1 += firma;
        rachunek1 -= firma;
        rachunek1 -= osoba;  
        
    }
}

