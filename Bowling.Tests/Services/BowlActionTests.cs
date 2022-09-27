using Bowling.ExternalServices.Interfaces;
using Bowling.Serivces;

namespace Bowling.Tests.Services;

[TestClass]
public class BowlActionTests
{
    private readonly Mock<IConsoleActions> _consoleActionsMock;
    private readonly Mock<IRandomHelper> _randomHelperMock;

    private readonly BowlAction _bowlAction;

    public BowlActionTests()
    {
        _consoleActionsMock = new Mock<IConsoleActions>();
        _randomHelperMock = new Mock<IRandomHelper>();

        _bowlAction = new BowlAction(_consoleActionsMock.Object, _randomHelperMock.Object);
    }

    [TestMethod]
    public void BowlBall_Returns_Successfully()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _randomHelperMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));

        // Act
        var result = _bowlAction.BowlBall(10, showAnimation: false);

        // Assert
        _randomHelperMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(3));
        Assert.AreEqual(typeof(int), result.GetType());
    }

    [TestMethod]
    public void BowlBall_Returns_Successfully_With_Light_Ball_And_Grippy_Shoes()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _randomHelperMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));
        _consoleActionsMock.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("3");

        // Act
        _bowlAction.BallSelection();
        _bowlAction.ShoeSelection();
        var result = _bowlAction.BowlBall(10, showAnimation: false);

        // Assert
        _randomHelperMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        Assert.AreEqual(typeof(int), result.GetType());
    }

    [TestMethod]
    public void BowlBall_Returns_Successfully_With_Heavy_Ball_And_Slick_Shoes()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _randomHelperMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));
        _consoleActionsMock.SetupSequence(x => x.ReadLine())
            .Returns("3")
            .Returns("1");

        // Act
        _bowlAction.BallSelection();
        _bowlAction.ShoeSelection();
        var result = _bowlAction.BowlBall(10, showAnimation: false);

        // Assert
        _randomHelperMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(5));
        Assert.AreEqual(typeof(int), result.GetType());
    }

    [TestMethod]
    public void BowlBall_Returns_Successfully_With_Medium_Ball_And_Normal_Shoes()
    {
        // Arrange
        _consoleActionsMock.Setup(x => x.WriteLine(It.IsAny<string>()));
        _consoleActionsMock.Setup(x => x.Write(It.IsAny<string>()));
        _randomHelperMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));
        _consoleActionsMock.SetupSequence(x => x.ReadLine())
            .Returns("2")
            .Returns("2");

        // Act
        _bowlAction.BallSelection();
        _bowlAction.ShoeSelection();
        var result = _bowlAction.BowlBall(10, showAnimation: false);

        // Assert
        _randomHelperMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(3));
        Assert.AreEqual(typeof(int), result.GetType());
    }
}