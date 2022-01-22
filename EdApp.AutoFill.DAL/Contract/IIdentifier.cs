namespace EdApp.AutoFill.DAL.Contract
{
    /// <summary>
    /// Interface for the entities which have identifier of int type
    /// name 'Id'. Don't know why I use it.
    /// </summary>
    public interface IIdentifier
    {
        int Id { get; set; }
    }
}