using System;
using System.Collections.Generic;
using System.Data;


namespace IDbEz
{
    public interface IQueryBuilder
    {
        IQueryBuilder Add( String sql );
        IQueryBuilder Add( String sql, params Object[] parameterValues );
        IQueryBuilder Add( String sql, Object parameterValue, DbType dbType );
        String GetSql();
        IEnumerable<IParameterStub> GetParameters();
    }
}
