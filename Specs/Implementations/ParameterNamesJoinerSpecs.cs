using System;
using IDbEz.Implementations;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ParameterNamesJoinerSpecs
{
    [TestFixture]
    public class When_joining_parameter_names_into_one_string
    {
        [Test]
        [TestCase( "x" )]
        [TestCase( "y" )]
        public void The_joiner_should_return_all_parameter_names_in_one_string_separated_by_the_string_from_the_separator_source( String separator )
        {
            var separatorSource = MockRepository.GenerateMock<IParameterNamesSeparatorSource>();
            separatorSource.Stub( s => s.GetSeparator() ).Return( separator );

            var joiner = new ParameterNamesJoiner( separatorSource );
            var parameterNames = new[] { "name1", "name2", "name3" };
            var expected = String.Join( separator, parameterNames );
            var actual = joiner.JoinNames( parameterNames );

            Assert.AreEqual( expected, actual );
        }
    }
}
