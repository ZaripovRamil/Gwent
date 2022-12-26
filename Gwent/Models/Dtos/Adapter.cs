namespace Models.Dtos;

public class Adapter
{
    public static (long, long, long) Adapt(string s)
    {
        if (s.Length > 24) throw new ArgumentException("Can't pass more than 24 characters");
        var chars = s.ToList();
        while (chars.Count < 24)
            chars.Add((char) 0);
        return (Adapt(chars.GetRange(0, 8)), Adapt(chars.GetRange(8, 8)), Adapt(chars.GetRange(16, 8)));
    }

    private static long Adapt(List<char> chars)
    {
        if (chars.Count != 8) throw new ArgumentException("Incorrect char list size");
        if (chars.Any(c => c > 255)) throw new ArgumentException("Impossible characters");
        return chars.Aggregate<char, long>(0, (current, c) => current * 256 + c);
    }

    public static string ParseString(long part1, long part2, long part3)
    {
        return new string(ParseCharArray(part1).Concat(ParseCharArray(part2)).Concat(ParseCharArray(part3)).TakeWhile(c => c != 0).ToArray());
    }

    private static IEnumerable<char> ParseCharArray(long part)
    {
        var res = new char[8];
        var index = 7;
        while (part > 0)
        {
            res[index] = (char) (part % 256);
            index--;
            part /= 256;
        }

        return res;
    }

    public static long Adapt(IEnumerable<byte> cards)
    {
        
        return cards.Aggregate<byte, long>(0, (current, card) =>
        {
            Console.WriteLine(card+" "+current);
            return current * 256 + card;
        });
    }

    public static IEnumerable<byte> ParseCards(long cards)
    {
        var res = new List<byte>();
        while (cards > 0)
        {
            res.Add((byte)(cards % 256));
            cards /= 256;
        } 
        res.Reverse();
        return res;
    }
}