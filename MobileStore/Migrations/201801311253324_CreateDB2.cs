namespace MobileStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "BrandName", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String());
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Title", c => c.String());
            AlterColumn("dbo.Brands", "BrandName", c => c.String());
        }
    }
}
