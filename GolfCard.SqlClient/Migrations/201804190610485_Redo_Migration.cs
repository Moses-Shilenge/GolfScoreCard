namespace GolfCard.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Redo_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IN = c.Int(nullable: false),
                        OUT = c.Int(nullable: false),
                        TOT = c.Int(nullable: false),
                        HCP = c.Int(nullable: false),
                        NET = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Player_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shots",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Shots = c.Int(nullable: false),
                        Tee_Id = c.Guid(),
                        Game_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tees", t => t.Tee_Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Tee_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Tees",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Yards = c.Int(nullable: false),
                        Par = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shots", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Shots", "Tee_Id", "dbo.Tees");
            DropForeignKey("dbo.Games", "Player_Id", "dbo.Players");
            DropIndex("dbo.Shots", new[] { "Game_Id" });
            DropIndex("dbo.Shots", new[] { "Tee_Id" });
            DropIndex("dbo.Games", new[] { "Player_Id" });
            DropTable("dbo.Tees");
            DropTable("dbo.Shots");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
        }
    }
}
