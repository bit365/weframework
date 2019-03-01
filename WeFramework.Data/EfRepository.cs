using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T:class
    {
        #region Fields

        private readonly IDbContext dbContext;

        private IDbSet<T> dbSet;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Object context</param>
        public EfRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.DbSet.Add(entity);

            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.DbSet.Add(entity);
            }

            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.DbSet.Remove(entity);

            this.dbContext.SaveChanges();

        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.DbSet.Remove(entity);
            }

            this.dbContext.SaveChanges();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get { return this.DbSet; }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get { return this.DbSet.AsNoTracking(); }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> DbSet
        {
            get
            {
                this.dbSet = this.dbSet ?? dbContext.Set<T>();
                return this.dbSet;
            }
        }

        #endregion
    }
}
