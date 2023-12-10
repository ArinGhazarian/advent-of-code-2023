var input = File.ReadAllLines("./input.txt");

var cards = new Dictionary<int, int>(Enumerable.Range(1, input.Length).Select(i => new KeyValuePair<int, int>(i, 1)));
foreach (var line in input)
{
    var (cardNumber, winningNumbers, yourNumbers) = ParseCardInfo(line);
    var cardsWon = yourNumbers.Intersect(winningNumbers).Count();
    var count = cards[cardNumber];
    for (var i = 1; i <= cardsWon; i++)
    {
        cards[cardNumber + i] += count;
    }
}

var totalCards = cards.Values.Sum(x => (long)x);
Console.WriteLine(totalCards);


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