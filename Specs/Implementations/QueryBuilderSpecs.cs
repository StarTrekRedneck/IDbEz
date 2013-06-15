using System;
using System.Collections.Generic;
using System.Data;
using IDbEz.Implementations;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Implementations.QueryBuilderSpecs
{
    [TestFixture]
    public class When_a_string_is_added_to_the_query_builder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;

        private String _inputSql = "sql string";
        private QueryBuilder _queryBuilder;
        private IQueryBuilder _result;


        [SetUp]
        public void SetupTest()
        {
            _sqlBuilder = MockRepository.GenerateMock<ISqlBuilder>();
            _paramMgr = MockRepository.GenerateMock<IParameterManager>();
            _queryBuilder = new QueryBuilder( _sqlBuilder, _paramMgr );
            _result = _queryBuilder.Add( _inputSql );
        }


        [Test]
        public void The_query_builder_should_add_the_string_to_the_sql_builder()
        {
            _sqlBuilder.AssertWasCalled( sb => sb.Add( _inputSql ) );
        }


        [Test]
        public void The_query_builder_should_not_add_anything_to_the_parameter_manager()
        {
            _paramMgr.AssertWasNotCalled( pm => pm.AddParameters( Arg<Object[]>.Is.Anything ) );
        }


        [Test]
        public void The_query_builder_should_return_a_reference_to_itself()
        {

            Assert.AreSame( _queryBuilder, _result );
        }
    }


    [TestFixture]
    public class When_a_string_with_a_parameter_placeholder_and_a_parameter_are_added_to_a_query_builder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;

        private String _inputSql = "sql string {0}";
        private Object _parameterValue = "PARAM";
        private String[] _paramNames = new[] { "@param0" };
        private QueryBuilder _queryBuilder;
        private IQueryBuilder _result;


        [SetUp]
        public void SetupTest()
        {
            _sqlBuilder = MockRepository.GenerateMock<ISqlBuilder>();
            _paramMgr = MockRepository.GenerateMock<IParameterManager>();
            _paramMgr.Stub( m => m.AddParameters( _parameterValue ) ).Return( _paramNames );
            _queryBuilder = new QueryBuilder( _sqlBuilder, _paramMgr );
            _result = _queryBuilder.Add( _inputSql, _parameterValue );
        }


        [Test]
        public void The_query_builder_should_add_the_parameter_to_the_parameter_manager()
        {
            _paramMgr.AssertWasCalled( pm => pm.AddParameters( _parameterValue ) );
        }


        [Test]
        public void The_query_builder_should_add_the_string_to_the_sql_builder_with_the_parameter_name_array_from_the_parameter_manager()
        {
            _sqlBuilder.AssertWasCalled( sb => sb.Add( _inputSql, _paramNames ) );
        }


        [Test]
        public void The_query_builder_should_return_a_reference_to_itself()
        {
            Assert.AreSame( _queryBuilder, _result );
        }
    }


    [TestFixture]
    public class When_a_string_with_a_parameter_placeholder_and_a_parameter_value_and_dbType_are_added_to_a_query_builder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;

        private String _inputSql = "sql string {0}";
        private Object _parameterValue = "PARAM";
        private String _paramName = "@param0";
        private DbType _dbType = DbType.String;
        private QueryBuilder _queryBuilder;
        private IQueryBuilder _result;


        [SetUp]
        public void SetupTest()
        {
            _sqlBuilder = MockRepository.GenerateMock<ISqlBuilder>();
            _paramMgr = MockRepository.GenerateMock<IParameterManager>();
            _paramMgr.Stub( m => m.AddParameter( _parameterValue, _dbType ) ).Return( _paramName );
            _queryBuilder = new QueryBuilder( _sqlBuilder, _paramMgr );
            _result = _queryBuilder.Add( _inputSql, _parameterValue, _dbType );
        }


        [Test]
        public void The_query_builder_should_add_the_parameter_value_and_dbType_to_the_parameter_manager()
        {
            _paramMgr.AssertWasCalled( pm => pm.AddParameter( _parameterValue, _dbType ) );
        }


        [Test]
        public void The_query_builder_should_add_the_string_to_the_sql_builder_with_the_parameter_name_from_the_parameter_manager()
        {
            _sqlBuilder.AssertWasCalled( sb => sb.Add( _inputSql, _paramName ) );
        }


        [Test]
        public void The_query_builder_should_return_a_reference_to_itself()
        {
            Assert.AreSame( _queryBuilder, _result );
        }
    }


    [TestFixture]
    public class When_retrieving_sql_from_a_query_builder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;

        private String _outputSql = "output";
        private String _resultSql;
        private QueryBuilder _queryBuilder;


        [SetUp]
        public void SetupTest()
        {
            _sqlBuilder = MockRepository.GenerateMock<ISqlBuilder>();
            _sqlBuilder.Stub( sb => sb.GetSql() ).Return( _outputSql );
            _paramMgr = MockRepository.GenerateMock<IParameterManager>();
            _queryBuilder = new QueryBuilder( _sqlBuilder, _paramMgr );
            _resultSql = _queryBuilder.GetSql();
        }


        [Test]
        public void The_query_builder_should_return_the_sql_from_the_sql_builder()
        {
            Assert.AreEqual( _outputSql, _resultSql );
        }
    }


    [TestFixture]
    public class When_retrieving_parameters_from_a_query_builder
    {
        private ISqlBuilder _sqlBuilder;
        private IParameterManager _paramMgr;

        private IEnumerable<IParameterStub> _outputParameters = new List<IParameterStub>();
        private IEnumerable<IParameterStub> _resultParameters;
        private QueryBuilder _queryBuilder;


        [SetUp]
        public void SetupTest()
        {
            _sqlBuilder = MockRepository.GenerateMock<ISqlBuilder>();
            _paramMgr = MockRepository.GenerateMock<IParameterManager>();
            _paramMgr.Stub( pm => pm.GetParameterStubs() ).Return( _outputParameters );
            _queryBuilder = new QueryBuilder( _sqlBuilder, _paramMgr );
            _resultParameters = _queryBuilder.GetParameters();
        }


        [Test]
        public void The_query_builder_should_return_the_parameters_from_the_paremeter_manager()
        {
            Assert.AreEqual( _outputParameters, _resultParameters );
        }
    }
}