using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IDbEz.Implementations;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.ParameterManagerSpecs
{
    [TestFixture]
    public class When_adding_a_parameter_to_a_parameter_manager
    {
        private String _inputParam = "inputParam";
        private String _paramName = "paramName";
        private String _result;
        private IParameterNameFactory _paramNameGenerator;
        private IParameterNamesJoiner _parameterNamesJoiner;
        private IParameterStubFactory _parameterStubFactory;
        private ParameterManager _paramMgr;


        [SetUp]
        public void SetupTest()
        {
            _paramNameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
            _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 0 ) ).Return( _paramName );
            _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
            _paramMgr = new ParameterManager( _paramNameGenerator, _parameterNamesJoiner, _parameterStubFactory );
            _result = _paramMgr.AddParameter( _inputParam );
        }


        [Test]
        public void The_parameter_manager_should_return_a_parameter_name_from_the_name_generator()
        {
            Assert.AreEqual( _paramName, _result );
        }
    }


    [TestFixture]
    public class When_adding_a_parameter_value_and_dbType_to_a_parameter_manager
    {
        private String _paramValue = "inputParam";
        private DbType _dbType = DbType.String;
        private String _paramName = "paramName";
        private String _result;
        private IParameterNameFactory _paramNameGenerator;
        private IParameterNamesJoiner _parameterNamesJoiner;
        private IParameterStubFactory _parameterStubFactory;
        private ParameterManager _paramMgr;


        [SetUp]
        public void SetupTest()
        {
            _paramNameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
            _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 0 ) ).Return( _paramName );
            _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
            _paramMgr = new ParameterManager( _paramNameGenerator, _parameterNamesJoiner, _parameterStubFactory );
            _result = _paramMgr.AddParameter( _paramValue, _dbType );
        }


        [Test]
        public void The_parameter_manager_should_return_a_parameter_name_from_the_name_generator()
        {
            Assert.AreEqual( _paramName, _result );
        }
    }


    [TestFixture]
    public class When_adding_multiple_parameters_to_a_parameter_manager
    {
        private String _inputParam1 = "inputParam1";
        private String _inputParam2 = "inputParam2";
        private String _inputParam3 = "inputParam3";
        private List<String> _expectedParamNames = new List<String> { "paramName1", "paramName2", "paramName3" };
        private IEnumerable<String> _results;
        private IParameterNameFactory _paramNameGenerator;
        private IParameterNamesJoiner _parameterNamesJoiner;
        private IParameterStubFactory _parameterStubFactory;
        private ParameterManager _paramMgr;


        [SetUp]
        public void SetupTest()
        {
            _paramNameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
            _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
            _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 0 ) ).Return( _expectedParamNames[0] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 1 ) ).Return( _expectedParamNames[1] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 2 ) ).Return( _expectedParamNames[2] );
            _paramMgr = new ParameterManager( _paramNameGenerator, _parameterNamesJoiner, _parameterStubFactory );
            _results = _paramMgr.AddParameters( _inputParam1, _inputParam2, _inputParam3 );
        }


        [Test]
        public void The_parameter_manager_should_return_an_enumerable_of_the_names_from_the_name_generator_in_the_order_the_parameters_were_added()
        {
            CollectionAssert.AreEqual( _expectedParamNames, _results );
        }
    }


    [TestFixture]
    public class When_adding_an_enumerable_of_parameters_to_a_parameter_manager
    {
        private List<String> _expectedParamNames = new List<String> { "paramName0", "paramName1", "paramName2" };
        private String _expectedParamNamesUnion = "paramName0, paramName1, paramName2";
        private String _result;
        private IParameterNameFactory _paramNameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
        private IParameterNamesJoiner _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
        private IParameterStubFactory _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
        private ParameterManager _paramMgr;


        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 0 ) ).Return( _expectedParamNames[0] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 1 ) ).Return( _expectedParamNames[1] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 2 ) ).Return( _expectedParamNames[2] );
            _parameterNamesJoiner.Stub( pnj => pnj.JoinNames( Arg<IEnumerable<String>>.List.Equal( _expectedParamNames ) ) ).Return( _expectedParamNamesUnion );
        }


        [Test]
        [TestCase( "value0", "value1", "value2" )]
        [TestCase( 0, 1, 2 )]
        [TestCase( true, true, false )]
        [TestCase( 'A', 'B', 'C' )]
        [TestCase( 1, 3.14, 9999999999.9999 )]
        [TestCase( "value0", 2, true )]
        public void The_parameter_manager_should_return_the_string_from_the_parameter_names_joiner( Object value0, Object value1, Object value2 )
        {
            var inputParam = new Object[] { value0, value1, value2 };
            _paramMgr = new ParameterManager( _paramNameGenerator, _parameterNamesJoiner, _parameterStubFactory );
            _result = _paramMgr.AddParameter( inputParam );
            Assert.AreEqual( _expectedParamNamesUnion, _result );
        }
    }


    [TestFixture]
    public class When_adding_both_an_enumerable_of_parameters_and_an_individual_parameter_to_a_parameter_manager
    {
        private List<String> _paramEnum = new List<String> { "inputParamA1", "inputParamA2", "inputParamA3" };
        private List<String> _expectedParamEnumNames = new List<String> { "paramName1", "paramName2", "paramName3" };
        private String _expectedParamEnumNamesUnion = "paramName1, paramName2, paramName3";

        private String _individualInputParam = "inputParamB";
        private String _expectedIndividualParamName = "paramName4";

        private IEnumerable<String> _expectedResults;

        private IEnumerable<String> _results;
        private IParameterNameFactory _paramNameGenerator;
        private IParameterNamesJoiner _parameterNamesJoiner;
        private IParameterStubFactory _parameterStubFactory;
        private ParameterManager _paramMgr;


        [SetUp]
        public void SetupTest()
        {
            _paramNameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 0 ) ).Return( _expectedParamEnumNames[0] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 1 ) ).Return( _expectedParamEnumNames[1] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 2 ) ).Return( _expectedParamEnumNames[2] );
            _paramNameGenerator.Stub( png => png.GenerateParameterName( 3 ) ).Return( _expectedIndividualParamName );
            _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
            _parameterNamesJoiner.Stub( pnj => pnj.JoinNames( Arg<IEnumerable<String>>.List.Equal( _expectedParamEnumNames ) ) ).Return( _expectedParamEnumNamesUnion );
            _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
            _expectedResults = new List<String> { _expectedParamEnumNamesUnion, _expectedIndividualParamName };
            _paramMgr = new ParameterManager( _paramNameGenerator, _parameterNamesJoiner, _parameterStubFactory );
            _results = _paramMgr.AddParameters( _paramEnum, _individualInputParam );
        }


        [Test]
        public void Test_test()
        {
            CollectionAssert.AreEqual( _expectedResults, _results );
        }
    }


    namespace When_retrieving_parameters_from_a_parameter_manager
    {
        [TestFixture]
        public class Given_a_parameter_manager_with_no_parameters
        {
            private IParameterNameFactory _nameGenerator;
            private IParameterNamesJoiner _parameterNamesJoiner;
            private IParameterStubFactory _parameterStubFactory;
            private ParameterManager _paramMgr;
            private IEnumerable<IParameterStub> _results;


            [TestFixtureSetUp]
            public void SetupFixture()
            {
                _nameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
                _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
                _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
            }


            [SetUp]
            public void SetupTest()
            {
                _paramMgr = new ParameterManager( _nameGenerator, _parameterNamesJoiner, _parameterStubFactory );
                _results = _paramMgr.GetParameterStubs();
            }


            [Test]
            public void The_parameter_manager_should_return_an_empty_parameter_stub_collection()
            {
                Assert.AreEqual( 0, _results.Count() );
            }
        }


        [TestFixture]
        public class Given_a_parameter_manager_with_one_parameter_value_and_no_db_type
        {
            private String _inputParam;
            private String _paramName;
            private String _returnedParamName;
            private IParameterNameFactory _nameGenerator;
            private IParameterNamesJoiner _parameterNamesJoiner;
            private IParameterStubFactory _parameterStubFactory;
            private IParameterStub _parameterStub;
            private ParameterManager _paramMgr;
            private IEnumerable<IParameterStub> _results;
            private IEnumerable<IParameterStub> _expectedResults;


            [TestFixtureSetUp]
            public void SetupFixture()
            {
                _inputParam = "inputParam";
                _paramName = "paramName";
                _parameterStub = MockRepository.GenerateMock<IParameterStub>();
                _nameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
                _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
                _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName, _inputParam ) ).Return( _parameterStub );
                _nameGenerator.Stub( ng => ng.GenerateParameterName( 0 ) ).Return( _paramName );
            }


            [SetUp]
            public void SetupTest()
            {
                _paramMgr = new ParameterManager( _nameGenerator, _parameterNamesJoiner, _parameterStubFactory );
                _returnedParamName = _paramMgr.AddParameter( _inputParam );
                _expectedResults = new List<IParameterStub> { _parameterStub };
                _results = _paramMgr.GetParameterStubs();
            }


            [Test]
            public void The_parameter_manager_should_return_a_parameter_stub_collection_containing_the_stub_from_the_factory()
            {
                CollectionAssert.AreEqual( _expectedResults, _results );
            }
        }


        [TestFixture]
        public class Given_a_parameter_manager_with_multiple_parameter_values
        {
            private String _inputParamValue0;
            private String _inputParamValue1;
            private String _inputParamValue2;
            private List<String> _inputEnumParam;
            private String _paramName0;
            private String _paramName1;
            private String _paramName2;
            private String _paramName3;
            private String _paramName4;
            private String _paramName5;
            private IEnumerable<String> _paramNameCollection;
            private IParameterStub _paramStub0;
            private IParameterStub _paramStub1;
            private IParameterStub _paramStub2;
            private IParameterStub _paramStub3;
            private IParameterStub _paramStub4;
            private IParameterStub _paramStub5;
            private List<IParameterStub> _expectedParameterStubs;
            private IParameterNameFactory _nameFactory;
            private IParameterNamesJoiner _parameterNamesJoiner;
            private IParameterStubFactory _parameterStubFactory;
            private ParameterManager _paramMgr;
            private IEnumerable<IParameterStub> _results;


            [TestFixtureSetUp]
            public void SetupFixture()
            {
                _inputParamValue0 = "inputParam0";
                _inputParamValue1 = "inputParam1";
                _inputParamValue2 = "inputParam2";
                _inputEnumParam = new List<String> { "inputParam3", "inputParam4", "inputParam5" };

                _paramName0 = "paramName0";
                _paramName1 = "paramName1";
                _paramName2 = "paramName2";
                _paramName3 = "paramName3";
                _paramName4 = "paramName4";
                _paramName5 = "paramName5";
                _paramNameCollection = new List<String> { _paramName3, _paramName4, _paramName5 };

                _paramStub0 = MockRepository.GenerateMock<IParameterStub>();
                _paramStub1 = MockRepository.GenerateMock<IParameterStub>();
                _paramStub2 = MockRepository.GenerateMock<IParameterStub>();
                _paramStub3 = MockRepository.GenerateMock<IParameterStub>();
                _paramStub4 = MockRepository.GenerateMock<IParameterStub>();
                _paramStub5 = MockRepository.GenerateMock<IParameterStub>();

                _expectedParameterStubs = new List<IParameterStub>();
                _expectedParameterStubs.Add( _paramStub0 );
                _expectedParameterStubs.Add( _paramStub1 );
                _expectedParameterStubs.Add( _paramStub2 );
                _expectedParameterStubs.Add( _paramStub3 );
                _expectedParameterStubs.Add( _paramStub4 );
                _expectedParameterStubs.Add( _paramStub5 );

                _nameFactory = MockRepository.GenerateMock<IParameterNameFactory>();
                _nameFactory.Stub( ng => ng.GenerateParameterName( 0 ) ).Return( _paramName0 );
                _nameFactory.Stub( ng => ng.GenerateParameterName( 1 ) ).Return( _paramName1 );
                _nameFactory.Stub( ng => ng.GenerateParameterName( 2 ) ).Return( _paramName2 );
                _nameFactory.Stub( ng => ng.GenerateParameterName( 3 ) ).Return( _paramName3 );
                _nameFactory.Stub( ng => ng.GenerateParameterName( 4 ) ).Return( _paramName4 );
                _nameFactory.Stub( ng => ng.GenerateParameterName( 5 ) ).Return( _paramName5 );

                _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();

                _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName0, _inputParamValue0 ) ).Return( _paramStub0 );
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName1, _inputParamValue1 ) ).Return( _paramStub1 );
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName2, _inputParamValue2 ) ).Return( _paramStub2 );
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName3, _inputEnumParam[0] ) ).Return( _paramStub3 );
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName4, _inputEnumParam[1] ) ).Return( _paramStub4 );
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName5, _inputEnumParam[2] ) ).Return( _paramStub5 );
            }


            [SetUp]
            public void SetupTest()
            {
                _paramMgr = new ParameterManager( _nameFactory, _parameterNamesJoiner, _parameterStubFactory );
                _paramMgr.AddParameter( _inputParamValue0 );
                _paramMgr.AddParameter( _inputParamValue1 );
                _paramMgr.AddParameter( _inputParamValue2 );
                _paramMgr.AddParameter( _inputEnumParam );
                _results = _paramMgr.GetParameterStubs();
            }


            [Test]
            public void The_parameter_manager_should_return_a_parameter_stub_collection_containing_the_stubs_from_the_factory()
            {
                CollectionAssert.AreEqual( _expectedParameterStubs, _results );
            }
        }


        [TestFixture]
        public class Given_a_parameter_manager_with_one_parameter_value_and_a_db_type
        {
            private String _inputParamValue;
            private DbType _inputDbType;
            private String _paramName;
            private String _returnedParamName;
            private IParameterNameFactory _nameGenerator;
            private IParameterNamesJoiner _parameterNamesJoiner;
            private IParameterStubFactory _parameterStubFactory;
            private IParameterStub _parameterStub;
            private ParameterManager _paramMgr;
            private IEnumerable<IParameterStub> _results;
            private IEnumerable<IParameterStub> _expectedResults;


            [TestFixtureSetUp]
            public void SetupFixture()
            {
                _inputParamValue = "inputParam";
                _inputDbType = DbType.String;
                _paramName = "paramName";
                _parameterStub = MockRepository.GenerateMock<IParameterStub>();
                _nameGenerator = MockRepository.GenerateMock<IParameterNameFactory>();
                _parameterNamesJoiner = MockRepository.GenerateMock<IParameterNamesJoiner>();
                _parameterStubFactory = MockRepository.GenerateMock<IParameterStubFactory>();
                _parameterStubFactory.Stub( f => f.CreateParameterStub( _paramName, _inputParamValue, _inputDbType ) ).Return( _parameterStub );
                _nameGenerator.Stub( ng => ng.GenerateParameterName( 0 ) ).Return( _paramName );
            }


            [SetUp]
            public void SetupTest()
            {
                _paramMgr = new ParameterManager( _nameGenerator, _parameterNamesJoiner, _parameterStubFactory );
                _returnedParamName = _paramMgr.AddParameter( _inputParamValue, _inputDbType );
                _expectedResults = new List<IParameterStub> { _parameterStub };
                _results = _paramMgr.GetParameterStubs();
            }


            [Test]
            public void The_parameter_manager_should_return_a_parameter_stub_collection_containing_the_stub_from_the_factory()
            {
                CollectionAssert.AreEqual( _expectedResults, _results );
            }
        }
    }
}