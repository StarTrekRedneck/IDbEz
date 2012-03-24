using System;
using System.Data;


namespace IDbEz.Extensions
{
    public static class DbConnectionExtensions
    {
        public static void RunInTransaction( this IDbConnection dbConnection, Action<IDbTransaction> action )
        {
            using ( IDbTransaction dbTransaction = dbConnection.BeginTransaction() )
            {
                action( dbTransaction );
                dbTransaction.Commit();
            }
        }


        public static IDbCommand CreateCommand( this IDbConnection dbConnection, String commandText )
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }


        public static void ExecuteReader( this IDbConnection dbConnection, IQueryBuilder queryBuilder, Action<IDataReader> action )
        {
            using ( IDbTransaction dbTransaction = dbConnection.BeginTransaction() )
            {
                dbTransaction.ExecuteReader( queryBuilder, action );
            }
        }
    }
}