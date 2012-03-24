using System;


namespace IDbEz.Implementations
{
    internal class ParameterStubFactory : IParameterStubFactory
    {
        public IParameterStub CreateParameterStub( String parameterName, Object parameterValue )
        {
            return new ParameterStub { ParameterName = parameterName, Value = parameterValue };
        }


        public IParameterStub CreateParameterStub( String parameterName, Object parameterValue, System.Data.DbType dbType )
        {
            return new ParameterStub { ParameterName = parameterName, Value = parameterValue, DbType = dbType };
        }
    }
}
