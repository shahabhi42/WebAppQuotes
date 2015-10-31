namespace QuotationsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotations", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotations", "Date", c => c.DateTime(nullable: false));
        }
    }
}
