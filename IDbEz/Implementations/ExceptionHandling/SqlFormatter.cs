using System;
using IDbEz.ExceptionHandling;


namespace IDbEz.Implementations.ExceptionHandling
{
    internal class SqlFormatter : ISqlFormatter
    {
        public String Format( string sql )
        {
            return sql;
        }
    }
}