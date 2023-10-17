using Dapper;
using System.Data;

namespace DataDog.Database.MSSQL
{

    public class MSSQLProvider : BaseSQLProvider
    {
        public MSSQLProvider(IDbConnection context) : base(context)
        {
        }

    }

}
