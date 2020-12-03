namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Completedforeign : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CompletedOrders", "ProductId");
            AddForeignKey("dbo.CompletedOrders", "ProductId", "dbo.Producttables", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompletedOrders", "ProductId", "dbo.Producttables");
            DropIndex("dbo.CompletedOrders", new[] { "ProductId" });
        }
    }
}
