using System;
using System.Data;
using IDbEz.Extensions;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Extensions.DataReaderExtensionsSpecs
{
    [TestFixture]
    public class When_checking_a_data_reader_column_for_a_null_value_given_a_data_reader_and_column_name : Concerns_for_data_reader_extensions
    {
        [Test]
        [TestCase( true )]
        [TestCase( false )]
        public void The_IsDbNull_extension_should_return_the_whether_the_column_is_db_null( Boolean testColumnIsDbNull )
        {
            _dataReader.Stub( r => r.GetOrdinal( _columnName ) ).Return( _columnNum );
            _dataReader.Stub( r => r.IsDBNull( _columnNum ) ).Return( testColumnIsDbNull );

            var result = _dataReader.IsDBNull( _columnName );

            Assert.AreEqual( testColumnIsDbNull, result );
        }
    }


    [TestFixture]
    public class When_retrieving_a_nullable_datetime_value_from_a_data_reader_column_given_a_data_reader_and_column_name_of_a_null_column : Concerns_for_data_reader_extensions
    {
        [Test]
        public void The_DateTime_extension_should_return_null()
        {
            _dataReader.Stub( r => r.GetOrdinal( _columnName ) ).Return( _columnNum );
            _dataReader.Stub( r => r.IsDBNull( _columnNum ) ).Return( true );

            DateTime? result = _dataReader.GetDateTime( _columnName );

            Assert.IsNull( result );
        }
    }


    [TestFixture]
    public class When_retrieving_a_nullable_datetime_value_from_a_data_reader_column_given_a_data_reader_and_column_name_of_a_column_containing_a_date_time_value : Concerns_for_data_reader_extensions
    {
        [Test]
        public void The_DateTime_extension_should_return_the_date_time_value()
        {
            _dataReader.Stub( r => r.GetOrdinal( _columnName ) ).Return( _columnNum );
            _dataReader.Stub( r => r.IsDBNull( _columnNum ) ).Return( false );
            _dataReader.Stub( r => r.GetDateTime( _columnNum ) ).Return( _dateTime );

            DateTime? result = _dataReader.GetDateTime( _columnName );

            Assert.AreEqual( _dateTime, result );
        }
    }


    public class Concerns_for_data_reader_extensions
    {
        protected String _columnName;
        protected Int32 _columnNum;
        protected DateTime _dateTime;
        protected IDataReader _dataReader;


        [SetUp]
        public virtual void SetupTest()
        {
            _columnName = "column";
            _columnNum = 1;
            _dateTime = DateTime.Now;
            _dataReader = MockRepository.GenerateMock<IDataReader>();
        }
    }
}
