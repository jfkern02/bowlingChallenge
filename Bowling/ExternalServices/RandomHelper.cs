using Bowling.ExternalServices.Interfaces;

namespace Bowling.ExternalServices;

public class RandomHelper : IRandomHelper
{
    public int Next(int min, int max)
    {
        var rnd = new Random();
        return rnd.Next(min, max);
    }
}