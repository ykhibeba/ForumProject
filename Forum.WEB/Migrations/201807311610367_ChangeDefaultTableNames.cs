namespace Forum.WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDefaultTableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "Role");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRole");
            RenameTable(name: "dbo.AspNetUsers", newName: "User");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "Claim");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "Login");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Login", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.Claim", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.User", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRole", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Role", newName: "AspNetRoles");
        }
    }
}
