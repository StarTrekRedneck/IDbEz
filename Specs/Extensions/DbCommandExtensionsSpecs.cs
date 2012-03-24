using System;
using System.Data;
using IDbEz.Extensions;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Extensions.DbCommandExtensionsSpecs
{
    namespace Given_a_DbCommand
    {
        [TestFixture]
        public class When_creating_a_parameter_given_a_parameter_name_and_value
        {
            IDbCommand _dbCommand;
            IDbDataParameter _dataParameter;
            String _parameterName;
            Object _parameterValue;
            IDbDataParameter _resultParameter;


            [TestFixtureSetUp]
            public void SetupTestFixture()
            {
                _dbCommand = MockRepository.GenerateMock<IDbCommand>();
                _dataParameter = MockRepository.GenerateMock<IDbDataParameter>();
                _dbCommand.Stub( c => c.CreateParameter() ).Return( _dataParameter );
                _parameterName = "Given name";
                _parameterValue = new { Property = "given value" };
                _resultParameter = DbCommandExtensions.CreateParameter( _dbCommand, _parameterName, _parameterValue );
            }


            [Test]
            public void The_extension_should_return_a_parameter_created_from_the_db_command()
            {
                Assert.AreSame( _dataParameter, _resultParameter );
            }


            [Test]
            public void The_extension_should_add_the_parameter_name_to_the_returned_parameter()
            {
                _resultParameter.AssertWasCalled( p => p.ParameterName = _parameterName );
            }


            [Test]
            public void The_extension_should_add_the_parameter_value_to_the_returned_parameter()
            {
                _resultParameter.AssertWasCalled( p => p.Value = _parameterValue );
            }
        }


        [TestFixture]
        public class When_creating_a_parameter_given_a_parameter_name_and_value_and_DbType
        {
            IDbCommand _dbCommand;
            IDbDataParameter _dataParameter;
            String _parameterName;
            Object _parameterValue;
            DbType _dbType;
            IDbDataParameter _resultParameter;


            [TestFixtureSetUp]
            public void SetupTestFixture()
            {
                _dbCommand = MockRepository.GenerateMock<IDbCommand>();
                _dataParameter = MockRepository.GenerateMock<IDbDataParameter>();
                _dbCommand.Stub( c => c.CreateParameter() ).Return( _dataParameter );
                _parameterName = "Given name";
                _parameterValue = new { Property = "given value" };
                _dbType = DbType.Object;
                _resultParameter = DbCommandExtensions.CreateParameter( _dbCommand, _parameterName, _parameterValue, _dbType );
            }


            [Test]
            public void The_extension_should_return_a_parameter_created_from_the_db_command()
            {
                Assert.AreSame( _dataParameter, _resultParameter );
            }


            [Test]
            public void The_extension_should_add_the_parameter_name_to_the_returned_parameter()
            {
                _resultParameter.AssertWasCalled( p => p.ParameterName = _parameterName );
            }


            [Test]
            public void The_extension_should_add_the_parameter_value_to_the_returned_parameter()
            {
                _resultParameter.AssertWasCalled( p => p.Value = _parameterValue );
            }


            [Test]
            public void The_extension_should_set_the_DbType_on_the_returned_parameter()
            {
                _resultParameter.AssertWasCalled( p => p.DbType = _dbType );
            }
        }


        [TestFixture]
        public class When_adding_a_parameter_to_the_db_command_given_a_parameter_name_and_value
        {
            IDbCommand _dbCommand;
            IDbDataParameter _dataParameter;
            IDataParameterCollection _parameters;
            String _parameterName;
            Object _parameterValue;
            Int32 _parameterNumber;
            Int32 _result;


            [TestFixtureSetUp]
            public void SetupTestFixture()
            {
                _parameterName = "Given name";
                _parameterValue = new { Property = "given value" };
                _parameterNumber = 5;

                _dbCommand = MockRepository.GenerateMock<IDbCommand>();
                _dataParameter = MockRepository.GenerateMock<IDbDataParameter>();
                _parameters = MockRepository.GenerateMock<IDataParameterCollection>();

                _parameters.Stub( p => p.Add( _dataParameter ) ).Return( _parameterNumber );
                _dbCommand.Stub( c => c.CreateParameter() ).Return( _dataParameter );
                _dbCommand.Stub( c => c.Parameters ).Return( _parameters );
                
                _result = DbCommandExtensions.AddParameter( _dbCommand, _parameterName, _parameterValue );
            }


            [Test]
            public void The_extension_should_return_the_parameter_number()
            {
                Assert.AreEqual( _parameterNumber, _result );
            }
        }
    }
}