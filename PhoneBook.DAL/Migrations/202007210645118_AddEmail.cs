namespace PhoneBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "Email");
        }
    }
}
