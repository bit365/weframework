namespace WeFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WeFramework.Core.Domain.Navigates;
    using WeFramework.Core.Domain.Security;
    using WeFramework.Core.Domain.Users;

    internal sealed class Configuration : DbMigrationsConfiguration<WeFramework.Data.CustomDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WeFramework.Data.CustomDbContext context)
        {
    
        }
    }
}
