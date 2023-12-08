var input = File.ReadAllLines("./input.txt");

var partNumbers = new List<long>();
for (var row = 0; row < input.Length; row++)
{
    partNumbers.AddRange(FindPartNumbers(input[row], input, row));
}

Console.WriteLine(partNumbers.Sum());

IEnumerable<long> FindPartNumbers(string line, string[] input, int row)
{
    foreach (var (number, position) in ExtractNumbers(line))
    {
        if (IsPartNumber(number, input, row, position))
        {
            yield return number;
        }
    }
}

IEnumerable<(long Number, int Position)> ExtractNumbers(string line)
{
    var startIndex = 0;
    while (startIndex < line.Length)
    {
        var (number, position) = ExtractNextNumber(line, startIndex);

        if (number is null)
        {
            yield break;
        }

        yield return (number.Value, position);

        startIndex = position + number.Value.ToString().Length;
    }
}

(long? Number, int Position) ExtractNextNumber(string line, int startIndex)
{
    while (startIndex < line.Length && !char.IsDigit(line[startIndex])) startIndex++;

    if (startIndex >= line.Length)
    {
        return (null, -1);
    }

    var num = string.Join("", line.Skip(startIndex).TakeWhile(c => char.IsDigit(c)));
    return long.TryParse(num, out long result) ? (result, startIndex) : (null, -1);
}

bool IsPartNumber(long number, string[] input, int row, int col)
{
    var numLength = number.ToString().Length;

    for (int r = row - 1; r <= row + 1; r++)
    {
        if (r < 0 || r >= input.Length)
        {
            continue;
        }

        var leftCol = Math.Max(0, col - 1);
        var rightCol = Math.Min(col + numLength + 1, input[row].Length);
        for (int c = leftCol; c < rightCol; c++)
        {
            if (IsSymbol(input[r][c]))
            {
                return true;
            }
        }
    }

    return false;
}

bool IsSymbol(char c) => !char.IsDigit(c) && c != '.';