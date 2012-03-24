## IDbEz

IDbEz is a set of helper classes and extension methods created for use with the IDb*-related interfaces in System.Data.

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