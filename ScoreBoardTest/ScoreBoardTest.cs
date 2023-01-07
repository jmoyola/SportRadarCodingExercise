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
}