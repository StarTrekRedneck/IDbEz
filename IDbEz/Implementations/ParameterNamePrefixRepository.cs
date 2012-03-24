using System;


namespace IDbEz.Implementations
{
    internal class ParameterNamePrefixRepository : IParameterNamePrefixRepository
    {
        public const String ParameterNamePrefix = "@";


        public String GetParameterNamePrefix()
        {
            return ParameterNamePrefix;
        }
    }
}
