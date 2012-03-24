using System;
using System.Data;
using IDbEz.Extensions;
using NUnit.Framework;
using Rhino.Mocks;


namespace IDbEz.Specs.Extensions.DbConnectionExtensionsSpecs
{
    namespace Given_a_db_connection
    {
        [TestFixture]
        public class When_sending_an_action_to_run_within_a_db_transaction
        {
            private IDbConnection _dbConnection;
            private IDbTransaction _dbTransaction;
            private Action<IDbTransaction> _action;


            [SetUp]
            public void SetupTest()
            {
                _dbConnection = MockRepository.GenerateMock<IDbConnection>();
                _dbTransaction = MockRepository.GenerateMock<IDbTransaction>();
                _dbConnection.Stub( c => c.BeginTransaction() ).Return( _dbTransaction );
                _action = MockRepository.GenerateMock<Action<IDbTransaction>>();
                _dbConnection.RunInTransaction( _action );
            }


            [Test]
            public void The_extension_should_begin_a_transaction_from_the_connection()
            {
                _dbConnection.AssertWasCalled( c => c.BeginTransaction() );
            }


            [Test]
            public void The_extenstion_should_invoke_the_action_with_the_begun_transaction()
            {
                _action.AssertWasCalled( a => a.Invoke( _dbTransaction ) );
            }


            [Test]
            public void The_extension_should_commit_the_transaction()
            {
                _dbTransaction.AssertWasCalled( t => t.Commit() );
            }


            [Test]
            public void The_extension_should_dispose_of_the_transaction()
            {
                _dbTransaction.AssertWasCalled( t => t.Dispose() );
            }
        }


        [TestFixture]
        public class When_creating_a_db_command_given_a_command_text_string
        {
            private IDbConnection _dbConnection;
            private IDbCommand _command;
            private String _commandText;
            private IDbCommand _result;

            [SetUp]
            public void SetupTest()
            {
                _dbConnection = MockRepository.GenerateMock<IDbConnection>();
                _commandText = "test command";
                _command = MockRepository.GenerateMock<IDbCommand>();
                _dbConnection.Stub( c => c.CreateCommand() ).Return( _command );
                _result = _dbConnection.CreateCommand( _commandText );
            }


            [Test]
            public void The_extension_should_return_a_command_from_the_connection()
            {
                Assert.AreSame( _result, _command );
            }


            [Test]
            public void The_extension_should_add_the_command_text_to_the_returned_command()
            {
                _result.AssertWasCalled( c => c.CommandText = _commandText );
            }
        }
    }
}
