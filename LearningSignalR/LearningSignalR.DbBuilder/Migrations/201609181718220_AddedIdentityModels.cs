namespace LearningSignalR.DbBuilder.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdentityModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_Roles_Name");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 256),
                        LastName = c.String(nullable: false, maxLength: 256),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 256),
                        SecurityStamp = c.String(nullable: false, maxLength: 256),
                        PhoneNumber = c.String(maxLength: 256),
                        IsPhoneNumberConfirmed = c.Boolean(nullable: false),
                        NoOfFailedLogins = c.Int(nullable: false),
                        IsLockoutEnabled = c.Boolean(nullable: false),
                        LockoutEndingAt = c.DateTime(),
                        IsDisabled = c.Boolean(nullable: false),
                        IsTwoFactorEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "IX_Users_UserName")
                .Index(t => t.Email, unique: true, name: "IX_Users_Email");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(nullable: false, maxLength: 256),
                        ClaimValue = c.String(nullable: false, maxLength: 2048),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.ClaimType }, unique: true, name: "IX_UserClaims_UserId_ClaimType");
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 256),
                        ProviderKey = c.String(nullable: false, maxLength: 2048),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.LoginProvider }, unique: true, name: "IX_UserLogins_UserId_LoginProvider");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserLogins", "IX_UserLogins_UserId_LoginProvider");
            DropIndex("dbo.UserClaims", "IX_UserClaims_UserId_ClaimType");
            DropIndex("dbo.Users", "IX_Users_Email");
            DropIndex("dbo.Users", "IX_Users_UserName");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "IX_Roles_Name");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
