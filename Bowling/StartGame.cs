using Bowling.ExternalServices.Interfaces;
using Bowling.Games.Interfaces;

namespace Bowling;

public class StartGame
{
    private readonly IGame _game;
    private readonly IConsoleActions _consoleActions;

    public StartGame(IGame game, IConsoleActions consoleActions)
    {
        _game = game;
        _consoleActions = consoleActions;
    }

    public void Start() 
    {
        while (true)
        {
            _consoleActions.WriteLine("-----------------------------------------");
            _consoleActions.WriteLine("Time to bowl! Want to see a random game or manual game?");
            _consoleActions.WriteLine("1) Random Game");
            _consoleActions.WriteLine("2) Manual Game");
            _consoleActions.WriteLine("3) Exit");
            var options = new HashSet<int>{1, 2, 3};

            var gameTypeString = _consoleActions.ReadLine();
            var parsed = int.TryParse(gameTypeString, out int gameType);
            
            if (parsed && options.Contains(gameType)) 
            {
                if (gameType == 1) 
                    _game.PlayRandomGame();
                    
                if (gameType == 2)
                    _game.PlayManualGame();

                if (gameType == 3)
                    break;
            }
            else
            {
                _consoleActions.WriteLine("Supplied option was invalid.");
            }
        }
    }
}