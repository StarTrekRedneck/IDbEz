using System;


namespace IDbEz.ExceptionHandling
{
    public interface IParameterStubFormatter
    {
        String Format( IParameterStub parameterStub );
    }
}