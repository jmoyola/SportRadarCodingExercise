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
        DbSession<ScoreBoardDbModel>? dbSession = null;
        try
        {
            // Assert
            if(homeTeam==null)
                throw new ArgumentNullException(nameof(homeTeam));
            if(awayTeam==null)
                throw new ArgumentNullException(nameof(awayTeam));

            // Creating dbSession
            using (dbSession = this.DbSessionFactory.NewSession())
            {
                // Add Teams to DB if not exists?? (is not specified in the exercise)
                // if(dbSession.Teams.Contains(homeTeam))
                //    dbSession.Teams.Add(homeTeam);
                // if(dbSession.Teams.Contains(awayTeam))
                //    dbSession.Teams.Add(awayTeam);

                // New Game Match
                GameMatch ret = new GameMatch(homeTeam, awayTeam);
                
                // Add new Game Match to DB
                dbSession.Model.GameMatches.Add(ret);
                
                // Start new Game Match
                ret.Start();
                
                dbSession.Commit(); // Commit model changes

                return ret;
            }
        }
        catch (Exception ex)
        {
            if(dbSession!=null)
                dbSession.Rollback(); // Rollback model changes
            
            throw new ScoreBoardException("Error starting a game: " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Finish a game match by Id
    /// </summary>
    /// <param name="gameMatchId">Game match Id</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void FinishById(Guid gameMatchId)
    {
        try
        {
            // Creating dbSession
            using (DbSession<ScoreBoardDbModel>? dbSession = this.DbSessionFactory.NewSession())
            {
                // Retrieving GameMatch for id
                GameMatch? gameMatch = dbSession.Model.GameMatches.FirstOrDefault(v => v.Id.Equals(gameMatchId));
                
                // DB errors
                if(gameMatch==null)
                    throw new ScoreBoardException("Don't exists a game match with id '" + gameMatchId + "'");
                
                this.Finish(gameMatch);
            }
        }
        catch (Exception ex)
        {
            throw new ScoreBoardException("Error finishing game match with id '" + gameMatchId + "': " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Finish a game match
    /// </summary>
    /// <param name="gameMatch">Game match</param>
    /// <exception cref="ScoreBoardException"></exception>
    public void Finish(GameMatch gameMatch)
    {
        // Assert
        if(gameMatch==null)
            throw new ArgumentNullException(nameof(gameMatch));

        DbSession<ScoreBoardDbModel>? dbSession = null;
        try
        {
            // Creating dbSession
            using (dbSession = this.DbSessionFactory.NewSession())
            {
                gameMatch.Finish();
                
                dbSession.Commit(); // Commit model changes
            }
        }
        catch (Exception ex)
        {
            if(dbSession!=null)
                dbSession.Rollback(); // Rollback model changes
            
            throw new ScoreBoardException("Error finishing game match '" + gameMatch.ToString() + "': " + ex.Message, ex);
        }
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
        DbSession<ScoreBoardDbModel>? dbSession = null;
        try
        {
            // Creating dbSession
            using (dbSession = this.DbSessionFactory.NewSession())
            {
                // Retrieving GameMatch for id
                GameMatch? gameMatch = dbSession.Model.GameMatches.FirstOrDefault(v => v.Id.Equals(gameMatchId));
                
                // DB errors
                if(gameMatch==null)
                    throw new ScoreBoardException("Don't exists a game match with id '" + gameMatchId + "'");
                
                this.UpdateScore(gameMatch, homeScore, awayScore);
            }
        }
        catch (Exception ex)
        {
            throw new ScoreBoardException("Error updating score for game match with id '"+ gameMatchId + "': " + ex.Message, ex);
        }        
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
        // Assert
        if(gameMatch==null)
            throw new ArgumentNullException(nameof(gameMatch));
        
        DbSession<ScoreBoardDbModel>? dbSession = null;
        try
        {
            // Creating dbSession
            using (dbSession = this.DbSessionFactory.NewSession())
            {
                gameMatch.HomeScore=homeScore;
                gameMatch.AwayScore = awayScore;
                
                dbSession.Commit(); // Commit model changes
            }
        }
        catch (Exception ex)
        {
            if(dbSession!=null)
                dbSession.Rollback(); // Rollback model changes
            
            throw new ScoreBoardException("Error updating score for game match '"+ gameMatch.ToString() + "': " + ex.Message, ex);
        }        
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