var games = File.ReadAllLines("./input.txt");

var fact = (Red: 12, Green: 13, Blue: 14);

long sum = 0;
foreach (var line in games)
{
    var gameId = int.Parse(line[..line.IndexOf(':')].Replace("Game ", ""));
    var sets = line[(line.IndexOf(':') + 1)..].Split(';')
        .Select(set => ConvertToRgbValues(set.Split(',')));
    if (IsPossibleGame(sets))
    {
        sum += gameId;
    }
}

Console.WriteLine(sum);

(int Red, int Green, int Blue) ConvertToRgbValues(IEnumerable<string> values)
{
    var rgbCount = new Dictionary<string, int> { ["red"] = 0, ["green"] = 0, ["blue"] = 0 };
    
    foreach (var value in values)
    {
        var combo = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var count = int.Parse(combo[0].Trim());
        var color = combo[1].Trim();
        rgbCount[color] += count;
    }

    return (Red: rgbCount["red"], Green: rgbCount["green"], Blue: rgbCount["blue"]);
}

bool IsPossibleGame(IEnumerable<(int Red, int Green, int Blue)> sets) =>
    sets.All(set => set.Red <= fact.Red && set.Green <= fact.Green && set.Blue <= fact.Blue);

