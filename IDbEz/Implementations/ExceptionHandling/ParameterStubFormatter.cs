using System;
using IDbEz.ExceptionHandling;


namespace IDbEz.Implementations.ExceptionHandling
{
    internal class ParameterStubFormatter : IParameterStubFormatter
    {
        public String Format( IParameterStub parameterStub )
        {
            return String.Format( "{0} = {1}", parameterStub.ParameterName, parameterStub.Value.ToString() );
        }
    }
}