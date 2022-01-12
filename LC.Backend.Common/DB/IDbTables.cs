namespace LC.Backend.Common.DB
{
    public interface IDbTables
    {
        string[] Tables { get; set; }
    }

    public class DbTables : IDbTables
    {
        public string[] Tables { get; set; }
    }
}
