using System;


namespace IDbEz.Implementations
{
    internal class ParameterRootNameRepository : IParameterRootNameRepository
    {
        public const String ParameterRootName = "param";


        public String GetParameterRootName()
        {
            return ParameterRootName;
        }
    }
}
