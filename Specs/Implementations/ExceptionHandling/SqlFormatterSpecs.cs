using IDbEz.Implementations.ExceptionHandling;
using NUnit.Framework;


namespace IDbEz.Specs.Implementations.ExceptionHandling.SqlFormatterSpecs
{
    [TestFixture]
    public class Given_an_sql_string
    {
        [Test]
        public void The_formatter_should_return_the_sql_string_unaltered()
        {
            var sqlFormatter = new SqlFormatter();
            var sql = "given sql";
            var formattedSql = sqlFormatter.Format( sql );

            Assert.AreEqual( sql, formattedSql );
        }
    }
}