namespace GolfCard.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScoreField_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScoreCards", "Shots", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScoreCards", "Shots");
        }
    }
}
