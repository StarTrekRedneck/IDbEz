using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace IDbEz.Implementations
{
    internal class QueryBuilder : IQueryBuilder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;


        public QueryBuilder( ISqlBuilder sqlBuilder, IParameterManager parameterManager )
        {
            _sqlBuilder = sqlBuilder;
            _paramMgr = parameterManager;
        }


        public IQueryBuilder Add( String sql )
        {
            _sqlBuilder.Add( sql );
            return this;
        }


        public IQueryBuilder Add( String sql, Object parameterValue, DbType dbType )
        {
            var paramName = _paramMgr.AddParameter( parameterValue, dbType );
            _sqlBuilder.Add( sql, paramName );
            return this;
        }


        public IQueryBuilder Add( String sql, params Object[] parameterValues )
        {
            var paramNames = _paramMgr.AddParameters( parameterValues );
            _sqlBuilder.Add( sql, paramNames.ToArray() );
            return this;
        }


        public String GetSql()
        {
            return _sqlBuilder.GetSql();
        }


        public IEnumerable<IParameterStub> GetParameters()
        {
            return _paramMgr.GetParameterStubs();
        }
    }
}