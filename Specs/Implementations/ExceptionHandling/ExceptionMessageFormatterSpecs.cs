using System;
using System.Collections.Generic;
using IDbEz.ExceptionHandling;
using IDbEz.Implementations.ExceptionHandling;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ExceptionHandling.ExceptionMessageFormatterSpecs
{
    [TestFixture]
    public class Given_the_exception_the_sql_and_the_parameter_stubs
    {
        [Test]
        public void The_formatter_should_return_a_concatenation_of_the_original_message_the_sql_formatter_and_the_parameter_stubs_formatter_results()
        {
            var sqlFormatter = MockRepository.GenerateMock<ISqlFormatter>();
            var parameterStubCollectionFormatter = MockRepository.GenerateMock<IParameterStubCollectionFormatter>();
            var exceptionFormatter = new ExceptionMessageFormatter( sqlFormatter, parameterStubCollectionFormatter );

            var exception = new Exception( "given message" );
            var sql = "given sql";
            var paramStubs = new List<IParameterStub>();
            var formattedSql = "formatted sql";
            sqlFormatter.Stub( f => f.Format( sql ) ).Return( formattedSql );

            var formattedParams = "formatted params";
            parameterStubCollectionFormatter.Stub( f => f.Format( paramStubs ) ).Return( formattedParams );

            var expectedMessage = exception.Message + Environment.NewLine +
                                  formattedSql + Environment.NewLine + 
                                  formattedParams;
            var resultMessage = exceptionFormatter.Format( exception, sql, paramStubs );

            Assert.AreEqual( expectedMessage, resultMessage );
        }
    }
}