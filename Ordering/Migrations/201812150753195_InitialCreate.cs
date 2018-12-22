namespace Ordering.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        dishId = c.Int(nullable: false, identity: true),
                        dishName = c.String(),
                        dishPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dishIntroduction = c.String(),
                        dishPhoto = c.String(),
                    })
                .PrimaryKey(t => t.dishId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        orderItemId = c.Int(nullable: false, identity: true),
                        itemId = c.Int(nullable: false),
                        orderId = c.Int(nullable: false),
                        num = c.Int(nullable: false),
                        title = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalFree = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.orderItemId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        state = c.Int(nullable: false),
                        creatTime = c.DateTime(nullable: false),
                        endTime = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.orderId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserAccount = c.Int(nullable: false),
                        UserName = c.String(),
                        UserPassword = c.String(),
                        CreateData = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Dishes");
        }
    }
}
