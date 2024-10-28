## Create a local db
Start the `QuickGridTest.Migrations.Executor` to create a local db with a single table (used by the blazor app)

## Reproduce the error
- Start the `QuickGridTestProj` project
- Navigate to `Resource overview`
- When you add or delete a resource, most of the times you get the error
 ```
   An exception occurred while iterating over the results of a query for context type 'QuickGridTest.Data.Contexts.QuickGridDbContext'.
      System.InvalidOperationException: A second operation was started on this context instance before a previous operation completed. This is usually caused by different threads concurrently using the same instance of DbContext. For more information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913.
  ```
  Sometimes, I'm able to add or delete resources. But most of the times the error is thrown
