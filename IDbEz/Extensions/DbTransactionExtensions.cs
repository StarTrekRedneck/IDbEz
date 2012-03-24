using System;
using System.Collections.Generic;
using System.Data;


namespace IDbEz.Extensions
{
    public static class DbTransactionExtensions
    {
        public static IDbCommand CreateCommand( this IDbTransaction dbTransaction, String commandText )
        {
            IDbCommand dbCommand = dbTransaction.Connection.CreateCommand( commandText );
            dbCommand.Transaction = dbTransaction;
            return dbCommand;
        }


        public static IDbCommand CreateCommand( this IDbTransaction dbTransaction, String commandText, IEnumerable<IParameterStub> parameterStubs )
        {
            IDbCommand dbCommand = dbTransaction.CreateCommand( commandText );
            dbCommand.AddParameters( parameterStubs );
            return dbCommand;
        }


        public static void ExecuteReader( this IDbTransaction dbTransaction, Action<IDataReader> action, String sql, params Object[] parameterValues )
        {
            var queryBuilder = Ez.QueryBuilder();
            queryBuilder.Add( sql, parameterValues );
            ExecuteReader( dbTransaction, queryBuilder, action );
        }


        public static void ExecuteReader( this IDbTransaction dbTransaction, IQueryBuilder query, Action<IDataReader> action )
        {
            ExecuteReader( dbTransaction, query.GetSql(), query.GetParameters(), action );
        }


        public static void ExecuteReader( this IDbTransaction dbTransaction, String sql, IEnumerable<IParameterStub> parameterStubs, Action<IDataReader> action )
        {
            using ( IDbCommand dbCommand = dbTransaction.CreateCommand( sql, parameterStubs ) )
            using ( IDataReader dbReader = dbCommand.ExecuteReader() )
            {
                action( dbReader );
            }
        }


        public static void ExecuteReaderAndRead( this IDbTransaction dbTransaction, Action<IDataReader> action, String sql, params Object[] parameterValues )
        {
            ExecuteReader( dbTransaction,
                           dbReader => { while ( dbReader.Read() ) action( dbReader ); },
                           sql,
                           parameterValues );
        }


        public static void ExecuteReaderAndRead( this IDbTransaction dbTransaction, IQueryBuilder query, Action<IDataReader> action )
        {
            ExecuteReader( dbTransaction, 
                           query.GetSql(),
                           query.GetParameters(),
                           dbReader => { while ( dbReader.Read() ) action( dbReader ); } );
        }


        public static void ExecuteReaderAndRead( this IDbTransaction dbTransaction, String sql, IEnumerable<IParameterStub> parameterStubs, Action<IDataReader> action )
        {
            ExecuteReader( dbTransaction,
                           sql,
                           parameterStubs,
                           dbReader => { while ( dbReader.Read() ) action( dbReader ); } );
        }


        public static void ExecuteNonQuery( this IDbTransaction dbTransaction, IQueryBuilder query )
        {
            ExecuteNonQuery( dbTransaction, query.GetSql(), query.GetParameters() );
        }


        public static void ExecuteNonQuery( this IDbTransaction dbTransaction, String sql, params Object[] parameterValues )
        {
            var queryBuilder = Ez.QueryBuilder();
            queryBuilder.Add( sql, parameterValues );
            ExecuteNonQuery( dbTransaction, queryBuilder );
        }


        public static void ExecuteNonQuery( this IDbTransaction dbTransaction, String sql, IEnumerable<IParameterStub> parameterStubs )
        {
            using ( IDbCommand dbCommand = dbTransaction.CreateCommand( sql, parameterStubs ) )
            {
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}