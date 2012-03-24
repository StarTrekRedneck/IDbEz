using IDbEz.Implementations;
using NUnit.Framework;


namespace IDbEz.Specs.ParameterNamesSeparatorSourceSpecs
{
    [TestFixture]
    public class When_returning_the_parameter_names_separator
    {
        [Test]
        public void The_parameter_names_separator_source_should_return_a_comma_followed_by_a_space()
        {
            var source = new ParameterNamesSeparatorSource();
            var expected = ", ";
            var actual = source.GetSeparator();

            Assert.AreEqual( expected, actual );
        }
    }
}
