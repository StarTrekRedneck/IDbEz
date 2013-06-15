using System;
using IDbEz.Implementations;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ParameterUnnumberedNameRepositorySpecs
{
    [TestFixture]
    public class When_getting_an_unnumbered_parameter_name_given_a_parameter_name_prefix_repository_and_a_parameter_root_name_repository
    {
        [Test]
        [TestCase( "@", "param" )]
        [TestCase( "#", "name" )]
        [TestCase( "aos8sd", "0a8sv0asjd0wej0wvjLja" )]
        public void The_unnumbered_parameter_name_repository_should_return_a_string_combination_of_the_prefix_and_then_the_root_name( String prefix, String rootName )
        {
            var expectedName = prefix + rootName;

            var prefixRepository = MockRepository.GenerateMock<IParameterNamePrefixRepository>();
            prefixRepository.Stub( r => r.GetParameterNamePrefix() ).Return( prefix );

            var rootNameRepository = MockRepository.GenerateMock<IParameterRootNameRepository>();
            rootNameRepository.Stub( r => r.GetParameterRootName() ).Return( rootName );

            var paramterUnnumberedNameRepository = new ParameterUnnumberedNameRepository( prefixRepository, rootNameRepository );
            var result = paramterUnnumberedNameRepository.GetUnnumberedName();

            Assert.AreEqual( expectedName, result );
        }
    }
}
