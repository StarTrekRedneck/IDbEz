using System;
using System.Data;
using IDbEz.Implementations;
using NUnit.Framework;


namespace IDbEz.Specs.ParameterStubFactorySpecs
{
    [TestFixture]
    public class When_creating_a_parameter_stub_given_a_parameter_name_and_value
    {
        private String _paramName = "name";
        private Int32 _value = 17;
        private ParameterStubFactory _factory;
        private IParameterStub _result;


        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _factory = new ParameterStubFactory();
            _result = _factory.CreateParameterStub( _paramName, _value );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_name_equal_to_that_given()
        {
            Assert.AreEqual( _paramName, _result.ParameterName );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_value_equal_to_that_given()
        {
            Assert.AreEqual( _value, _result.Value );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_dbType_value_equal_to_its_default_value()
        {
            Assert.AreEqual( new DbType(), _result.DbType );
        }
    }



    [TestFixture]
    public class When_creating_a_parameter_stub_given_a_parameter_name_and_value_and_dbType
    {
        private String _paramName = "name";
        private Int32 _value = 17;
        private DbType _dbType = DbType.Int32;
        private ParameterStubFactory _factory;
        private IParameterStub _result;


        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _factory = new ParameterStubFactory();
            _result = _factory.CreateParameterStub( _paramName, _value, _dbType );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_name_equal_to_that_given()
        {
            Assert.AreEqual( _paramName, _result.ParameterName );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_value_equal_to_that_given()
        {
            Assert.AreEqual( _value, _result.Value );
        }


        [Test]
        public void The_factory_should_return_a_paramter_stub_with_the_dbType_value_equal_to_that_given()
        {
            Assert.AreEqual( _dbType, _result.DbType );
        }
    }
}
