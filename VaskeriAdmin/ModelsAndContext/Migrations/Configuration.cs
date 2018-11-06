namespace ModelsAndContext.Migrations
{
    using ModelsAndContext.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.VaskeriContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.VaskeriContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.WasherServices.AddOrUpdate(s => s.Name, new WasherService() { Name = "Vaskeri Tomsen", DaytimeOnly = true });
        }
    }
}
