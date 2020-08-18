namespace PhoneBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.State", t => t.StateID)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Country", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(),
                        CityID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        PinCode = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.State", t => t.StateID)
                .ForeignKey("dbo.Country", t => t.CountryID)
                .ForeignKey("dbo.Person", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CityID)
                .Index(t => t.StateID)
                .Index(t => t.CountryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "ID", "dbo.Person");
            DropForeignKey("dbo.Address", "CountryID", "dbo.Country");
            DropForeignKey("dbo.Address", "StateID", "dbo.State");
            DropForeignKey("dbo.Address", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "StateID", "dbo.State");
            DropForeignKey("dbo.State", "CountryID", "dbo.Country");
            DropIndex("dbo.Address", new[] { "CountryID" });
            DropIndex("dbo.Address", new[] { "StateID" });
            DropIndex("dbo.Address", new[] { "CityID" });
            DropIndex("dbo.Address", new[] { "ID" });
            DropIndex("dbo.State", new[] { "CountryID" });
            DropIndex("dbo.City", new[] { "StateID" });
            DropTable("dbo.Address");
            DropTable("dbo.Person");
            DropTable("dbo.Country");
            DropTable("dbo.State");
            DropTable("dbo.City");
        }
    }
}
