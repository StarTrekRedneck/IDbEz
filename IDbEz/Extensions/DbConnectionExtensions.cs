using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


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


		public static IDbCommand CreateCommand( this IDbConnection dbConnection, IQueryBuilder queryBuilder )
		{
			return dbConnection.CreateCommand( queryBuilder.GetSql(), queryBuilder.GetParameters() );
		}


		public static IDbCommand CreateCommand( this IDbConnection dbConnection, String commandText, IEnumerable<IParameterStub> parameterStubs )
		{
			IDbCommand dbCommand = dbConnection.CreateCommand( commandText );
			dbCommand.AddParameters( parameterStubs );
			return dbCommand;
		}


        public static IDbCommand CreateCommand( this IDbConnection dbConnection, String commandText )
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }


		public static void ExecuteReader( this IDbConnection dbConnection, IQueryBuilder queryBuilder, Action<IDataReader> action )
		{
			try
			{
				RootExecuteReader( dbConnection, queryBuilder, action );
			}
			catch ( DbException ex )
			{
				Ez.DbExceptionHandler().Handle( ex, queryBuilder.GetSql(), queryBuilder.GetParameters() );
			}
		}


		public static void ExecuteReader( this IDbConnection dbConnection, String sql, Action<IDataReader> action )
		{
			try
			{
				RootExecuteReader( dbConnection, sql, action );
			}
			catch ( DbException ex )
			{
				Ez.DbExceptionHandler().Handle( ex, sql );
			}
		}


		private static void RootExecuteReader( this IDbConnection dbConnection, IQueryBuilder queryBuilder, Action<IDataReader> action )
		{
			using ( IDbCommand dbCommand = dbConnection.CreateCommand( queryBuilder ) )
			using ( IDataReader dataReader = dbCommand.ExecuteReader() )
			{
				action( dataReader );
			}
		}


		private static void RootExecuteReader( this IDbConnection dbConnection, String sql, Action<IDataReader> action )
		{
			using ( IDbCommand dbCommand = dbConnection.CreateCommand( sql ) )
			using ( IDataReader dataReader = dbCommand.ExecuteReader() )
			{
				action( dataReader );
			}
		}


		public static void ExecuteReaderAndRead( this IDbConnection dbConnection, IQueryBuilder queryBuilder, Action<IDataReader> action )
        {
			ExecuteReader( dbConnection, 
						   queryBuilder, 
						   dataReader => dataReader.Read( action ) );
        }
    }
}