using System;
using System.Text;


namespace IDbEz.Implementations
{
    internal class SqlBuilder : ISqlBuilder
    {
        private StringBuilder _sql = new StringBuilder();


        public ISqlBuilder Add( String sql )
        {
            return Add( sql, null );
        }


        public ISqlBuilder Add( String sql, params String[] parameterNames )
        {
            if ( parameterNames == null )
                _sql.Append( sql );
            else
                _sql.AppendFormat( sql, parameterNames );
            return this;
        }


        public String GetSql()
        {
            return _sql.ToString();
        }
    }
}
