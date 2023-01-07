using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ORDataBase.Core;
using ScoreBoardModel.Entities;
using ScoreBoardModel.Model;

namespace ScoreBoardTest;

[TestFixture]
public class Tests
{
    private ScoreBoard.Core.ScoreBoard _scoreBoard;
    private ScoreBoardDbModel dbModel;
    
    [SetUp]
    public void Setup()
    {
        // Initialize Test setups
        this._scoreBoard = new ScoreBoard.Core.ScoreBoard(); // Main instance of library
        this.dbModel = new ScoreBoardDbModel(); // Initialize in-memory database
        // Setting dbSessionFactory fo dbModel to instance of library
        this._scoreBoard.DbSessionFactory = new DbSessionFactory<ScoreBoardDbModel>(dbModel); 
    }

    [Test]
    public void ScoreBoard_StartGame_Test()
    {
        Team homeTeam;
        Team awayTeam;

        Debug.WriteLine("ScoreBoard_StartGame_Test Start...");
        
        // Creating teams for game
        homeTeam = new Team("homeTeam");
        awayTeam = new Team("awayTeam");
        
        // Starting game
        this._scoreBoard.StartGame(homeTeam, awayTeam);
        
        Debug.WriteLine("ScoreBoard:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Getting storeboard
        var sb = this._scoreBoard.GetScoreBoard();
        
        // Is ok if count is 1 and exists any game with match in base properties.
        Assert.IsTrue(sb.Count()==1
                      && sb.Any(
                        v=>v.HomeTeam.Equals(homeTeam) && v.AwayTeam.Equals(awayTeam) 
                        && v.HomeScore==0 && v.AwayScore==0)
                      );
        
        Debug.WriteLine("ScoreBoard_StartGame_Test End...");
    }
    
    [Test]
    public void ScoreBoard_UpdateScore_Test()
    {
        Team homeTeam;
        Team awayTeam;

        Debug.WriteLine("ScoreBoard_UpdateScore_Test Start...");
        
        // Creating teams for game
        homeTeam = new Team("homeTeam");
        awayTeam = new Team("awayTeam");
        
        // Starting game
        this._scoreBoard.StartGame(homeTeam, awayTeam);
        
        Debug.WriteLine("ScoreBoard before update score:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Getting first (and only) game in scoreboard
        GameMatch? gameMatch=this._scoreBoard.GetScoreBoard().FirstOrDefault();

        Debug.WriteLine("Changing score for '" + gameMatch.ToString() + "' to 5 - 7...");
        
        // Changing gameMatch scores
        this._scoreBoard.UpdateScore(gameMatch, 5, 7);

        Debug.WriteLine("ScoreBoard after update score"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Retrieving another time scoreboard
        var sb = this._scoreBoard.GetScoreBoard();
        
        // Its ok if gameMatch scores are changed
        Assert.IsTrue(gameMatch.HomeScore==5 && gameMatch.AwayScore==7);

        Debug.WriteLine("ScoreBoard_UpdateScore_Test End...");
    }
    
    [Test]
    public void ScoreBoard_FinishGame_Test()
    {
        Team homeTeam;
        Team awayTeam;

        Debug.WriteLine("ScoreBoard_FinishGame_Test Start...");
        
        // Creating teams for game
        homeTeam = new Team("homeTeam");
        awayTeam = new Team("awayTeam");
        
        // Starting game
        this._scoreBoard.StartGame(homeTeam, awayTeam);
        
        // Getting scoreboard content before finish game 
        GameMatch? gameMatch=this._scoreBoard.GetScoreBoard().FirstOrDefault();
        
        Debug.WriteLine("ScoreBoard before finish game:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Finish game
        this._scoreBoard.Finish(gameMatch);

        
        Debug.WriteLine("ScoreBoard after finish game:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Getting scoreboard content after finish game
        var sb = this._scoreBoard.GetScoreBoard();
        
        // Its ok if in scoreboard are not any game match with homeTeam and awayTeam 
        Assert.IsTrue(!sb.Any(v=>v.HomeTeam.Equals(homeTeam) && v.AwayTeam.Equals(awayTeam)));
        
        Debug.WriteLine("ScoreBoard_FinishGame_Test End...");
    }
    
    [Test]
    public void ScoreBoard_Summary_Test()
    {
        // Auxiliar gameMatch variable
        GameMatch auxGameMatch;

        Debug.WriteLine("ScoreBoard_Summary_Test Start...");

        // Populating games and updating score
        auxGameMatch=this._scoreBoard.StartGame(new Team("Mexico"), new Team("Canada"));
        this._scoreBoard.UpdateScore(auxGameMatch, 0, 5);

        auxGameMatch=this._scoreBoard.StartGame(new Team("Spain"), new Team("Brazil"));
        this._scoreBoard.UpdateScore(auxGameMatch, 10, 2);

        auxGameMatch=this._scoreBoard.StartGame(new Team("Germany"), new Team("France"));
        this._scoreBoard.UpdateScore(auxGameMatch, 2, 2);

        auxGameMatch=this._scoreBoard.StartGame(new Team("Uruguay"), new Team("Italy"));
        this._scoreBoard.UpdateScore(auxGameMatch, 6, 6);
        
        auxGameMatch=this._scoreBoard.StartGame(new Team("Argentina"), new Team("Australia"));
        this._scoreBoard.UpdateScore(auxGameMatch, 3, 1);

        
        Debug.WriteLine("ScoreBoard content:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, this._scoreBoard.GetScoreBoard()));

        // Getting scoreboard summary
        var summary = this._scoreBoard.GetSummary();

        Debug.WriteLine("ScoreBoard summary:"
                        + Environment.NewLine
                        + String.Join(Environment.NewLine, summary));

        
        bool assert = true;
        
        // Iterates over all gameMatchs for index
        for (int i = 0; i < summary.Count; i++)
        {
            // Assert verification for each element (from second to end) 
            if (assert && i > 0)
            {
                // If previous gameMatch totalScore is equal to actual GameMatch totalScore (second order desc by lastUpdateTime)
                if (summary[i - 1].TotalScore==summary[i].TotalScore)
                    // Asser is true if previous gameMatch lastUpdateTime is greater than actual GameMatch lastUpdateTime
                    assert = assert & summary[i - 1].LastUpdateTime > summary[i].LastUpdateTime;
                else  // first order desc by totalScore
                    // Asser is true if previous gameMatch totalScore is greater than actual GameMatch totalScore
                    assert = assert & summary[i-1].TotalScore > summary[i].TotalScore;
                
                if(!assert)
                    break;
            }
        }
        
        // It's ok if all items (from second to end) are in correct order possition
        Assert.IsTrue(assert);
        
        Debug.WriteLine("ScoreBoard_Summary_Test End...");
    }
}