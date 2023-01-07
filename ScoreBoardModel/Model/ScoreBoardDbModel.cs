using ORDataBase.Core;
using ScoreBoardModel.Entities;

namespace ScoreBoardModel.Model;

/// <summary>
/// ScoreBoard DataBase Session implementation simulated with in-memory lists
/// </summary>
public class ScoreBoardDbModel:DbModel
{
    /// <summary>
    /// Emulated teams Entity Object Relational elements with typed list
    /// </summary>
    private readonly IList<Team> _teams = new List<Team>();

    /// <summary>
    /// Emulated matches Entity Object Relational elements with typed list 
    /// </summary>
    private readonly IList<GameMatch> _gameMatches = new List<GameMatch>();
    
    /// <summary>
    /// Teams elements property
    /// </summary>
    public IList<Team> Teams => this._teams;

    /// <summary>
    /// Matches elements property
    /// </summary>
    public IList<GameMatch> GameMatches => this._gameMatches;
}