using System;
using System.Collections.Generic;


namespace IDbEz.ExceptionHandling
{
    public interface IExceptionMessageFormatter
    {
        String Format( Exception exception, String sql, IEnumerable<IParameterStub> parameterStubs );
    }
}