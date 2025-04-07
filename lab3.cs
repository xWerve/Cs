/* 
 * ZADANIE: Przetwarzanie danych tweetów w formacie JSONL
 *
 * 1. Wczytaj dane z pliku JSONL zawierającego tweety.
 * 2. Zaimplementuj zapis oraz odczyt tych danych w formacie XML.
 * 3. Dodaj możliwość sortowania tweetów:
 *    a) według nazw użytkowników,
 *    b) według daty utworzenia (uwaga: przekształć `CreatedAt` na `DateTime`).
 * 4. Znajdź i wyświetl najnowszy oraz najstarszy tweet.
 * 5. Pogrupuj tweety według użytkowników i wyświetl, ilu tweetów każdy z nich jest autorem.
 * 6. Policz częstość występowania słów we wszystkich tweetach (ignorując wielkość liter oraz znaki interpunkcyjne).
 * 7. Wyświetl 10 najczęściej występujących słów, które mają co najmniej 5 znaków.
 * 8. Oblicz i wypisz 10 słów o najwyższym wskaźniku IDF (Inverse Document Frequency).
 *
 * Użyj menu tekstowego, by umożliwić użytkownikowi wybór opcji. Program powinien działać w konsoli.
 * Dodaj obsługę błędów (np. brak plików, błędny format) oraz zadbaj o poprawne sortowanie po dacie.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

public class Tweet
{
    [JsonPropertyName("Text")]
    public string Text { get; set; }

    [JsonPropertyName("UserName")]
    public string UserName { get; set; }

    [JsonPropertyName("LinkToTweet")]
    public string LinkToTweet { get; set; }

    [JsonPropertyName("FirstLinkUrl")]
    public string FirstLinkUrl { get; set; }

    [JsonPropertyName("CreatedAt")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("TweetEmbedCode")]
    public string TweetEmbedCode { get; set; }

    // Pomocnicze pole do sortowania po dacie
    [XmlIgnore]
    public DateTime CreatedDate => DateTime.TryParse(CreatedAt, out var dt) ? dt : DateTime.MinValue;
}

public class Program
{
    static string jsonPath = @"C:\Users\mkrol\RiderProjects\lab3\lab3\favorite-tweets.jsonl";
    static string xmlPath = @"C:\Users\mkrol\RiderProjects\lab3\lab3\tweets.xml";

    public static List<Tweet> LoadTweets(string path)
    {
        var tweets = new List<Tweet>();
        if (!File.Exists(path))
        {
            Console.WriteLine("Plik nie istnieje!");
            return tweets;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            try
            {
                var tweet = JsonSerializer.Deserialize<Tweet>(line);
                if (tweet != null)
                    tweets.Add(tweet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas parsowania linii: {ex.Message}");
            }
        }

        return tweets;
    }

    public static void SaveToXml(List<Tweet> tweets, string path)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(List<Tweet>));
            using var writer = new StreamWriter(path);
            serializer.Serialize(writer, tweets);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu do XML: {ex.Message}");
        }
    }

    public static List<Tweet> LoadFromXml(string path)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(List<Tweet>));
            using var reader = new StreamReader(path);
            return (List<Tweet>)serializer.Deserialize(reader);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania z XML: {ex.Message}");
            return new List<Tweet>();
        }
    }

    public static List<Tweet> SortedByUserName(List<Tweet> tweets) =>
        tweets.OrderBy(t => t.UserName).ToList();

    public static List<Tweet> SortedByDate(List<Tweet> tweets) =>
        tweets.OrderBy(t => t.CreatedDate).ToList();

    public static void PrintOldestAndNewest(List<Tweet> tweets)
    {
        var latest = tweets.MaxBy(t => t.CreatedDate);
        var oldest = tweets.MinBy(t => t.CreatedDate);
        Console.WriteLine($"Najnowszy tweet: {latest.CreatedAt} -> {latest.Text}");
        Console.WriteLine($"Najstarszy tweet: {oldest.CreatedAt} -> {oldest.Text}");
    }

    public static void GroupByUser(List<Tweet> tweets)
    {
        var grouped = tweets.GroupBy(t => t.UserName)
                            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var kv in grouped)
        {
            Console.WriteLine($"Użytkownik: {kv.Key}, liczba tweetów: {kv.Value.Count}");
        }
    }

    public static Dictionary<string, int> CountWords(List<Tweet> tweets)
    {
        var wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var tweet in tweets)
        {
            var words = tweet.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var clean = word.Trim().ToLower().Trim('.', ',', '!', '?', ':', ';', '"');
                if (string.IsNullOrWhiteSpace(clean)) continue;

                if (wordCount.ContainsKey(clean))
                    wordCount[clean]++;
                else
                    wordCount[clean] = 1;
            }
        }
        return wordCount;
    }

    public static void Top10Words(List<Tweet> tweets)
    {
        var wordCount = CountWords(tweets);
        var topWords = wordCount
            .Where(kv => kv.Key.Length >= 5)
            .OrderByDescending(kv => kv.Value)
            .Take(10);

        foreach (var word in topWords)
            Console.WriteLine($"{word.Key}: {word.Value}");
    }

    public static void CalculateIDF(List<Tweet> tweets)
    {
        int totalDocs = tweets.Count;
        var docFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var tweet in tweets)
        {
            var uniqueWords = new HashSet<string>(
                tweet.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                          .Select(w => w.ToLower().Trim('.', ',', '!', '?', ':', ';', '"'))
            );

            foreach (var word in uniqueWords)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;

                if (!docFrequency.ContainsKey(word))
                    docFrequency[word] = 1;
                else
                    docFrequency[word]++;
            }
        }

        var idfValues = docFrequency
            .Select(kv => new { Word = kv.Key, Value = Math.Log(totalDocs / (1.0 + kv.Value)) })
            .OrderByDescending(x => x.Value)
            .Take(10);

        foreach (var entry in idfValues)
            Console.WriteLine($"{entry.Word}: {entry.Value:F4}");
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var tweets = LoadTweets(jsonPath);
        Console.WriteLine($"Wczytano {tweets.Count} tweetów.");

        Console.WriteLine("Wybierz opcję:");
        Console.WriteLine("1 - Zapisz do XML");
        Console.WriteLine("2 - Wczytaj z XML");
        Console.WriteLine("3 - Sortuj po użytkownikach");
        Console.WriteLine("4 - Sortuj po dacie");
        Console.WriteLine("5 - Najnowszy i najstarszy tweet");
        Console.WriteLine("6 - Grupowanie tweetów po użytkownikach");
        Console.WriteLine("7 - Top 10 najczęstszych słów (min 5 znaków)");
        Console.WriteLine("8 - Oblicz IDF");
        Console.WriteLine("0 - Wyjście");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                SaveToXml(tweets, xmlPath);
                Console.WriteLine("Zapisano do XML.");
                break;
            case "2":
                var fromXml = LoadFromXml(xmlPath);
                Console.WriteLine($"Wczytano {fromXml.Count} tweetów z XML.");
                break;
            case "3":
                var sortedByUser = SortedByUserName(tweets);
                sortedByUser.ForEach(t => Console.WriteLine($"{t.UserName}: {t.Text}"));
                break;
            case "4":
                var sortedByDate = SortedByDate(tweets);
                sortedByDate.ForEach(t => Console.WriteLine($"{t.CreatedDate:yyyy-MM-dd HH:mm}: {t.Text}"));
                break;
            case "5":
                PrintOldestAndNewest(tweets);
                break;
            case "6":
                GroupByUser(tweets);
                break;
            case "7":
                Top10Words(tweets);
                break;
            case "8":
                CalculateIDF(tweets);
                break;
            case "0":
                Console.WriteLine("Koniec.");
                break;
            default:
                Console.WriteLine("Nieznana opcja.");
                break;
        }
    }
}
