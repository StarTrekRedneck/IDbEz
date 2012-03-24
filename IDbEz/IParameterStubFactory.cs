using System;
using System.Data;


namespace IDbEz
{
    public interface IParameterStubFactory
    {
        IParameterStub CreateParameterStub( String parameterName, Object parameterValue );
        IParameterStub CreateParameterStub( String parameterName, Object parameterValue, DbType dbType );
    }
}
