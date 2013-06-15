using System;
using System.Data;


namespace IDbEz.Extensions
{
    public static class DataReaderExtensions
    {
        public static Boolean IsDBNull( this IDataReader dataReader, String columnName )
        {
            return dataReader.IsDBNull( dataReader.GetOrdinal( columnName ) );
        }


        public static DateTime? GetDateTime( this IDataReader dataReader, String columnName )
        {
            return ( dataReader.IsDBNull( columnName ) ? (DateTime?)null : dataReader.GetDateTime( dataReader.GetOrdinal( columnName ) ) );
        }


        public static void Read( this IDataReader dataReader, Action<IDataReader> action )
        {
            while ( dataReader.Read() ) action( dataReader );
        }
    }
}