namespace ORDataBase.Core;

/// <summary>
/// Entity base class
/// </summary>
public abstract class Entity:IEntity
{
    /// <summary>
    /// Id variable member for Entity
    /// </summary>
    private readonly Guid _id;

    /// <summary>
    /// Constructor for Entity
    /// </summary>
    protected Entity()
    {
        // Create new GUID
        this._id = Guid.NewGuid();
    }
    
    /// <summary>
    /// Id property for Entity
    /// </summary>
    public Guid Id => this._id;
    
    /// <summary>
    /// Equals implementation for entity
    /// </summary>
    /// <param name="obj">Object to compare</param>
    /// <returns>True if are equal</returns>
    public override bool Equals(object? obj)
    {
                                                            // One object is equal to me if:
        return obj != null                                  // - Is Not null
               && obj is IEntity                            // - Is Instance of IEntity
               && this.GetType().Equals(obj.GetType())      // - Is Equal type of me
               && ((Entity) obj).Id.Equals(this._id);       // - Its id is equal to me
    }

    /// <summary>
    /// Hash code for entity
    /// </summary>
    /// <returns>Hash code of entity</returns>
    public override int GetHashCode()
    {
        // Hashcode is dependent of Full Class type and its Id 
        return this.GetType().GetHashCode()
               | this.Id.GetHashCode();
    }

    /// <summary>
    /// ToString implementation for entity
    /// </summary>
    /// <returns>String representation of entity</returns>
    public override string ToString()
    {
        return this.GetType().Name + " (" + this.Id + ")";
    }
}