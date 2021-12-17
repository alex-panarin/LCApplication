namespace LC.Backend.Common.DB
{
    public interface IDbConnection
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
