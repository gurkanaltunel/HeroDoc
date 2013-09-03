using System.Data;
using ServiceStack.OrmLite;
using System.Configuration;
using DocumentService.Exceptions;

namespace DocumentService.Repository
{
    public class DbHelper
    {
        private static OrmLiteConnectionFactory _dbFactory;

        public static IDbConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionString == null)
            {
                throw new ConnectionStringNotFoundException();
            }
            if (_dbFactory == null)
            {
                _dbFactory = new OrmLiteConnectionFactory(connectionString.ConnectionString, SqlServerDialect.Provider);
            }
            return _dbFactory.OpenDbConnection();
        }
    }
}
