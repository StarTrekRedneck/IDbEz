using System;
using System.Collections.Generic;


namespace IDbEz.ExceptionHandling
{
    public interface IParameterStubCollectionFormatter
    {
        String Format( IEnumerable<IParameterStub> parameterStubs );
    }
}