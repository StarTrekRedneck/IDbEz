using IDbEz.Implementations;
using NUnit.Framework;


namespace IDbEz.Specs.Implementations.SqlBuilderSpecs
{
    [TestFixture]
    public class When_adding_a_string_to_a_sql_builder
    {
        [Test]
        public void The_sql_builder_should_return_a_reference_to_itself()
        {
            var inputSql = "input";
            var sqlBuilder = new SqlBuilder();
            var result = sqlBuilder.Add( inputSql );
            Assert.AreEqual( sqlBuilder, result );
        }
    }


    [TestFixture]
    public class When_adding_a_string_and_a_parmater_name_to_a_sql_builder
    {
        [Test]
        public void The_sql_builder_should_return_a_reference_to_itself()
        {
            var inputSql = "input";
            var parameterName = "name";
            var sqlBuilder = new SqlBuilder();
            var result = sqlBuilder.Add( inputSql, parameterName );
            Assert.AreEqual( sqlBuilder, result );
        }
    }


    namespace When_retrieving_the_generated_sql_from_a_sql_builder
    {
        [TestFixture]
        public class Given_a_sql_builder_with_an_input_sql_string_and_no_parameter_name
        {
            [Test]
            public void The_sql_builder_should_return_a_sql_string_equal_to_the_input()
            {
                var inputSql = "input";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql );
                var resultSql = sqlBuilder.GetSql();

                Assert.AreEqual( inputSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_two_input_sql_strings_and_no_parameter_name
        {
            [Test]
            public void The_sql_builder_should_return_a_concatenated_sql_string()
            {
                var inputSql1 = "input1";
                var inputSql2 = "input2";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql1 );
                sqlBuilder.Add( inputSql2 );

                var expectedSql = inputSql1 + inputSql2;
                var resultSql = sqlBuilder.GetSql();

                Assert.AreEqual( expectedSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_an_input_sql_string_with_one_parameter_placeholder_and_one_parameter_name
        {
            [Test]
            public void The_sql_builder_should_return_a_string_with_the_placeholder_replaced_with_the_parameter_name()
            {
                var inputSql = "input {0}";
                var parameterName = "parameter";
                var expectedSql = "input parameter";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql, parameterName );

                var resultSql = sqlBuilder.GetSql();
                Assert.AreEqual( expectedSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_two_input_sql_strings_with_one_parameter_placeholder_and_one_parameter_name
        {
            [Test]
            public void The_sql_builder_should_return_a_concatenated_string_with_the_placeholder_replaced_with_the_parameter_name()
            {
                var inputSql1 = "input1 {0}";
                var inputSql2 = "input2";
                var parameterName = "parameter";
                var expectedSql = "input1 parameterinput2";

                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql1, parameterName );
                sqlBuilder.Add( inputSql2 );

                var resultSql = sqlBuilder.GetSql();
                Assert.AreEqual( expectedSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_an_input_sql_string_with_two_parameter_placeholders_and_two_parameter_names
        {
            [Test]
            public void The_sql_builder_should_return_a_string_with_the_placeholders_replaced_in_order_with_the_parameter_names()
            {
                var inputSql = "input {0} {1}";
                var parameterName1 = "param1";
                var parameterName2 = "param2";
                var expectedSql = "input param1 param2";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql, parameterName1, parameterName2 );

                var resultSql = sqlBuilder.GetSql();
                Assert.AreEqual( expectedSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_two_input_sql_strings_with_two_parameter_placeholders_and_two_parameter_names
        {
            [Test]
            public void The_sql_builder_should_return_a_string_with_the_placeholders_replaced_in_order_with_the_parameter_names()
            {
                var inputSql = "input1 {0} ";
                var inputSql2 = "{0} input2";
                var parameterName1 = "param1";
                var parameterName2 = "param2";
                var expectedSql = "input1 param1 param2 input2";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql, parameterName1 );
                sqlBuilder.Add( inputSql2, parameterName2 );

                var resultSql = sqlBuilder.GetSql();
                Assert.AreEqual( expectedSql, resultSql );
            }
        }


        [TestFixture]
        public class Given_a_sql_builder_with_an_input_sql_string_and_a_null_parameter_name
        {
            [Test]
            public void The_sql_builder_should_return_a_sql_string_equal_to_the_one_input()
            {
                var inputSql = "input";
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Add( inputSql, null );
                var resultSql = sqlBuilder.GetSql();

                Assert.AreEqual( inputSql, resultSql );
            }
        }
    }
}