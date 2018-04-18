namespace GolfCard.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScoreCards",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Yards = c.Int(nullable: false),
                        Par = c.Int(nullable: false),
                        Player_1 = c.Int(nullable: false),
                        Player_2 = c.Int(nullable: false),
                        Player_3 = c.Int(nullable: false),
                        Player_4 = c.Int(nullable: false),
                        Player_5 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScoreCards");
        }
    }
}
