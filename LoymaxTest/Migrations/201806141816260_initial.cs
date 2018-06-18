namespace LoymaxTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatStates",
                c => new
                    {
                        ChatId = c.Long(nullable: false),
                        UserId = c.Int(nullable: false),
                        StateGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChatId, t.UserId });
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        MidName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserDatas");
            DropTable("dbo.ChatStates");
        }
    }
}
