namespace QuotationsApp.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuotationsApp.Models.QuotationsAppCSISDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "QuotationsApp.Models.QuotationsAppCSISDBContext";
        }

        protected override void Seed(QuotationsApp.Models.QuotationsAppCSISDBContext context)
        {
            context.Categories.AddOrUpdate(x => x.CategoryID,
                new Category() { CategoryID = 1, Name = "New Category" },
                new Category() { CategoryID = 2, Name = "Wisdom" },
                new Category() { CategoryID = 3, Name = "Political" },
                new Category() { CategoryID = 4, Name = "Life" });
        }
    }
}
