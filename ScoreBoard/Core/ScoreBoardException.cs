namespace ScoreBoard.Core;

/// <summary>
/// ScoreBoard exception
/// </summary>
public class ScoreBoardException:Exception
{
    /// <summary>
    /// ScoreBoard exception for logic exceptions
    /// </summary>
    /// <param name="message">Message String</param>
    public ScoreBoardException(string? message) : base(message)
    {
    }

    /// <summary>
    /// ScoreBoard exception for chain exceptions
    /// </summary>
    /// <param name="message">Message String</param>
    /// <param name="innerException">Inner Exception</param>
    public ScoreBoardException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}