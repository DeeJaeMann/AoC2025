
List<string> input = File.ReadAllLines("input.txt").ToList();
int count = 0;
int position = 50;

foreach (string line in input)
{
    int amount = 0;
    int refPos = position;
    char direction;
    Console.WriteLine(line);
    if (line.StartsWith("L"))
    {
        direction = 'L';
        amount = int.Parse(line.Split('L')[1]);
        position -= amount;
    }
    else
    {
        direction = 'R';
        amount = int.Parse(line.Split('R')[1]);
        position += amount;
    }

    position %= 100;

    if (position is 0)
    {
        count++;
    }

    count += CheckZeros(position, refPos, amount, direction);

}
Console.WriteLine($"Final count is {count}");

static int CheckZeros(int position, int refPos, int amount, char direction)
{
    int result = 0;

    if (position < 0) position = 100 + position;
    if (refPos < 0) refPos = 100 + refPos;
    
    Console.WriteLine($"Position: {position}, RefPos: {refPos}, Amount: {amount}");

    if (direction == 'L')
    {
        // Left
        if (refPos < position && position is not 0 && refPos is not 0)
        {
            Console.WriteLine("Add one Left");
            result++;
        }
    }
    else
    {
        // Right
        if (refPos > position && position is not 0 &&  refPos is not 0)
        {
            Console.WriteLine("Add one Right");
            result++;
        }
    }

    result += amount / 100;

    return result;
}