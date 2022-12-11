namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_writerrole1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "RoleId", c => c.Int());
            CreateIndex("dbo.Writers", "RoleId");
            AddForeignKey("dbo.Writers", "RoleId", "dbo.Roles", "RoleId");
            DropColumn("dbo.Writers", "WriterRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Writers", "WriterRole", c => c.String(maxLength: 1));
            DropForeignKey("dbo.Writers", "RoleId", "dbo.Roles");
            DropIndex("dbo.Writers", new[] { "RoleId" });
            DropColumn("dbo.Writers", "RoleId");
        }
    }
}
