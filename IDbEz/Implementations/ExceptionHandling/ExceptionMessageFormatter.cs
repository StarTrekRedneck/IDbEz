using System;
using System.Collections.Generic;
using IDbEz.ExceptionHandling;


namespace IDbEz.Implementations.ExceptionHandling
{
    internal class ExceptionMessageFormatter : IExceptionMessageFormatter
    {
        private ISqlFormatter _sqlFormatter;
        private IParameterStubCollectionFormatter _parameterStubCollectionFormatter;


        public ExceptionMessageFormatter( ISqlFormatter sqlFormatter, IParameterStubCollectionFormatter parameterStubCollectionFormatter )
        {
            _sqlFormatter = sqlFormatter;
            _parameterStubCollectionFormatter = parameterStubCollectionFormatter;
        }


        public String Format( Exception exception, String sql, IEnumerable<IParameterStub> parameterStubs )
        {
            return exception.Message + Environment.NewLine +
                   _sqlFormatter.Format( sql ) + Environment.NewLine + 
                   _parameterStubCollectionFormatter.Format( parameterStubs );
        }
    }
}