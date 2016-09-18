namespace LearningSignalR.DbBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCodeToMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Code", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Code");
        }
    }
}
