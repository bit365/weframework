using System.Collections.Generic;
using System.Data.Entity;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Data
{
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary> 
        /// Executes the given DDL/DML command against the database. 
        /// </summary> 
        /// <param name="sql">The command string. </param> 
        /// <param name="parameters">The parameters to apply to the command string.</param> 
        /// <returns>The result returned by the database after executing the command.</returns> 
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary> 
        ///Creates a raw SQL query that will return elements of the given generic type. 
        ///The type can be any type that has properties that match the names of the columns returned 
        ///from the query, or can be a simple primitive type.  The type does not have to be an 
        ///entity type. The results of this query are never tracked by the context even if the 
        ///type of object returned is an entity type.  Use the <see cref="DbSet{TEntity}.SqlQuery" /> 
        ///method to return entities that are tracked by the context. 
        /// </summary> 
        /// <typeparam name="TElement"> The type of object returned by the query. </typeparam> 
        /// <param name="sql"> The SQL query string. </param> 
        /// <param name="parameters"> The parameters to apply to the SQL query string. </param> 
        /// <returns> 
        ///A object that will execute the query when it is enumerated. 
        /// </returns> 
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

    }
}
