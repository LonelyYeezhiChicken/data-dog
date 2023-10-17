using Dapper;
using System.Data;

namespace DataDog.Database.MYSQL
{
    public class MYSQLProvider : BaseSQLProvider
    {
        public MYSQLProvider(IDbConnection context) : base(context)
        {
        }
    }
}
