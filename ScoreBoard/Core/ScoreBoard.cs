using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ORDataBase.Core;
using ScoreBoardModel.Entities;
using ScoreBoardModel.Model;

namespace ScoreBoard.Core;

public class ScoreBoard
{
    /// <summary>
    /// Dependency Injection for Database
    /// </summary>
    public DbSessionFactory<ScoreBoardDbModel> DbSessionFactory { get; set; }

    /// <summary>
    /// Get Actual Scoreboard
    /// </summary>
    /// <returns>Return scoreboard</returns>
    /// <exception cref="ScoreBoardException"></exception>
    public IEnumerable<GameMatch> GetScoreBoard()
    {
        try
        {
            // Creating dbSession
            using (DbSession<ScoreBoardDbModel> dbSession = this.DbSessionFactory.NewSession())
            {
                return dbSession.Model.GameMatches.Where(v => v.IsStarted && !v.IsFinish);
            }
        }
        catch (Exception ex)
        {
            throw new ScoreBoardException("Error getting scoreboard: " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Start a game
    /// </summary>
    /// <param name="homeTeam">Local team</param>
    /// <param name="awayTeam">Away team</param>
    /// <returns>Return new started game match</returns>
    /// <exception cref="ScoreBoardException"></exception>
    public GameMatch StartGame(Team homeTeam,Team awayTeam)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Finish a game match by Id
    /// </summary>
    /// <param name="gameMatchId">Game match Id</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void FinishById(Guid gameMatchId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Finish a game match
    /// </summary>
    /// <param name="gameMatch">Game match</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void Finish(GameMatch gameMatch)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update a game match score by Id
    /// </summary>
    /// <param name="gameMatchId">Id for game match to update</param>
    /// <param name="homeScore">New Home Score</param>
    /// <param name="awayScore">New Away Score</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void UpdateScoreById(Guid gameMatchId, Int32 homeScore, Int32 awayScore)
    {
        throw new NotImplementedException();
    } 

    /// <summary>
    /// Update a game match score
    /// </summary>
    /// <param name="gameMatch">Game match to update</param>
    /// <param name="homeScore">New Home Score</param>
    /// <param name="awayScore">New Away Score</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void UpdateScore(GameMatch gameMatch, Int32 homeScore, Int32 awayScore)
    {
        throw new NotImplementedException();
    } 
    
    /// <summary>
    /// Return Games summary
    /// </summary>
    /// <returns>Return Games summary ordered</returns>
    /// <exception cref="ScoreBoardException"></exception>
    public IList<GameMatch> GetSummary()
    {
        throw new NotImplementedException();
    } 
}