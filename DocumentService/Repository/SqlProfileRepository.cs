using System.Collections.Generic;
using DocumentService.Abstractions;
using DocumentService.Interceptors;
using DocumentService.Models;
using ServiceStack.OrmLite;

namespace DocumentService.Repository
{
    public class SqlProfileRepository : IProfileRepository, IRepository
    {
        [RequestCache]
        public virtual IList<Profile> GetProfileForUser(int id)
        {
            using (var connection = DbHelper.CreateConnection())
            {
                return connection.Select<Profile>();
            }
        }
    }
}
