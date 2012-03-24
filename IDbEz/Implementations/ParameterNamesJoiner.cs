using System;
using System.Collections.Generic;
using System.Linq;


namespace IDbEz.Implementations
{
    public class ParameterNamesJoiner : IParameterNamesJoiner
    {
        private IParameterNamesSeparatorSource _separatorSource;


        public ParameterNamesJoiner( IParameterNamesSeparatorSource separatorSource )
        {
            _separatorSource = separatorSource;
        }


        public String JoinNames( IEnumerable<String> parameterNames )
        {
            return String.Join( _separatorSource.GetSeparator(), parameterNames.ToArray() );
        }
    }
}
