using System.Diagnostics;
using ORDataBase.Core;

namespace ScoreBoardModel.Entities;

public class Team:Entity
{
    /// <summary>
    /// Member variable for Name
    /// </summary>
    private String _name;

    public Team(String name)
    {
        Debug.Assert(!String.IsNullOrEmpty(name), "No valid value", "Value '{0}' for parameter '{1}' must be not null or empty", name, nameof(name));
        this._name = name;
    }
    
    /// <summary>
    /// Name property for Team (must be not null or empty) 
    /// </summary>
    public String Name
    {
        get => this._name;
        set
        {
            Debug.Assert(!String.IsNullOrEmpty(value), "No valid value", "Value '{0}' for parameter '{1}' must be not null or empty", value, nameof(value));
            this._name = value;
        }
    }

    /// <summary>
    /// ToString implementation for Team
    /// </summary>
    /// <returns>String implementation for Team</returns>
    public override string ToString()
    {
        return this._name;
    }
}