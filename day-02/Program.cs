var games = File.ReadAllLines("./input.txt");

long sum = 0;
foreach (var line in games)
{
    var sets = line[(line.IndexOf(':') + 1)..].Split(';').Select(set => ConvertToRgbValues(set.Split(',')));
    var power = sets.Aggregate(
        seed: (Red: 0, Green: 0, Blue: 0),
        (previous, current) => (
            Red: Math.Max(previous.Red, current.Red),
            Green: Math.Max(previous.Green, current.Green),
            Blue: Math.Max(previous.Blue, current.Blue)),
        set => set.Red * set.Green * set.Blue);
    sum += power;
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
        rgbCount[color] = Math.Max(rgbCount[color], count);
    }

    return (Red: rgbCount["red"], Green: rgbCount["green"], Blue: rgbCount["blue"]);
}

