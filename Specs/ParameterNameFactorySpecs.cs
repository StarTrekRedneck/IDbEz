using System;
using NUnit.Framework;
using Rhino.Mocks;
using IDbEz.Implementations;


namespace IDbEz.Specs.ParameterNameFactorySpecs
{
    [TestFixture]
    public class When_generating_a_parameter_name_given_a_non_negative_integer
    {
        [Test]
        [TestCase( "paramName", 0 )]
        [TestCase( "myName", 2 )]
        [TestCase( "20f9j2e", 1203 )]
        public void The_generator_should_return_a_string_from_the_unnumbered_parameter_name_repository_with_the_integer_appended_at_the_end( String unnumberedName, Int32 paramNumber )
        {
            var expectedGeneratedName = unnumberedName + paramNumber.ToString();
            var unnumberedNameRepository = MockRepository.GenerateMock<IParameterUnnumberedNameRepository>();
            unnumberedNameRepository.Stub( r => r.GetUnnumberedName() ).Return( unnumberedName );

            var nameGenerator = new ParameterNameFactory( unnumberedNameRepository );
            var result = nameGenerator.GenerateParameterName( paramNumber );

            Assert.AreEqual( expectedGeneratedName, result );
        }
    }
}
