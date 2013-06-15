using System;
using System.Collections.Generic;
using System.Data;


namespace IDbEz.Extensions
{
    public static class DbCommandExtensions
    {
        public static IDbDataParameter CreateParameter( this IDbCommand dbCommand, IParameterStub parameterStub )
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter( parameterStub.ParameterName, parameterStub.Value );

            if ( parameterStub.DbTypeManuallySet )
                dbParameter.DbType = parameterStub.DbType;

            if ( parameterStub.DirectionManuallySet )
                dbParameter.Direction = parameterStub.Direction;

            if ( parameterStub.PrecisionManuallySet )
                dbParameter.Precision = parameterStub.Precision;

            if ( parameterStub.ScaleManuallySet )
                dbParameter.Scale = parameterStub.Scale;

            if ( parameterStub.SizeManuallySet )
                dbParameter.Size = parameterStub.Size;

            if ( parameterStub.SourceColumnManuallySet )
                dbParameter.SourceColumn = parameterStub.SourceColumn;

            if ( parameterStub.SourceVersionManuallySet )
                dbParameter.SourceVersion = parameterStub.SourceVersion;

            return dbParameter;
        }


        public static IDbDataParameter CreateParameter( this IDbCommand dbCommand, String dbParameterName, Object dbParameterValue )
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = dbParameterName;
            dbParameter.Value = dbParameterValue;
            return dbParameter;
        }


        public static IDbDataParameter CreateParameter( this IDbCommand dbCommand, String dbParameterName, Object dbParameterValue, DbType dbType )
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter(dbParameterName, dbParameterValue);
            dbParameter.DbType = dbType;
            return dbParameter;
        }


        public static Int32 AddParameter( this IDbCommand dbCommand, String dbParameterName, Object dbParameterValue )
        {
            return dbCommand.Parameters.Add( dbCommand.CreateParameter( dbParameterName, dbParameterValue ) );
        }


        public static void AddParameters( this IDbCommand dbCommand, IEnumerable<IParameterStub> parameterStubs )
        {
            foreach ( var parameterStub in parameterStubs )
            {
                dbCommand.AddParameter( parameterStub.ParameterName, parameterStub.Value );
            }
        }
    }
}