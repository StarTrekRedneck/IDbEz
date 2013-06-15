using System;
using IDbEz.Implementations;
using NUnit.Framework;


namespace IDbEz.Specs.Implementations.ParameterRootNameRepositorySpecs
{
    [TestFixture]
    public class When_getting_the_paramter_root_name_from_the_repository
    {
        [Test]
        public void The_repository_should_return_the_string_param()
        {
            var repository = new ParameterRootNameRepository();
            var result = repository.GetParameterRootName();
            Assert.AreEqual( "param", result );
        }
    }
}
