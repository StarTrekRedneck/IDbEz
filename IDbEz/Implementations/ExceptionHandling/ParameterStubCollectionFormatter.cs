using System;
using System.Collections.Generic;
using System.Linq;
using IDbEz.ExceptionHandling;


namespace IDbEz.Implementations.ExceptionHandling
{
    internal class ParameterStubCollectionFormatter : IParameterStubCollectionFormatter
    {
        private IParameterStubFormatter _parameterStubFormatter;


        public ParameterStubCollectionFormatter( IParameterStubFormatter parameterStubFormatter )
        {
            _parameterStubFormatter = parameterStubFormatter;
        }


        public String Format( IEnumerable<IParameterStub> parameterStubs )
        {
            var parametersFormattedForExceptionMessage = parameterStubs.Select( _parameterStubFormatter.Format ).ToArray();
            return String.Join( Environment.NewLine, parametersFormattedForExceptionMessage );
        }
    }
}