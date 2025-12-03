using System.Diagnostics;

List<string> input =  File.ReadAllLines("input.txt").ToList();
List<int> maxJolts = new();

foreach (string line in input)
{
    #if DEBUG
    Debug.WriteLine(line);
    #endif
    var indexedDigits = line
        .Select((digit, index) => new { Value = int.Parse(digit.ToString()), Index = int.Parse(index.ToString()) });
    #if DEBUG
    foreach (var element in indexedDigits)
    {
        Debug.WriteLine(element);
    }
    #endif
    var firstMax = indexedDigits
        .OrderByDescending(digit => digit.Value)
        .First();
    var secondMax = indexedDigits
        .Where(digit => digit.Index > firstMax.Index)
        .OrderByDescending(digit => digit.Value)
        .FirstOrDefault();
    Debug.WriteLine($"firstMax: {firstMax}");
    Debug.WriteLine($"secondMax: {secondMax}");
    if (secondMax is null)
    {
        secondMax = firstMax;
        firstMax = indexedDigits
            .Where(digit => digit.Index < firstMax.Index)
            .OrderByDescending(digit => digit.Value)
            .FirstOrDefault();
        Debug.WriteLine("secondMax empty");
        Debug.WriteLine($"New firstMax: {firstMax}");
        Debug.WriteLine($"New secondMax: {secondMax}");
    }
    maxJolts.Add(int.Parse($"{firstMax.Value}{secondMax.Value}"));
}

#if DEBUG
foreach (int num in maxJolts)
{
    Debug.WriteLine(num);
}
#endif

Console.WriteLine($"Number of Jolts: {maxJolts.Count}");
Console.WriteLine($"Max Jolts: {maxJolts.Sum()}");