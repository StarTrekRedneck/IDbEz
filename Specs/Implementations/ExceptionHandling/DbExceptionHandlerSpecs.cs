using System;
using System.Collections.Generic;
using System.Data.Common;
using IDbEz.ExceptionHandling;
using IDbEz.Implementations;
using IDbEz.Implementations.ExceptionHandling;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ExceptionHandling.DbExceptionHandlerSpecs
{
    [TestFixture]
    public class When_handling_a_db_exception
    {
        private DbExceptionHandler _exceptionHandler;

        private IExceptionMessageFormatter _exceptionMessageFormatter = MockRepository.GenerateMock<IExceptionMessageFormatter>();

        private DbException _givenException = MockRepository.GenerateMock<DbException>();
        private String _sql = "given sql";
        private String _message = "formatted message";
        private IEnumerable<IParameterStub> _parameterStubs = new List<IParameterStub> { new ParameterStub { ParameterName = "param1" }, 
                                                                                         new ParameterStub { ParameterName = "param2" } };
        private Exception _thrownException;


        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _exceptionHandler = new DbExceptionHandler( _exceptionMessageFormatter );
            _exceptionMessageFormatter.Stub( f => f.Format( _givenException, _sql, _parameterStubs ) ).Return( _message );

            try
            {
                _exceptionHandler.Handle( _givenException, _sql, _parameterStubs );
            }
            catch ( Exception ex )
            {
                _thrownException = ex;
            }
        }


        [Test]
        public void The_handler_should_throw_a_new_db_ez_exception()
        {
            Assert.IsInstanceOf<DbEzException>( _thrownException );
        }
        

        [Test]
        public void The_new_exception_should_contain_the_given_exception()
        {
            Assert.AreSame( _givenException, _thrownException.InnerException );
        }


        [Test]
        public void The_new_exception_message_should_be_that_from_the_exception_message_formatter()
        {
            Assert.AreEqual( _message, _thrownException.Message );
        }


        [Test]
        public void The_new_exception_data_should_contain_the_sql()
        {
            Assert.AreEqual( _sql, _thrownException.Data["SQL"] );
        }


        [Test]
        public void The_new_exception_data_should_contain_the_parameter_stubs()
        {
            Assert.AreEqual( _parameterStubs, _thrownException.Data["PARAMETERS"] );
        }
    }
}