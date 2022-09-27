namespace Bowling.Serivces.Interfaces;

public interface IBowlAction
{
    /// <summary>
    /// Bowls.
    /// </summary>
    /// <param name="numberOfPins">The number of pins left standing.</param>
    /// <returns>Score achieved on the bowl.</returns>
    public int BowlBall(int numberOfPins, bool showAnimation = false);

    /// <summary>
    /// Sets the type of ball you wish to use.
    /// </summary>
    public void BallSelection();

    /// <summary>
    /// Sets the type of show you want to use.
    /// </summary>
    public void ShoeSelection();
}