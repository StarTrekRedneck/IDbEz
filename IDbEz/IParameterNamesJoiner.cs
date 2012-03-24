using System;
using System.Collections.Generic;
using System.Text;


namespace IDbEz
{
    public interface IParameterNamesJoiner
    {
        String JoinNames( IEnumerable<String> parameterNames );
    }
}
