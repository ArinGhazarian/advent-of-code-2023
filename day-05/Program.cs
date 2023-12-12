var input = File.ReadAllLines("./input.txt");

var seeds = input.First().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(s => long.Parse(s))
    .ToArray();
var maps = new Dictionary<string, List<(long DestinationStart, long SourceStart, long Length)>>
{
    ["seed-to-soil"] = [],
    ["soil-to-fertilizer"] = [],
    ["fertilizer-to-water"] = [],
    ["water-to-light"] = [],
    ["light-to-temperature"] = [],
    ["temperature-to-humidity"] = [],
    ["humidity-to-location"] = []
};
var keys = new List<string>(7);

var currentKey = "";
foreach (var line in input.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)))
{
    if (maps.Keys.SingleOrDefault(k => line.StartsWith(k)) is { } key)
    {
        currentKey = key;
        keys.Add(key);
    }
    else
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
        maps[currentKey].Add((parts[0], parts[1], parts[2]));
    }
}

var locations = new List<long>(seeds.Length);
foreach (var seed in seeds)
{
    long next = seed;
    foreach (var key in keys)
    {
        next = GetNext(next, maps[key]);
    }

    locations.Add(next);
}

Console.WriteLine(locations.Min());

long GetNext(long seed, List<(long DestinationStart, long SourceStart, long Length)> map)
{
    if (map.SingleOrDefault(x => x.SourceStart <= seed && seed < x.SourceStart + x.Length) is (_, _, > 0) entry)
    {
        var diff = seed - entry.SourceStart;
        return entry.DestinationStart + diff;
    }

    return seed;
}