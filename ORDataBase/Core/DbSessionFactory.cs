namespace ORDataBase.Core;

/// <summary>
/// DataBase Session Factory class (In-Memory simulation)
/// </summary>
public class DbSessionFactory<T> where T:DbModel 
{
    // Instance of dbModel in memory
    private readonly T _dbModel;

    public DbSessionFactory(T dbModel)
    {
        this._dbModel = dbModel ?? throw new ArgumentNullException(nameof(dbModel));
    }
    
    /// <summary>
    /// Create new DbSession of typed DbModel specified
    /// </summary>
    /// <typeparam name="T">DbModel type</typeparam>
    /// <returns>Return new DbSession of typed DbModel specified</returns>
    public DbSession<T> NewSession()
    {
        // Return new dbsession of dbModel
        return new DbSession<T>(this._dbModel);
    }
}