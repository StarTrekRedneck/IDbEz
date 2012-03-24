using System;


namespace IDbEz.Implementations
{
    internal class ParameterNameFactory : IParameterNameFactory
    {
        private IParameterUnnumberedNameRepository _unnumberedParameterNameRepository;


        public ParameterNameFactory( IParameterUnnumberedNameRepository unnumberedParameterNameRepository )
        {
            _unnumberedParameterNameRepository = unnumberedParameterNameRepository;
        }


        public String GenerateParameterName( Int32 paramNumber )
        {
            return _unnumberedParameterNameRepository.GetUnnumberedName() + paramNumber.ToString();
        }
    }
}
