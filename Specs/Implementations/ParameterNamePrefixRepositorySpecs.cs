using System;
using IDbEz.Implementations;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ParameterNamePrefixRepositorySpecs
{
    [TestFixture]
    public class When_getting_the_parameter_name_prefix_from_the_repository
    {
        [Test]
        public void The_repository_should_return_the_at_symbol()
        {
            var prefixRepository = new ParameterNamePrefixRepository();
            var result = prefixRepository.GetParameterNamePrefix();
            Assert.AreEqual( "@", result );
        }
    }
}
