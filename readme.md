## IDbEz

IDbEz is a set of helper classes and extension methods created for use with the IDb*-related interfaces in System.Data.

IDbEz is a published NuGet package with no dependencies: Install-Package IDbEz

The intent is to abstract much of the work of creating commands, transactions, committing, disposing, creating parameters, etc. leaving little more than just the SQL.

For example, a typical method to retrieve data from a database might go something like this...

      public string GetUserNameFromDatabase( string userId )
      {
         string name = null;

         string sql = "select NAME from users where ID = @IdParameter";

         using ( IDbConnection dbConnection = GetDatabaseConnection( "connection string" ) )
         using ( IDbCommand = dbConnection.CreateCommand() )
         {
            dbCommand.CommandText = sql;

            var idParam = dbCommand.CreateParameter();
            idParam.ParameterName = "@IdParameter";
            idParam.Value = userId;
            dbCommand.Parameters.Add( idParam );

            using ( IDataReader dbReader = dbCommand.ExecuteReader() )
            {
               if ( dbReader.Read() )
               {
                  name = dbReader.GetString( dbReader.GetOrdinal( "NAME" ) ).Trim();
               }
            }
         }

         return name;
      }

With IDbEz, it might go something like this...

      public string GetUserNameFromDatabase( string userId )
      {
         string name = null;

         var sql = Ez.QueryBuilder().Add( "select NAME from users where ID = {0} ", userId );

         using ( IDbConnection dbConnection = GetDatabaseConnection( "connection string" ) )
         {
            dbConnection.ExecuteReaderAndRead( sql, reader => name = dbReader.GetString( "NAME" ) );
         }

         return name;
      }

It still uses parameterized SQL, but cuts out much of the plumbing. I really suggest isolating connection code in one location in an application that accepts a delegate, which would eliminate the using IDbConnection statements in both examples. Extension methods provide for a similar code style using Transactions.

