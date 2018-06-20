namespace LoymaxTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDatas", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.UserDatas", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDatas", "LastName", c => c.String());
            AlterColumn("dbo.UserDatas", "Name", c => c.String());
        }
    }
}
