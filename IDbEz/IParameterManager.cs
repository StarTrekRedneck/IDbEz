using System;
using System.Collections.Generic;
using System.Data;


namespace IDbEz
{
    public interface IParameterManager
    {
        String AddParameter( Object parameterValue );
        String AddParameter( Object parameterValue, DbType dbType );
        IEnumerable<String> AddParameters( params Object[] parameterValues );
        String AddParameter( IEnumerable<Object> parameterValue );
        IEnumerable<IParameterStub> GetParameterStubs();
    }
}