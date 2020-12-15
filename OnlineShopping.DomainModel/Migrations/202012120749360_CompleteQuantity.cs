namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompleteQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedOrders", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompletedOrders", "quantity");
        }
    }
}
