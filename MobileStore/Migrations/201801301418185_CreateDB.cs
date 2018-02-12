namespace MobileStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblroles",
                c => new
                    {
                        roleId = c.Int(nullable: false, identity: true),
                        rolename = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.roleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        IsEmailVerified = c.Boolean(nullable: false),
                        roleId = c.Int(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.tblroles", t => t.roleId, cascadeDelete: true)
                .Index(t => t.roleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "roleId", "dbo.tblroles");
            DropIndex("dbo.Users", new[] { "roleId" });
            DropTable("dbo.Users");
            DropTable("dbo.tblroles");
        }
    }
}
