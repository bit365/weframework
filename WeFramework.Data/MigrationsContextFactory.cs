using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Data;

namespace WeFramework.Data
{
    public class MigrationsContextFactory : IDbContextFactory<CustomDbContext>
    {
        public CustomDbContext Create()
        {
            return new CustomDbContext("firstConnectionString");
        }
    }
}
