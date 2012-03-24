using System;


namespace IDbEz.Implementations
{
    internal class ParameterUnnumberedNameRepository : IParameterUnnumberedNameRepository
    {
        private IParameterNamePrefixRepository _prefixRepository;
        private IParameterRootNameRepository _rootNameRepository;

        public ParameterUnnumberedNameRepository( IParameterNamePrefixRepository parameterNamePrefixRepository, IParameterRootNameRepository parameterRootNameRepository )
        {
            _prefixRepository = parameterNamePrefixRepository;
            _rootNameRepository = parameterRootNameRepository;
        }


        public String GetUnnumberedName()
        {
            return _prefixRepository.GetParameterNamePrefix() + _rootNameRepository.GetParameterRootName();
        }
    }
}
