using Bll_Service.Service;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UILayer_Presentation.Controllers
{
    public class LivreController : Controller
    {
        // IoC
        private readonly ILivreService ls;

        public LivreController(ILivreService lss)
        {
            this.ls = lss;
        }



        // 1- Liste livre
        public ActionResult ListeLivre()
        {
            List<Livre> listeLivres = ls.GetAllLivres();
            return View(listeLivres);
        }



        // 2- Ajout Livre
        [HttpGet]
        public ActionResult AjoutLivre()
        {
            return View();
        }


        // La methode pour traiter le formulaire d'ajout
        [HttpPost]
        public ActionResult AjoutLivre(Livre liv)
        {
            // Valider les champs
            if (string.IsNullOrEmpty(liv.Auteur)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (personne) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Auteur", "Le champs de l'auteur est obligatoire");
            }

            if (string.IsNullOrEmpty(liv.Titre)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (livre) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Titre", "Le champs du titre est obligatoire");
            }

            // Valider le champ à la propriété Age de Personne
            if (string.IsNullOrEmpty(Convert.ToString(liv.Annee)) || liv.Annee < 1000 ) 
            { // string.IsNullOrEmpty(Convert.ToString(liv.Annee)) || Convert.ToString(liv.Annee).Length < 4 || Convert.ToString(liv.Annee).Length > 4) // 

                ModelState.AddModelError("Année", "L'année est obligatoire");
            }

            // Verifier l'état du model (si les données aisies sont valides ou pas)
            if (ModelState.IsValid)
            {
                int verif = ls.AjoutLivre(liv);
                if (verif != 0)
                {
                    // Si l'ajout est bien passé, on fait une redirection vers la methode d'action ListeEmrpunteur pour afficher la nouvelle liste des emprunteurs
                    return RedirectToAction("ListeLivre");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View(); // Et reste sur la vue

                }
            }

            return View(); // Et reste sur la vue


        }




        // 3. La fonctionnalité rechercher livre par son Id
        // Methode d'action pour afficher le formulaire de recherche
        public ActionResult FindLivre()
        {
            return View();
        }


        // Une methode dd'action pour traiter le formulaire de recherche
        [HttpPost]
        public ActionResult FindLivre(int id) // Attention: Dans ce parametre, faut donner le même nom que le parametre de la requete id (name de la balise). Sinon il ne va pas le reconnaitre
        {
            // Appel de la méthode service pour rechercher le livre par son id
            Livre efind = ls.GetLivresById(id);
            if (efind != null)
            {
                return View(efind);// IL retourne la vue avec eFind dans la table
            }
            else
            {
                ViewBag.Msg = "Le livre n'existe pas";
                return View();
            }

        }







        // 4 Fonctionnalité Modifier
        // Methode d'action pour afficher le formulaire de modification

        public ActionResult ModifLivre()
        {
            return View();
        }


        // Methode d'action pour traiter le formulaire de modification
        [HttpPost]
        public ActionResult ModifLivre(Livre liv)
        {
            int verif = ls.ModifLivre(liv);
            if (verif != 0)
            {
                // Redirection vers l'action Index afin d'afficher la nouvelle liste
                return RedirectToAction("ListeLivre");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }


        public ActionResult ModifLivreEmprunteur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifLivreEmprunteur(Livre liv)
        {
            int verif = ls.ModifLivre(liv);
            if (verif != 0)
            {
                // Redirection vers l'action Index afin d'afficher la nouvelle liste
                return RedirectToAction("ListeLivresEmprunteur");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }



        // Operateur par lien
        // En cliquant sur "modifier" sur la ligne du livre concerné dans la liste, ça va faire appel à cette méthode d'action.
        // Cette méthode va trouver le livre à travers le id recuperé, et l'envoyer à la methode d'action get de modifLivre, pour afficher le formulaire (sans param).
        // Lors de la validation du formulaire, ca va faire appel à la méthode post de modifLivre
        public ActionResult ModifLivLink(int id)
        {
            // Chercher toutes les infos du livre et les passer à la vue du model
            var eFind = ls.GetLivresById(id);

            // Faire une redirection vers l'action Update de ce controller etudiant pour qu'il affiche les infos du livre sur la vue update. Et là on peut faire les modifications qu'on souhaite.
            return View("ModifLivre", eFind);
        }


        public ActionResult ModifLivLink2(int id)
        {
            // Chercher toutes les infos du livre et les passer à la vue du model
            var eFind = ls.GetLivresById(id);

            // Faire une redirection vers l'action Update de ce controller etudiant pour qu'il affiche les infos du livre sur la vue update. Et là on peut faire les modifications qu'on souhaite.
            return View("ModifLivreEmprunteur", eFind);
        }





        // 5. Fonctionnalité supprimer
        // Operateur par lien: Les operateurs de lien sont des get
        // Ceci n'a pas besoin de post.

        // En cliquant sur "supprimer" sur la ligne du livre concerné dans la liste, ça va faire appel à cette méthode d'action.
        // Cette méthode va supprimer directement le livre, et actualiser la liste en retournant l'action sur Liste des livres, qui va actualiser la liste via GetAllLivres


        [HttpGet]
        public ActionResult SuppLivLink(int id)
        {
            // appeler la methode supprimer de service
            int verif = ls.SuppLivre(id);

            // Faire une redirection vers l'action index
            return RedirectToAction("ListeLivre");
        }


        [HttpGet]
        public ActionResult SuppLivLink2(int id)
        {
            // appeler la methode supprimer de service
            int verif = ls.SuppLivre(id);

            // Faire une redirection vers l'action index
            return RedirectToAction("ListeLivresEmprunteur");
        }




        // 3. La fonctionnalité rechercher livre par IdEmprunteur
        // Methode d'action pour afficher le formulaire de recherche
        public ActionResult FindLivresEmprunteur()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ListeLivresEmprunteur(int id) // Attention: Dans ce parametre, faut donner le même nom que le parametre de la requete id (name de la balise). Sinon il ne va pas le reconnaitre
        {
            // Appel de la méthode service pour rechercher le livre par son id
            List<Livre> listeLivre = ls.GetLivresByIdEmprunteur(id);
            if (listeLivre != null)
            {
                return View(listeLivre);// IL retourne la vue avec listeLivre dans la table
            }
            else
            {
                ViewBag.Msg = "L'Id de l'emprunteur n'existe pas";
                return View();
            }

        }

        public ActionResult ListeLivresEmprunteur() // Attention: Dans ce parametre, faut donner le même nom que le parametre de la requete id (name de la balise). Sinon il ne va pas le reconnaitre
        {
            Emprunteur emp = (Emprunteur)Session["emprunteurSession2"];

            // Appel de la méthode service pour rechercher le livre par son id
            List<Livre> listeLivre = ls.GetLivresByIdEmprunteur(emp.Id);
            if (listeLivre != null)
            {
                return View(listeLivre);// IL retourne la vue avec listeLivre dans la table
            }
            else
            {
                ViewBag.Msg = "L'Id de l'emprunteur n'existe pas";
                return View();
            }

        }





        [HttpGet]
        public ActionResult AjoutLivreEmprunteur()
        {
            var emp = (Emprunteur)Session["emprunteurSession1"];
            Livre liv = new Livre();
            liv.Id = emp.Id;
            return View(liv);
        }


        // La methode pour traiter le formulaire d'ajout
        [HttpPost]
        public ActionResult AjoutLivreEmprunteur(Livre liv)
        {
            // Valider les champs
            if (string.IsNullOrEmpty(liv.Auteur)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (personne) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Auteur", "Le champs de l'auteur est obligatoire");
            }

            if (string.IsNullOrEmpty(liv.Titre)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (personne) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Titre", "Le champs du titre est obligatoire");
            }

            // Valider le champ à la propriété Age de Personne
            if (string.IsNullOrEmpty(Convert.ToString(liv.Annee)) || Convert.ToString(liv.Annee).Length < 4 || Convert.ToString(liv.Annee).Length > 4) // 
            {
                ModelState.AddModelError("Année", "L'année est obligatoire");
            }

            // Verifier l'état du model (si les données aisies sont valides ou pas)
            if (ModelState.IsValid)
            {
                int verif = ls.AjoutLivre(liv);
                if (verif != 0)
                {
                    
                    // Si l'ajout est bien passé, on fait une redirection vers la methode d'action AjoutEmrpunteur pour afficher la nouvelle liste des emprunteurs
                    return RedirectToAction("ListeLivreEmp", "Emprunteur");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View(); // Et reste sur la vue

                }
            }

            return View(); // Et reste sur la vue


        }

    }
}