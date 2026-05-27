using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Use a HashSet to remember words we have already seen so that lookups
        // are O(1). Walking the array once gives an overall O(n) solution.
        //
        // Optimization: encode each 2-character word as a single int
        // ((c0 << 16) | c1). Storing ints avoids allocating any new strings
        // (such as a reversed copy) just to perform set lookups, which keeps
        // the inner loop cheap and well within the O(n) efficiency budget.
        var seen = new HashSet<int>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            char first = word[0];
            char second = word[1];

            // Skip same-letter words like "aa" — their reverse is themselves
            // and the problem says these should never match.
            if (first == second)
            {
                continue;
            }

            int encoded = (first << 16) | second;
            int reversedEncoded = (second << 16) | first;

            // If we have already seen the reverse, we have a symmetric pair.
            if (seen.Contains(reversedEncoded))
            {
                pairs.Add($"{word} & {second}{first}");
            }
            else
            {
                // Otherwise remember this word so a later word can match it.
                seen.Add(encoded);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // The degree is column 4 (index 3). Trim to be safe against stray
            // whitespace from the file.
            var degree = fields[3].Trim();

            // Increment the running count for this degree, starting from 0
            // if it has not been seen before.
            if (degrees.ContainsKey(degree))
            {
                degrees[degree] += 1;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: drop spaces and ignore letter case, so that comparisons
        // like "Ab"/"Ba" and "Eleven plus Two"/"Twelve Plus One" work.
        var clean1 = word1.Replace(" ", "").ToLower();
        var clean2 = word2.Replace(" ", "").ToLower();

        // If the cleaned strings have different lengths they cannot be anagrams.
        if (clean1.Length != clean2.Length)
        {
            return false;
        }

        // Use a dictionary to count letter frequencies from the first word,
        // then subtract counts using the letters in the second word. If any
        // count goes negative or a letter is missing, the words are not anagrams.
        var letterCounts = new Dictionary<char, int>();

        foreach (var c in clean1)
        {
            if (letterCounts.ContainsKey(c))
            {
                letterCounts[c] += 1;
            }
            else
            {
                letterCounts[c] = 1;
            }
        }

        foreach (var c in clean2)
        {
            if (!letterCounts.ContainsKey(c) || letterCounts[c] == 0)
            {
                return false;
            }
            letterCounts[c] -= 1;
        }

        // Equal lengths plus all counts reduced to 0 means the words match.
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Build one formatted summary string per earthquake in the feed.
        // Format: "<place> - Mag <mag>" (e.g. "1km NE of Pahala, Hawaii - Mag 2.36").
        var summaries = featureCollection!.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray();

        return summaries;
    }
}