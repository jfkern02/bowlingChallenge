using Bowling.Serivces.Interfaces;
using Bowling.ExternalServices.Interfaces;
using Bowling.Games;

namespace Bowling.Tests.Games;

[TestClass]
public class GameTests
{
    private readonly Mock<IBowlAction> _bowlActionMock;
    private readonly Mock<IConsoleActions> _consoleActionsMock;

    private readonly Game _game;

    public GameTests()
    {
        _bowlActionMock = new Mock<IBowlAction>();
        _consoleActionsMock = new Mock<IConsoleActions>();

        _game = new Game(_bowlActionMock.Object, _consoleActionsMock.Object);
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());
        _bowlActionMock
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(1);

        // Act
        _game.PlayRandomGame();

        // Assert
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(20));
        Assert.AreEqual(20, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_Max_Score_Being_300()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());
        _bowlActionMock
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(10);

        // Act
        _game.PlayRandomGame();

        // Assert
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(12));
        Assert.AreEqual(300, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_Every_Frame_As_A_Spare()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());
        _bowlActionMock
            .SetupSequence(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1);

        // Act
        _game.PlayRandomGame();

        // Assert
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(21));
        Assert.AreEqual(110, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_Every_Frame_As_A_Spare_Except_Last_Frame_Leads_Strike()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());
        _bowlActionMock
            .SetupSequence(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(1)
            .Returns(9)
            .Returns(10)
            .Returns(9)
            .Returns(1);

        // Act
        _game.PlayRandomGame();

        // Assert
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(21));
        Assert.AreEqual(128, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_BowlActoin_Called_With_Correct_Parameters_With_Last_Frame_Spare()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());

        var parameterCall = new List<int>();
        var sequence = new MockSequence();
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);

        // Act
        _game.PlayRandomGame();

        // Assert
        var expected = new List<int>
        {
            10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10
        };
        CollectionAssert.AreEqual(expected, parameterCall);
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_BowlActoin_Called_With_Correct_Parameters_With_Last_Frame_Strike()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());

        var parameterCall = new List<int>();
        var sequence = new MockSequence();
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(10);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(9);
        _bowlActionMock.InSequence(sequence)
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(1);

        // Act
        _game.PlayRandomGame();

        // Assert
        var expected = new List<int>
        {
            10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 9, 10, 10, 1
        };
        CollectionAssert.AreEqual(expected, parameterCall, $"Values (expected/actual): {string.Join(",", expected.ToArray())}/{string.Join(",", parameterCall.ToArray())}");
    }

    [TestMethod]
    public void PlayRandomGame_Runs_Successfully_With_BowlActoin_Called_With_Correct_Parameters_With_Every_Frame_Strike()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());

        var parameterCall = new List<int>();
        _bowlActionMock
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Callback<int, bool>((i, b) => parameterCall.Add(i)).Returns(10);

        // Act
        _game.PlayRandomGame();

        // Assert
        var expected = new List<int>
        {
            10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10
        };
        CollectionAssert.AreEqual(expected, parameterCall, $"Values (expected/actual): {string.Join(",", expected.ToArray())}/{string.Join(",", parameterCall.ToArray())}");
    }

    [TestMethod]
    public void PlayManualGame_Runs_Successfully_With_No_Changes_To_Settings()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());

        _consoleActionsMock
            .Setup(x => x.ReadLine())
            .Returns("4");
        _bowlActionMock
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(10);

        // Act
        _game.PlayManualGame();

        // Assert
        _consoleActionsMock.Verify(x => x.ReadLine(), Times.Exactly(12));
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(12));
        Assert.AreEqual(300, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }

    
    [TestMethod]
    public void PlayManualGame_Runs_Successfully_With_Changes_To_Settings()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _bowlActionMock.Setup(x => x.BallSelection());
        _bowlActionMock.Setup(x => x.ShoeSelection());

        _consoleActionsMock
            .SetupSequence(x => x.ReadLine())
            .Returns("3")
            .Returns("4");
        _bowlActionMock
            .Setup(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(10);

        // Act
        _game.PlayManualGame();

        // Assert
        _consoleActionsMock.Verify(x => x.ReadLine(), Times.Exactly(12));
        _bowlActionMock.Verify(x => x.BowlBall(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(12));
        Assert.AreEqual(300, _game.Scoreboard.frames.Select(x => x.score).Sum());
    }
}