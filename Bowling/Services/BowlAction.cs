using Bowling.Serivces.Interfaces;
using Bowling.Helpers;
using Bowling.ExternalServices.Interfaces;

namespace Bowling.Serivces;

public class BowlAction : IBowlAction
{
    private readonly IConsoleActions _consoleActions;
    private readonly IRandomHelper _randomHelper;

    private int _ball = 0;
    private int _shoe = 0;

    public BowlAction(IConsoleActions consoleActions, IRandomHelper randomHelper)
    {
        _consoleActions = consoleActions;
        _randomHelper = randomHelper;
    }

    public int BowlBall(int numberOfPins, bool showAnimation = true)
    {
        var rolls = 0;
        var totalRolls = 3 + BallModifier() + ShoeModifier();
        var score = 0;
        while (rolls < totalRolls) 
        {
            score = _randomHelper.Next(0, numberOfPins + 1);
            rolls++;
        }

        if (showAnimation)
        {
            _consoleActions.WriteLine("");
            _consoleActions.Write("Rolling the ball... ");
            using (var progress = new ProgressBar()) {
                for (int i = 0; i <= 100; i++) {
                    progress.Report((double) i / 100);
                    Thread.Sleep(5*totalRolls);
                }
            }
        }
        _consoleActions.WriteLine($"{score} pins were knocked down.");

        return score;
    }
    
    public void BallSelection()
    {
        while (true) 
        {
            _consoleActions.WriteLine("Select your ball from the choices below.");
            _consoleActions.WriteLine("1) Light : -1 roll.");
            _consoleActions.WriteLine("2) Medium : No change.");
            _consoleActions.WriteLine("3) Heavy : +1 roll.");

            var selection = _consoleActions.ReadLine();
            if (int.TryParse(selection, out int ball))
            {
                _ball = ball;
                break;
            }
            else
            {
                _consoleActions.WriteLine("The selected options was invalud. Please select a correct type.");
            }
        }
    }
    
    public void ShoeSelection()
    {
        while (true) 
        {
            _consoleActions.WriteLine("Select your shoe tpye from the choices below.");
            _consoleActions.WriteLine("1) Slick : +1 roll.");
            _consoleActions.WriteLine("2) Normal : No change.");
            _consoleActions.WriteLine("3) Grippy : -1 roll.");

            var selection = _consoleActions.ReadLine();
            if (int.TryParse(selection, out int shoe))
            {
                _shoe = shoe;
                break;
            }
            else
            {
                _consoleActions.WriteLine("The selected options was invalud. Please select a correct type.");
            }
        }
    }

    private int BallModifier() 
    {
        switch (_ball)
        {
            case 1:
                return -1;
            case 3:
                return 1;
            default:
                return 0;
        }
    }
    
    private int ShoeModifier() 
    {
        switch (_shoe)
        {
            case 1:
                return 1;
            case 3:
                return -1;
            default:
                return 0;
        }
    }
}