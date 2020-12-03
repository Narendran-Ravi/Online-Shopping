namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompletedOrders",
                c => new
                    {
                        CompletedOrderId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CompletedOrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompletedOrders");
        }
    }
}
