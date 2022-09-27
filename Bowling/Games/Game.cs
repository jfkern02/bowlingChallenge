using Bowling.Serivces.Interfaces;
using Bowling.Games.Interfaces;
using Bowling.ExternalServices.Interfaces;
using Bowling.Models;

namespace Bowling.Games;

public class Game : IGame
{
    public Scoreboard Scoreboard;

    private readonly IBowlAction _bowlAction;
    private readonly IConsoleActions _consoleActions;

    private Queue<Bowl> bowls;
    private Queue<Frame> strikeQueue;
    private Queue<Frame> spareQueue;

    public Game(IBowlAction bowlAction, IConsoleActions consoleActions)
    {
        _bowlAction = bowlAction;
        _consoleActions = consoleActions;
    }

    public void PlayRandomGame()
    {
        SetupGame();
        
        while (bowls.Count > 0)
        {
            PerformBowl();
        }
    
        Scoreboard.ShowScoreboard();
    }

    public void PlayManualGame()
    {
        SetupGame();
        
        while (bowls.Count > 0)
        {
            FrameSetup();

            PerformBowl();

            if (bowls.Count > 0)
                Scoreboard.ShowScoreboard();
        }
    
        Scoreboard.ShowScoreboard();
    }

    private void SetupGame()
    {
        _bowlAction.BallSelection();
        _bowlAction.ShoeSelection();

        Scoreboard = new Scoreboard();
        
        strikeQueue = new Queue<Frame>();
        spareQueue = new Queue<Frame>();
        bowls = new Queue<Bowl>();
        for (int i = 0; i < 10; i++) 
        {
            bowls.Enqueue(new Bowl{
                frame = Scoreboard.frames[i]
            });
            bowls.Enqueue(new Bowl{
                frame = Scoreboard.frames[i]
            });

            if (Scoreboard.frames[i].isLastFrame)
            {
                bowls.Enqueue(new Bowl{
                    frame = Scoreboard.frames[i]
                });
            }
        }
    }

    private void PerformBowl()
    {
        _consoleActions.WriteLine(strikeQueue.Count.ToString());
        var bowl = bowls.Dequeue();
        var bowlFrame = bowl.frame;
        var frame = Scoreboard.frames.Where(x => x.frameNumber == bowlFrame.frameNumber).First();

        var pinsRemove = frame.score;

        if ((bowls.Count == 1 && frame.isStrike) || (bowls.Count == 0 && frame.isSpare))
            pinsRemove = 0;
        if (bowls.Count == 0 && frame.isStrike)
            pinsRemove = frame.secondBowl == 10 ? 0 : frame.secondBowl;

        var score = _bowlAction.BowlBall(10 - pinsRemove);

        if (strikeQueue.Count > 0)
        {
            var dequeuStrike = false;
            foreach (Frame strikeFrame in strikeQueue) 
            {
                strikeFrame.score += score;
                strikeFrame.extraScoreCount--;

                if (strikeFrame.extraScoreCount == 0)
                    dequeuStrike = true;
            }

            if (dequeuStrike)
                strikeQueue.Dequeue();
        }

        if (spareQueue.Count > 0)
        {
            foreach (Frame spareFrame in spareQueue) 
            {
                spareFrame.score += score;
                spareFrame.extraScoreCount--;
            }

            spareQueue.Dequeue();
        }

        frame.score += score;

        if (!frame.isLastFrame) 
        {
            var bowlNumber = 0;
            var nextBowl = bowls.Peek();
            var nextFrame = nextBowl.frame;
            if (nextFrame.frameNumber == frame.frameNumber)
            {
                frame.firstBowl = score;
                bowlNumber = 1;
            }
            else
            {
                frame.secondBowl = score;
                bowlNumber = 2;
            }

            if (score == 10 && bowlNumber == 1)
            {
                frame.isStrike = true;
                frame.extraScoreCount = 2;
                strikeQueue.Enqueue(frame);
                bowls.Dequeue();

                _consoleActions.WriteLine("You got a strike! Great Job!");
            }
            else if (frame.score == 10)
            {
                frame.isSpare = true;
                frame.extraScoreCount = 1;
                spareQueue.Enqueue(frame);

                _consoleActions.WriteLine("You got a spare! Good Job!");
            }
        }
        else 
        {
            if (bowls.Count == 2)
                frame.firstBowl = score;
            else if (bowls.Count == 1)
                frame.secondBowl = score;
            else
                frame.thirdBowl = score;

            if (score == 10 && frame.score % 10 == 0)
            {
                frame.isStrike = true;

                _consoleActions.WriteLine("You got a strike! Great Job!");
            }
            else if (frame.score == 10 && score != 0)
            {
                frame.isSpare = true;

                _consoleActions.WriteLine("You got a spare! Good Job!");
            }
            
            if (!frame.isStrike && !frame.isSpare && bowls.Count == 1)
                bowls.Dequeue();
        }
    }

    private void FrameSetup()
    {
        var currentFrame = bowls.Peek().frame;
        _consoleActions.WriteLine("---------------------------------------");
        _consoleActions.WriteLine($"Frame {currentFrame.frameNumber + 1}");
        _consoleActions.WriteLine("1) Change Shoes");
        _consoleActions.WriteLine("2) Changes Ball");
        _consoleActions.WriteLine("3) Change Both");
        _consoleActions.WriteLine("4) Bowl");
        var options = new HashSet<int>{1, 2, 3, 4};

        var gameTypeString = _consoleActions.ReadLine();
        var parsed = int.TryParse(gameTypeString, out int gameType);

        if (parsed && options.Contains(gameType)) 
        {
            if (gameType == 1) 
                _bowlAction.ShoeSelection();
                
            if (gameType == 2)
                _bowlAction.BallSelection();

            if (gameType == 3)
            {
                _bowlAction.ShoeSelection();
                _bowlAction.BallSelection();
            }
        }
        else
        {
            _consoleActions.WriteLine("Supplied option was invalid. Performing next Bowl.");
        }
    }
}