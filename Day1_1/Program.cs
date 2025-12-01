// See https://aka.ms/new-console-template for more information

List<string> input = File.ReadAllLines("input.txt").ToList();
int count = 0;
int position = 50;

foreach (string line in input)
{
    int amount = 0;
    if (line.StartsWith("L"))
    {
        amount = int.Parse(line.Split('L')[1]);
        position -= amount;
    }
    else
    {
        amount = int.Parse(line.Split('R')[1]);
        position += amount;
    }

    position %= 100;

    if (position is 0)
    {
        count++;
    }

}
Console.WriteLine($"Final count is {count}");
