using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace IDbEz.ExceptionHandling
{
    public interface IDbExceptionHandler
    {
        void Handle( DbException dbException, String sql, IEnumerable<IParameterStub> parameterStubs );
    }
}