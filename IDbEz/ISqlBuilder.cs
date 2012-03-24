using System;
using System.Collections.Generic;


namespace IDbEz
{
    public interface ISqlBuilder
    {
        ISqlBuilder Add( String sql );
        ISqlBuilder Add( String sql, params String[] parameterName );
        String GetSql();
    }
}