var input = File.ReadAllLines("./input.txt");

var totalPoints = 0L;
foreach (var line in input)
{
    var (_, winningNumbers, yourNumbers) = ParseCardInfo(line);
    var count = yourNumbers.Intersect(winningNumbers).Count();
    var points = (long)Math.Pow(2, count - 1);
    totalPoints += points;
}

Console.WriteLine(totalPoints);


(int Card, int[] WinningNumbers, int[] YourNumbers) ParseCardInfo(string line)
{
    var colonIndex = line.IndexOf(':');
    var pipeIndex = line.IndexOf('|');
    
    var card = int.Parse(line[..colonIndex].Replace("Card", "").Trim());
    var winningNumbers = line[(colonIndex + 1)..pipeIndex]
        .Trim()
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(n => int.Parse(n))
        .ToArray();
    var yourNumbers = line[(pipeIndex + 1)..]
        .Trim()
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(n => int.Parse(n))
        .ToArray();

    return (card, winningNumbers, yourNumbers);
}