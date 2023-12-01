var values = File.ReadAllLines("./input.txt");

var numbers = new[]
{
    (Word: "one", Digit: 1),
    (Word: "two", Digit: 2),
    (Word: "three", Digit: 3),
    (Word: "four", Digit: 4),
    (Word: "five", Digit: 5),
    (Word: "six", Digit: 6),
    (Word: "seven", Digit: 7),
    (Word: "eight", Digit: 8),
    (Word: "nine", Digit: 9)
};

var sum = 0L;
foreach (var value in values)
{
    var digits = FindDigits(value).ToArray();
    var calibrationValue = int.Parse($"{digits.First()}{digits.Last()}");
    sum += calibrationValue;
}

Console.Write(sum);
return;

IEnumerable<int> FindDigits(string value)
{
    for (var index = 0; index < value.Length; index += 1)
    {
        if (char.IsDigit(value[index]))
        {
            yield return value[index] - '0';
        }
        else if (numbers.FirstOrDefault(n => value[index..].StartsWith(n.Word)) is (not null, _) number)
        {
            yield return number.Digit;
        }
    }
}