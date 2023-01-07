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
}