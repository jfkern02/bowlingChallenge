namespace Bowling.Models;

public class Frame
{
    public int frameNumber { get; set; }
    
    public int score { get; set; }

    public bool isStrike { get; set; }

    public bool isSpare { get; set; }

    public int firstBowl { get; set; }

    public int secondBowl { get; set; }

    public int thirdBowl { get; set; }

    public bool isLastFrame { get; set; }

    public int extraScoreCount { get; set; }
}