namespace InventoryApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinventorymodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientInformations", "ClientCode", c => c.String());
            AlterColumn("dbo.ClientInformations", "ClientName", c => c.String());
            AlterColumn("dbo.ClientInformations", "ContactPerson", c => c.String());
            AlterColumn("dbo.ClientInformations", "Address", c => c.String());
            AlterColumn("dbo.ClientInformations", "Email", c => c.String());
            AlterColumn("dbo.ClientInformations", "MobileNumber", c => c.String());
            AlterColumn("dbo.ItemCategories", "ItemCategoryCode", c => c.String());
            AlterColumn("dbo.ItemCategories", "ItemCategoryName", c => c.String());
            AlterColumn("dbo.ItemInformations", "ItemCode", c => c.String());
            AlterColumn("dbo.ItemInformations", "ItemName", c => c.String());
            AlterColumn("dbo.SupplierInformations", "SupplierCode", c => c.String());
            AlterColumn("dbo.SupplierInformations", "SupplierName", c => c.String());
            AlterColumn("dbo.SupplierInformations", "ContactPerson", c => c.String());
            AlterColumn("dbo.SupplierInformations", "Address", c => c.String());
            AlterColumn("dbo.SupplierInformations", "Email", c => c.String());
            AlterColumn("dbo.SupplierInformations", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplierInformations", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.SupplierInformations", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.SupplierInformations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.SupplierInformations", "ContactPerson", c => c.String(nullable: false));
            AlterColumn("dbo.SupplierInformations", "SupplierName", c => c.String(nullable: false));
            AlterColumn("dbo.SupplierInformations", "SupplierCode", c => c.String(nullable: false));
            AlterColumn("dbo.ItemInformations", "ItemName", c => c.String(nullable: false));
            AlterColumn("dbo.ItemInformations", "ItemCode", c => c.String(nullable: false));
            AlterColumn("dbo.ItemCategories", "ItemCategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.ItemCategories", "ItemCategoryCode", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "ContactPerson", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "ClientName", c => c.String(nullable: false));
            AlterColumn("dbo.ClientInformations", "ClientCode", c => c.String(nullable: false));
        }
    }
}
