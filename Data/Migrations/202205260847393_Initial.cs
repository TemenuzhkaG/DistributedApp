namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Instructions = c.String(maxLength: 500),
                        PreparationTime = c.Int(nullable: false),
                        CookingTime = c.Int(nullable: false),
                        PortionsCount = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredientRecipes",
                c => new
                    {
                        Ingredient_Id = c.Int(nullable: false),
                        Recipe_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_Id, t.Recipe_Id })
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .Index(t => t.Ingredient_Id)
                .Index(t => t.Recipe_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientRecipes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.IngredientRecipes", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.IngredientRecipes", new[] { "Recipe_Id" });
            DropIndex("dbo.IngredientRecipes", new[] { "Ingredient_Id" });
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropTable("dbo.IngredientRecipes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Recipes");
            DropTable("dbo.Categories");
        }
    }
}
