namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Producttables", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductID", "dbo.Producttables");
            DropIndex("dbo.Carts", new[] { "ProductID" });
            DropTable("dbo.Carts");
        }
    }
}
