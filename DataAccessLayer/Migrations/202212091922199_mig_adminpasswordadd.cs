namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_adminpasswordadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminPassword", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AdminPassword");
        }
    }
}
