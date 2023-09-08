using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Dao.Data
{
    public class ApplicationDbContext : DbContext
    {
        // 1 Declaration des propriétés DbSet pour les Entities persistantes (celles qui seront gérées par le context EF)

        public DbSet<Emprunteur> Emprunteurs { get; set; }
        public DbSet<Livre> Livres { get; set; }



        //2  Cette fois ci on est obligé de passer par le constructeur au lieu de Onconfiguring, car c'est lui qui va recperer la chaine de connexion declarée dans WebConfig
        public ApplicationDbContext() : base("BiblioChaineConnexion") // On lui passe le nom de la chaine de connexion définie dans WebConfig
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emprunteur>().HasMany(x => x.ListeLivre) // Chaque emprunteur peut avoir plusieurs livres
                                             .WithOptional(x => x.Emprunteur); // Chaque livre a un emprunteur
                                                                               // Ici il ny a pas de WithOne dans EF (# EFCore)
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
