using System;
using System.Collections.Generic;
using System.Data.Common;
using IDbEz.ExceptionHandling;


namespace IDbEz.Implementations.ExceptionHandling
{
    internal class DbExceptionHandler : IDbExceptionHandler
    {
        private IExceptionMessageFormatter _exceptionMessageFormatter;


        public DbExceptionHandler( IExceptionMessageFormatter exceptionMessageFormatter )
        {
            _exceptionMessageFormatter = exceptionMessageFormatter;
        }


        public void Handle( DbException dbException, String sql, IEnumerable<IParameterStub> parameterStubs )
        {
            var improvedExceptionMessage = _exceptionMessageFormatter.Format( dbException, sql, parameterStubs );
            var DbEzException = new DbEzException( improvedExceptionMessage, dbException );
            DbEzException.Data.Add( "SQL", sql );
            DbEzException.Data.Add( "PARAMETERS", parameterStubs );
            throw DbEzException;
        }
    }
}