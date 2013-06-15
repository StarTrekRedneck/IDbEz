using System;
using System.Collections.Generic;
using IDbEz.ExceptionHandling;
using IDbEz.Implementations.ExceptionHandling;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ExceptionHandling.ParameterStubCollectionFormatterSpecs
{
    [TestFixture]
    public class Given_a_collection_of_parameter_stubs
    {
        [Test]
        [TestCase( 0 )]
        [TestCase( 1 )]
        [TestCase( 2 )]
        [TestCase( 42 )]
        public void The_formatter_should_return_a_concatenation_of_the_results_of_the_parameter_stub_formatter_for_each_parameter( Int16 paramCount )
        {
            var _paramFormatter = MockRepository.GenerateMock<IParameterStubFormatter>();
            var _paramCol = new List<IParameterStub>();
            var _expectedResult = String.Empty;
            
            for ( Int32 i = 0; i <= paramCount; i++ ) _paramCol.Add( MockRepository.GenerateMock<IParameterStub>() );
            _paramCol.ForEach( param => _paramFormatter.Stub( f => f.Format( param ) ).Return( "formatted param" ) );
            _paramCol.ForEach( param => _expectedResult += _paramFormatter.Format( param ) + Environment.NewLine );
            _expectedResult = _expectedResult.Substring( 0, _expectedResult.Length - Environment.NewLine.Length );

            var _paramColFormatter = new ParameterStubCollectionFormatter( _paramFormatter );
            var _result = _paramColFormatter.Format( _paramCol );

            Assert.AreEqual( _expectedResult, _result );
        }
    }
}