using System;
using IDbEz.Implementations.ExceptionHandling;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ExceptionHandling.ParameterStubFormatterSpecs
{
    [TestFixture]
    public class Given_a_parameter_stub
    {
        [Test]
        [TestCase( "param0", "value0", "param0 = value0" )]
        [TestCase( "param1", 123, "param1 = 123" )]
        [TestCase( "param2", 789.012, "param2 = 789.012" )]
        public void The_formatter_should_return_a_string_of_the_parameter_name_and_value( String paramName, Object paramValue, String expectedFormat )
        {
            var formatter = new ParameterStubFormatter();
            var paramStub = MockRepository.GenerateMock<IParameterStub>();
            paramStub.Stub( param => param.ParameterName ).Return( paramName );
            paramStub.Stub( param => param.Value ).Return( paramValue );

            var actualFormat = formatter.Format( paramStub );

            Assert.AreEqual( expectedFormat, actualFormat );
        }
    }
}