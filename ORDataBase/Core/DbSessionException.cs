namespace ORDataBase.Core;

/// <summary>
/// Data Base session exception
/// </summary>
public class DbSessionException:Exception
{
    /// <summary>
    /// ScoreBoard exception for logic exceptions
    /// </summary>
    /// <param name="message">Message String</param>
    public DbSessionException(string? message) : base(message)
    {
    }

    /// <summary>
    /// ScoreBoard exception for chain exceptions
    /// </summary>
    /// <param name="message">Message String</param>
    /// <param name="innerException">Inner Exception</param>
    public DbSessionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}