namespace LC.Backend.Common.DB
{
    public class MongoConnection : IDbConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
