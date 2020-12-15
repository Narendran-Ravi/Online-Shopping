namespace OnlineShopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producttables", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producttables", "quantity");
        }
    }
}
