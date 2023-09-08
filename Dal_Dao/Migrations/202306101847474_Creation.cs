namespace Dal_Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emprunteurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        AnneeNaissance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Livres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        Auteur = c.String(),
                        Annee = c.Int(nullable: false),
                        Emprunteur_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Emprunteurs", t => t.Emprunteur_Id)
                .Index(t => t.Emprunteur_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livres", "Emprunteur_Id", "dbo.Emprunteurs");
            DropIndex("dbo.Livres", new[] { "Emprunteur_Id" });
            DropTable("dbo.Livres");
            DropTable("dbo.Emprunteurs");
        }
    }
}
