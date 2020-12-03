namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foreigntable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BuyRequests", "ProductId");
            AddForeignKey("dbo.BuyRequests", "ProductId", "dbo.Producttables", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyRequests", "ProductId", "dbo.Producttables");
            DropIndex("dbo.BuyRequests", new[] { "ProductId" });
        }
    }
}
