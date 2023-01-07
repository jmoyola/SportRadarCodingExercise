using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ORDataBase.Core;

namespace ScoreBoardModel.Entities;

/// <summary>
/// Game Match class
/// </summary>
public class GameMatch:Entity
{
    // Home team and its score member variables
    private Team _homeTeam;
    private Int32 _homeScore;
    
    // Away team and its score member variables
    private Team _awayTeam;
    private Int32 _awayScore;
    
    /// <summary>
    /// Times members variables
    /// </summary>
    private DateTimeOffset? _startTime;
    private DateTimeOffset? _finishTime;
    private DateTimeOffset? _lastUpdateTime;

    /// <summary>
    /// Constructor for Match
    /// </summary>
    /// <param name="homeTeam">Local team</param>
    /// <param name="awayTeam">Away team</param>
    public GameMatch(Team homeTeam, Team awayTeam)
    {
        // Assignation and Asserts for constructor parameters
        this._homeTeam=homeTeam ?? throw new ArgumentNullException(nameof(homeTeam));
        this._awayTeam=awayTeam ?? throw new ArgumentNullException(nameof(awayTeam));

        // Initial value for scores is zero
        this._homeScore = 0;
        this._awayScore = 0;
    }

    /// <summary>
    /// Home team property
    /// </summary>
    public Team HomeTeam=> this._homeTeam;

    /// <summary>
    /// Home score property
    /// </summary>
    public Int32 HomeScore
    {
        get =>_homeScore;
        set
        {
            // Assert valid game match status for update
            if (!this.IsStarted || this.IsFinish)
                throw new Exception("Game Match must be started and not finished");

            // Assignation and Assert for value
            _homeScore = value < 0 ? throw new ArgumentException("Only greater or equal than 0 are valid value", nameof(value)) : value;
            
            // Updating the last update time 
            this._lastUpdateTime=DateTimeOffset.Now;
        }
    }
    
    /// <summary>
    /// Away team property
    /// </summary>
    public Team AwayTeam=>this._awayTeam;

    /// <summary>
    /// Away score property
    /// </summary>
    public Int32 AwayScore
    {
        get => _awayScore;
        set
        {
            // Assert valid game match status for update
            if (!this.IsStarted || this.IsFinish)
                throw new Exception("Game Match must be started and not finished");
            
            // Assert for value
            _awayScore = value < 0 ? throw new ArgumentException("Only greater or equal than 0 are valid value", nameof(value)) : value;
            
            // Updating the last update time 
            this._lastUpdateTime=DateTimeOffset.Now;
        }
    }

    /// <summary>
    /// Total score property
    /// </summary>
    public Int32 TotalScore
    {
        get => this._homeScore + this._awayScore;
    }

    /// <summary>
    /// Start Time property
    /// </summary>
    public DateTimeOffset? StartTime=>_startTime;

    /// <summary>
    /// Finish Time property
    /// </summary>
    public DateTimeOffset? EndTime=> _finishTime;

    /// <summary>
    /// Last Update Time property
    /// </summary>
    public DateTimeOffset? LastUpdateTime => _lastUpdateTime;

    /// <summary>
    /// Start a Game Match
    /// </summary>
    public void Start()
    {
        // Assert
        if (this.IsStarted)
            throw new Exception("Game Match is already started");
        
        this._startTime=DateTimeOffset.Now; // Setting start time
        this._lastUpdateTime=DateTimeOffset.Now; // Updating last update time
    }
    
    /// <summary>
    /// Finish a Game Match
    /// </summary>
    public void Finish()
    {
        // Assert
        if (this.IsFinish)
            throw new Exception("Game Match is already finished");
        
        this._finishTime=DateTimeOffset.Now; // Setting finish time
    }

    /// <summary>
    /// Return if a game match is started
    /// </summary>
    public bool IsStarted => this._startTime.HasValue;
    
    /// <summary>
    /// Return if a game match is in finish
    /// </summary>
    public bool IsFinish => this._finishTime.HasValue;
    
    /// <summary>
    /// ToString implementation for match
    /// </summary>
    /// <returns>String representation of match</returns>
    public override string ToString()
    {
        return base.ToString()
               + " Time (" + (_startTime.HasValue?_startTime.ToString():"") + " - " + (_finishTime.HasValue?_finishTime.ToString():"") + ")" 
               + " " + _homeTeam.ToString() + " " + this._homeScore
               + " - " + _awayTeam.ToString() + " " + this._awayScore;
    }
}