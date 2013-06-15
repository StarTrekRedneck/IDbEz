using System;
using System.Collections.Generic;
using System.Data;
using IDbEz.Implementations;
using IDbEz.ExceptionHandling;
using IDbEz.Implementations.ExceptionHandling;


namespace IDbEz
{
    public static class Ez
    {
        private static Func<IQueryBuilder> _queryBuilderFactory;
        private static Func<ISqlBuilder> _sqlBuilderFactory;
        private static Func<IParameterManager> _parameterManagerFactory;
        private static Func<IParameterStubFactory> _parameterStubFactoryFactory;
        private static Func<IParameterNamePrefixRepository> _parameterNamePrefixRepositoryFactory;
        private static Func<IParameterRootNameRepository> _parameterRootNameRepositoryFactory;
        private static Func<IParameterUnnumberedNameRepository> _parameterUnnumberedNameRepositoryFactory;
        private static Func<IParameterNamesSeparatorSource> _parameterNamesSeparatorSourceFactory;
        private static Func<IParameterNamesJoiner> _parameterNamesJoinerFactory;
        private static Func<IParameterNameFactory> _parameterNameFactoryFactory;
        private static Func<IDbExceptionHandler> _dbExceptionHandlerFactory;
        private static Func<IExceptionMessageFormatter> _exceptionMessageFormatterFactory;
        private static Func<ISqlFormatter> _sqlFormatterFactory;
        private static Func<IParameterStubCollectionFormatter> _parameterStubCollectionFormatterFactory;
        private static Func<IParameterStubFormatter> _parameterStubFormatterFactory;

        //private static Dictionary<Type, Func<Object>> _registry = new Dictionary<Type, Func<Object>>();

        public static IQueryBuilder QueryBuilder()
        {
            return Resolve( _queryBuilderFactory, () => new QueryBuilder( SqlBuilder(), ParameterManager() ) );
        }


        public static void ReplaceQueryBuilder( Func<IQueryBuilder> queryBuilderFactory )
        {
            _queryBuilderFactory = queryBuilderFactory;
        }


        public static ISqlBuilder SqlBuilder()
        {
            return Resolve( _sqlBuilderFactory, () => new SqlBuilder() );
        }


        public static void ReplaceSqlBuilder( Func<ISqlBuilder> sqlBuilderFactory )
        {
            _sqlBuilderFactory = sqlBuilderFactory;
        }


        public static IParameterManager ParameterManager()
        {
            return Resolve( _parameterManagerFactory, () => new ParameterManager( ParameterNameFactory(), ParameterNamesJoiner(), ParameterStubFactory() ) );
        }


        public static void ReplaceParameterManager( Func<IParameterManager> parameterManagerFactory )
        {
            //if ( _registry.ContainsKey( typeof( IParameterManager ) ) )
            //{
            //    _registry[typeof( IParameterManager )] = parameterManagerFactory;
            //}
            //else
            //{
            //    _registry.Add( typeof( IParameterManager ), parameterManagerFactory );
            //}
            _parameterManagerFactory = parameterManagerFactory;
        }


        public static IParameterStubFactory ParameterStubFactory()
        {
            return Resolve( _parameterStubFactoryFactory, () => new ParameterStubFactory() );
        }


        public static void ReplaceParameterStubFactory( Func<IParameterStubFactory> parameterStubFactoryFactory )
        {
            _parameterStubFactoryFactory = parameterStubFactoryFactory;
        }


        public static IParameterNamePrefixRepository ParameterNamePrefixRepository()
        {
            return Resolve( _parameterNamePrefixRepositoryFactory, () => new ParameterNamePrefixRepository() );
        }


        public static void ReplaceParameterNamePrefixRepository( Func<IParameterNamePrefixRepository> parameterNamePrefixRepositoryFactory )
        {
            _parameterNamePrefixRepositoryFactory = parameterNamePrefixRepositoryFactory;
        }


        public static IParameterRootNameRepository ParameterRootNameRepository()
        {
            return Resolve( _parameterRootNameRepositoryFactory, () => new ParameterRootNameRepository() );
        }


        public static void ReplaceParameterRootNameRepository( Func<IParameterRootNameRepository> parameterRootNameRepositoryFactory )
        {
            _parameterRootNameRepositoryFactory = parameterRootNameRepositoryFactory;
        }


