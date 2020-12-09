namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuyRequests", "quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Producttables", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "quantity");
            DropColumn("dbo.Producttables", "Stock");
            DropColumn("dbo.BuyRequests", "quantity");
        }
    }
}
