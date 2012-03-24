using System;


namespace IDbEz
{
    public interface IParameterNameFactory
    {
        String GenerateParameterName( Int32 paramNumber );
    }
}
