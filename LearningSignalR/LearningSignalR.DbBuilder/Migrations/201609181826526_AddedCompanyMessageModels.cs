namespace LearningSignalR.DbBuilder.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyMessageModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_Companies_Name");
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CompanyId = c.Long(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 256),
                        Body = c.String(),
                        SentByUserId = c.Long(nullable: false),
                        SentAt = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ReatAt = c.DateTime(),
                        ReadByUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReadByUserId)
                .ForeignKey("dbo.Users", t => t.SentByUserId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.SentByUserId)
                .Index(t => t.ReadByUserId);
            
            AddColumn("dbo.Users", "CompanyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Users", "CompanyId");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Messages", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Messages", "SentByUserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReadByUserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Messages", new[] { "ReadByUserId" });
            DropIndex("dbo.Messages", new[] { "SentByUserId" });
            DropIndex("dbo.Messages", new[] { "CompanyId" });
            DropIndex("dbo.Companies", "IX_Companies_Name");
            DropColumn("dbo.Users", "CompanyId");
            DropTable("dbo.Messages");
            DropTable("dbo.Companies");
        }
    }
}
