using System;


namespace IDbEz.Implementations
{
    public class ParameterNamesSeparatorSource : IParameterNamesSeparatorSource
    {
        public String GetSeparator()
        {
            return ", ";
        }
    }
}