        public static IParameterUnnumberedNameRepository ParameterUnnumberedNameRepository()
        {
            return Resolve( _parameterUnnumberedNameRepositoryFactory, () => new ParameterUnnumberedNameRepository( ParameterNamePrefixRepository(), ParameterRootNameRepository() ) );
        }


        public static void ReplaceParameterNamePrefixRepository( Func<IParameterUnnumberedNameRepository> parameterUnnumberedNameRepositoryFactory )
        {
            _parameterUnnumberedNameRepositoryFactory = parameterUnnumberedNameRepositoryFactory;
        }


        public static IParameterNamesSeparatorSource ParameterNamesSeparatorSource()
        {
            return Resolve( _parameterNamesSeparatorSourceFactory, () => new ParameterNamesSeparatorSource() );
        }


        public static void ReplaceParameterNamesSeparatorSource( Func<IParameterNamesSeparatorSource> parameterNamesSeparatorSourceFactory )
        {
            _parameterNamesSeparatorSourceFactory = parameterNamesSeparatorSourceFactory;
        }


        public static IParameterNamesJoiner ParameterNamesJoiner()
        {
            return Resolve( _parameterNamesJoinerFactory, () => new ParameterNamesJoiner( ParameterNamesSeparatorSource() ) );
        }


        public static void ReplaceParameterNamesJoiner( Func<IParameterNamesJoiner> parameterNamesJoinerFactory )
        {
            _parameterNamesJoinerFactory = parameterNamesJoinerFactory;
        }


        public static IParameterNameFactory ParameterNameFactory()
        {
            return Resolve( _parameterNameFactoryFactory, () => new ParameterNameFactory( ParameterUnnumberedNameRepository() ) );
        }


        public static void ReplaceParameterNameFactory( Func<IParameterNameFactory> parameterNameFactoryFactory )
        {
            _parameterNameFactoryFactory = parameterNameFactoryFactory;
        }


        public static IDbExceptionHandler DbExceptionHandler()
        {
            return Resolve( _dbExceptionHandlerFactory, () => new DbExceptionHandler( Ez.ExceptionMessageFormatter() ) );
        }


        public static void ReplaceDbExceptionHandler( Func<IDbExceptionHandler> dbExceptionHandlerFactory )
        {
            _dbExceptionHandlerFactory = dbExceptionHandlerFactory;
        }


        public static IExceptionMessageFormatter ExceptionMessageFormatter()
        {
            return Resolve( _exceptionMessageFormatterFactory, () => new ExceptionMessageFormatter( Ez.SqlFormatter(), Ez.ParameterStubCollectionFormatter() ) );
        }


        public static void ReplaceExceptionMessageFormatter( Func<IExceptionMessageFormatter> exceptionMessageFormatterFactory )
        {
            _exceptionMessageFormatterFactory = exceptionMessageFormatterFactory;
        }


        public static ISqlFormatter SqlFormatter()
        {
            return Resolve( _sqlFormatterFactory, () => new SqlFormatter() );
        }


        public static void ReplaceSqlFormatter( Func<ISqlFormatter> sqlFormatterFactory )
        {
            _sqlFormatterFactory = sqlFormatterFactory;
        }


        public static IParameterStubCollectionFormatter ParameterStubCollectionFormatter()
        {
            return Resolve( _parameterStubCollectionFormatterFactory, () => new ParameterStubCollectionFormatter( Ez.ParameterStubFormatter() ) );
        }


        public static void ReplaceParameterStubCollectionFormatter( Func<IParameterStubCollectionFormatter> parameterStubCollectionFormatterFactory )
        {
            _parameterStubCollectionFormatterFactory = parameterStubCollectionFormatterFactory;
        }


        public static IParameterStubFormatter ParameterStubFormatter()
        {
            return Resolve( _parameterStubFormatterFactory, () => new ParameterStubFormatter() );
        }


        public static void ReplaceParameterStubFormatter( Func<IParameterStubFormatter> parameterStubFormatterFactory )
        {
            _parameterStubFormatterFactory = parameterStubFormatterFactory;
        }


        private static T Resolve<T>( Func<T> factory, Func<T> defaultFactory )
        {
            return factory != null
                ? factory.Invoke()
                : defaultFactory.Invoke();
        }
    }
}