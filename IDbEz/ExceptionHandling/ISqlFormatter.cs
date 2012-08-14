using System;


namespace IDbEz.ExceptionHandling
{
    public interface ISqlFormatter
    {
        String Format( String sql );
    }
}