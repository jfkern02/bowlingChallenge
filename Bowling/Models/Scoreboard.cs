namespace Bowling.Models;

public class Scoreboard
{
    public List<Frame> frames;

    public Scoreboard()
    {
        frames = new List<Frame>();
        AddFrames();
    }

    public void ShowScoreboard()
    {
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------");
        foreach (var frame in frames)
        {
            if (!frame.isLastFrame)
                Console.Write($"|{frame.firstBowl}/{frame.secondBowl}[{frame.score}]|");
            else
                Console.Write($"|{frame.firstBowl}/{frame.secondBowl}/{frame.thirdBowl}[{frame.score}]|");
        }
        Console.WriteLine("");
        Console.WriteLine($"Total Score: {frames.Select(x => x.score).Sum()}-------------------------");
        Console.WriteLine("");
    }

    private void AddFrames() 
    {
        for (int i = 0; i < 10; i++) 
        {
            if (frames.Count == 9)
            {
                frames.Add(new Frame {
                    frameNumber = i,
                    isLastFrame = true
                });
            }
            else 
            {
                frames.Add(new Frame {
                    frameNumber = i
                });
            }
        }
    }
}