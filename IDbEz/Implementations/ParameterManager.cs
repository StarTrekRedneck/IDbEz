using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace IDbEz.Implementations
{
    internal class ParameterManager : IParameterManager
    {
        private IParameterNameFactory _parameterNameGenerator;
        private IParameterNamesJoiner _parameterNamesJoiner;
        private IParameterStubFactory _parameterStubFactory;
        private Dictionary<String, IParameterStub> _parameterDictionary = new Dictionary<String, IParameterStub>();


        public ParameterManager( IParameterNameFactory parameterNameGenerator, IParameterNamesJoiner parameterNamesJoiner, IParameterStubFactory parameterStubFactory )
        {
            _parameterNameGenerator = parameterNameGenerator;
            _parameterNamesJoiner = parameterNamesJoiner;
            _parameterStubFactory = parameterStubFactory;
        }


        public String AddParameter( Object parameterValue )
        {
            if ( parameterValue is IEnumerable && !( parameterValue is String ) )
                return AddParameter( ( (IEnumerable)parameterValue ).Cast<Object>() );
            else
                return AddParameterToDictionaryAndReturnAssignedParameterName( parameterValue );
        }


        public String AddParameter( IEnumerable<Object> parameterValue )
        {
            return _parameterNamesJoiner.JoinNames( AddParameters( parameterValue.ToArray() ) );
        }


        public String AddParameter( Object parameterValue, DbType dbType )
        {
            String paramName = _parameterNameGenerator.GenerateParameterName( _parameterDictionary.Count );
            _parameterDictionary.Add( paramName, _parameterStubFactory.CreateParameterStub( paramName, parameterValue, dbType ) );
            return paramName;
        }


        public IEnumerable<String> AddParameters( params Object[] parameterValues )
        {
            var parameterNames = new List<String>();
            foreach ( var parameter in parameterValues )
            {
                parameterNames.Add( AddParameter( parameter ) );
            }

            return parameterNames;
        }


        public IEnumerable<IParameterStub> GetParameterStubs()
        {
            return _parameterDictionary.Values;
        }


        private String AddParameterToDictionaryAndReturnAssignedParameterName( Object parameterValue )
        {
            String paramName = _parameterNameGenerator.GenerateParameterName( _parameterDictionary.Count );
            _parameterDictionary.Add( paramName, _parameterStubFactory.CreateParameterStub( paramName, parameterValue ) );
            return paramName;
        }
    }
}