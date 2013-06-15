using System;
using System.Data.Common;


namespace IDbEz.ExceptionHandling
{
    public class DbEzException : DbException
    {
        public DbEzException( String message )
            : base( message )
        {
        }


        public DbEzException( String message, DbException innerException )
            : base( message, innerException )
        {
        }
    }
}