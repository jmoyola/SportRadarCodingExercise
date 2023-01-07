namespace ORDataBase.Core;

/// <summary>
/// DataBase session class
/// </summary>
public class DbSession<T>
    :IDisposable
    where T:DbModel
{
    /// <summary>
    /// Disposed member variable
    /// </summary>
    private bool _disposed = false;
    
    /// <summary>
    /// model member variable
    /// </summary>
    private T _model;
    
    /// <summary>
    /// DbSession constructor
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public DbSession(T dbModel)
    {
        this._model=dbModel??throw new ArgumentNullException(nameof(dbModel));
    }
    
    /// <summary>
    /// DataBase Model
    /// </summary>
    public T Model=>_model; 

    /// <summary>
    /// Commit session changes
    /// </summary>
    /// <exception cref="DbSessionException"></exception>
    public void Commit()
    {
        // Logical exceptions
        if (this._disposed)
            throw new DbSessionException("DbSession is disposed");
        
        // Code for commit
    }

    /// <summary>
    /// Rollback session changes
    /// </summary>
    /// <exception cref="DbSessionException"></exception>
    public void Rollback()
    {
        // Logical exceptions
        if (this._disposed)
            throw new DbSessionException("DbSession is disposed");
        
        // Code for rollback
    }

    /// <summary>
    /// Dispose implementation for BaseModel
    /// </summary>
    /// <param name="disposing">True for dispose managed objects</param>
    protected virtual void Dispose(bool disposing)
    {
        if (this._disposed)
            return;
        
        if(disposing){
            // Code to dispose...
        }
        
        this._disposed = true;
    }
    
    /// <summary>
    /// Dispose method
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
    }
}