
5. A web application called "Bookshelf" Administer bookcase's Book loans Function
   I�ve implemented the Bookshelf application as if it was a real Enterprise Application, to 
   demonstrate experiences in design/architecture and back-end programming with .NET.

   As you can see I took the freedom of choosing my own boundaries, usually a bit more than the most
   basic solutions.
   The reason for this is to show my capabilities, although I know that more basic solutions could fit 
   the purpose on many occasions.

   Note: update the connection string in the Web.config file and in the TestDatabaseConnectionString.cs file
         for the unit tests. The integration tests use the data created by the CreateDb.sql script

   The DB is normalized, key index and constraint are defined.
   The code deal properly with concurrency and concurrency conflicts.

   The web application can be hot-deployed because does not make use of Session objects and can run
   run in load-balancing on different production web servers.

   The design of the code make it more easy to:
   - replace an external system with a different implementation for example replace the current
     SqlServer persistence implementation with a Windows Azure one
   - make changes to the domain objects and to the applications services because the dependencies are
     limited and organized and every object have a small well defined responsibility
   - configure the system to run on different environments (dev PC, beta for acceptance tests or
     unit tests for manual tests, production, ...) even if more external systems are integrated/added
     because objects instantiation and dependencies injections are organized in the code
   - test the system with unit tests, integration tests and acceptance tests

   Globar error catching and logging need also to be implemented  as well as authentication and authorization.

   HTML and CSS are very basic. And the application have very basic features.
  
