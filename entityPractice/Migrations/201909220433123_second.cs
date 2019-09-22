namespace entityPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Image", c => c.String());
            AddColumn("dbo.Customers", "Email", c => c.String());
            AddColumn("dbo.Customers", "Phone", c => c.String());
            AddColumn("dbo.Customers", "_token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "_token");
            DropColumn("dbo.Customers", "Phone");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "Image");
        }
    }
}
