namespace TodoList.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Todoes", new[] { "UserId" });
            DropTable("dbo.Todoes");
        }
    }
}
