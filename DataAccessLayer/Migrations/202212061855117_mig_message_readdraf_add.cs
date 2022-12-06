namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_message_readdraf_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MessageRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "isDraft", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "isDraft");
            DropColumn("dbo.Messages", "MessageRead");
        }
    }
}
