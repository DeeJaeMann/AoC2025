using System.Diagnostics;
using System.Text.RegularExpressions;

string test1 = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
string input =
    "96952600-96977512,6599102-6745632,32748217-32835067,561562-594935,3434310838-3434398545,150-257,864469-909426,677627997-677711085,85-120,2-19,3081-5416,34-77,35837999-36004545,598895-706186,491462157-491543875,5568703-5723454,6262530705-6262670240,8849400-8930122,385535-477512,730193-852501,577-1317,69628781-69809331,2271285646-2271342060,282-487,1716-2824,967913879-967997665,22-33,5722-11418,162057-325173,6666660033-6666677850,67640049-67720478,355185-381658,101543-146174,24562-55394,59942-93946,967864-1031782";
List<string> rawList = input.Split(',').ToList();
List<long> invalidID = new();

foreach (string line in rawList)
{
    Debug.WriteLine(line);
    // Split the numbers
    List<string> parseLine = line.Split('-').ToList();
    string firstId = parseLine[0];
    string lastId = parseLine[1];
    Debug.WriteLine($"firsdId: {firstId} secondId: {lastId}");
    // Check for leading zeros
    if (CheckLeadingZero(firstId))
    {
        invalidID.Add(long.Parse(firstId));
        parseLine.Remove(firstId);
        Debug.WriteLine($"Added number {firstId} to invalidId");
    }
    
    if (CheckLeadingZero(lastId))
    {
        invalidID.Add(long.Parse(lastId));
        parseLine.Remove(lastId);
        Debug.WriteLine($"Added number {lastId} to invalidId");
    }

    // If we have a leading zero, the range is invalid
    if (parseLine.Count < 2) continue;
    
    List<long> range = BuildRange(long.Parse(firstId), long.Parse(lastId));
    
    #if DEBUG
    foreach (long num in range)
    {
        Debug.Write($"{num},");
    }
    Debug.WriteLine("");
    #endif

    foreach (long num in range)
    {
        // Check for a pattern match
        // Pattern starts at the beginning of the string
        // Checks for a duplicate pattern to the end of the string
        if (Regex.IsMatch(num.ToString(), @"(^\d+)\1$"))
        {
            invalidID.Add(num);
        }
    }
}
#if DEBUG
Debug.WriteLine("List of invalid IDs");
foreach (long num in invalidID)
{
    Debug.Write($"{num},");
}
#endif
Console.WriteLine($"{invalidID.Count} invalidIDs found");
Console.WriteLine($"Sum of IDs: {invalidID.Sum()}");

static bool CheckLeadingZero(string thisId)
{
    return thisId.StartsWith('0');
}

static List<long> BuildRange(long start, long end)
{
    List<long> thisRange = new();
    for (long num = start; num <= end; num++)
    {
        thisRange.Add(num);
    }
    return thisRange;
}