The QueryBuilder() class can handle several parameters, such as...

      var sql = Ez.QueryBuilder();
      sql.Add( @" select *
                  from my_table
                  where col0 = {0},
                    and col1 = {1},
                    and col2 = {2}", value0, value1, value2 );

which would generate actual SQL like this...

      select *
      from my_table
      where col0 = @param0,
        and col1 = @param1,
        and col2 = @param2

And can also handle collections...

      var paramCol = new List<string> { "a", "b", "c" };

      var sql = Ez.QueryBuilder();
      sql.Add( @" select *
                  from my_table
                  where col0 in ({0}) ", paramCol );

which would generate this...

      select *
      from my_table
      where col0 in (@param0, @param1, @param2)
	  
Lines can be broken up if desired...

	  var sql = Ez.QueryBuilder().Add( "select * " );
	  sql.Add( "from mytable ");
	  sql.Add( "where id = {0} ", theId );
	  sql.Add( "  and version = {0} ", theVersion );

Notice that the use of the parameter placeholders (the '{0}') functions exactly like a StringBuilder, so each call to Add() gets it's own numbering sequence. This DOES NOT impact the parameter names, values, or order. Just imagine you're using StringBuilder and let IDbEz handle the parameterization. And yes, StringBuilder is being used behind the scenes. :-)

#A Better Example

Like I mentioned above, I suggest isolating your connection code in just one spot in your application, giving you the ability to manage the connection across potentially multiple uses. If you do this and wrap up database transactions too, you can send the QueryBuilder object into transactions via the extension methods. This is what most of my querying ends up looking like...

	using System.Data;
	using IDbEz;
	using IDbEz.Extensions;
	
	public class MyClass
	{
		private IRunInTransaction _runInTransaction;  // This class calls a passed-in delegate giving it a database transaction which it afterwards commits

		public MyClass( IRunInTransaction runInTransaction )
		{
			_runInTransaction = runInTransaction;
		}


		public IEnumerable<Order> GetOrders( IEnumerable<String> orderNumbers )
		{
			IEnumerable<Order> orders = null;
			_runInTransaction.Execute( dbTrans => orders = GetOrders( orderNumbers, dbTrans );
			return orders;
		}
		
		
		private IEnumerable<Order> GetOrders( IEnumerable<String> orderNumbers, IDbTransaction dbTransaction )
		{
			var query = Ez.QueryBuilder();
			query.Add( @"	select *
			                from customer_orders 
			                where order_number in ({0}) ", orderNumbers );

			IEnumerable<Order> orders = new List<Order>();
			dbTransaction.ExecuteReaderAndRead( query, reader => orders.Add( MapQueryResultsToOrder( reader ) );
			return orders;
		}
		
		
		private Order MapQueryResultsToOrder( IDataReader dataReader )
		{
			// instantiates an Order object and populates its properties with values from the reader
			...
		}
	}
	  
In the above example, the public GetOrders method executes the RunInTransaction process, setting it's orders collection equal to the return from the private GetOrders. That function takes the transaction and utilizes IDbEz's QueryBuilder and an extension method to keep the focus of the function on the query. The ExecuteReaderAndRead() method performs a while ( reader.Read() ) loop, calling the anonymous delegate which adds the mapped order to the locally scoped orders collection.

#Enhanced Error Messages via DbEzException

Special thanks here to my coworker Robert who, seeing that all the database execution traffic was being funnelled through just a couple methods, came up with the idea to add some enhancements to the almost-helpful error messages we were getting from DB2. He added it to our fork of this at work, and I've added it separately here.

In the event of a thrown DbException, IDbEz will catch the DbException, create a DbEzException with the attempted SQL and a list of parameter values in the message and Data collection, and then throw that, which, by the way, inherits from DbException so catching DbException higher up will still work.

This has proven very valuable for me when a database call chokes and the error message doesn't tell me what the query was or what the parameters were. This is especially 
helpful when the SQL is pieced together conditionally, so at any time the actual SQL sent to the database may be different than the time before. Having the SQL right there in the error message, together with a list of what the parameter values were, can help immediately pinpoint the problem.

#Pluggability

IDbEz uses a very primitive IoC container, if I can call it that, but it's set up to allow a consumer to easily replace any given piece. So if we take the above mentioned exception handling, an application could easily replace the default IDbExceptionHandler with it's own implementation that includes, say, logging code. So for the sake of example...

	  public class CustomDbExceptionHandler : IDbExceptionHandler
	  {
	    private IExceptionMessageFormatter _exMsgFormatter; // perhaps we want to use IDbEz's exception message formatting for logging
	    private IExceptionLogger _exLogger;
		
	    public CustomDbExceptionHandler( IExceptionLogger exLogger, IExceptionMessageFormatter exMsgFormatter )
		{
		  _exLogger = exLogger;
		  _exMsgFormatter = exMsgFormatter;
		}
	  
	    // Implement the Handle() method...
	    public void Handle( DbException dbException, String sql, IEnumerable<IParameterStub> parameterStubs )
		{
		  var prettyMsg = _exMsgFormatter.Format( dbException, sql, parameterStubs );
		  _exLogger.LogException( prettyMsg, dbException );
		  // throw dbException back to the app???
		}
	  }
	  
	  ...
	  Ez.ReplaceDbExceptionHandler( () => new CustomDbExceptionHandler( myLogger, Ez.ExceptionMessageFormatter() ) );
	  ...

And to set it back to stock...

	  Ez.ReplaceDbExceptionHandler( null );
	  
#Inspiration

This wasn't created oblivious to ORMs and the better tooling out here. I recommend them if you can use them. Unfortunately, I can't at work. So we were left with lots of repeated plumbing code that I wanted to try and DRY up and simultaneously feed a desire to better learn single-purpose classes, test-driven development, and delegates. Later, I thought I could use it to step into open source, GitHub, and publishing a NuGet package. It was primarily an academic exercise now used in production code, so any feedback for just education alone is still warmly welcome.
