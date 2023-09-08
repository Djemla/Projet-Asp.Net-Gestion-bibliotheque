using Bll_Service.Service;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UILayer_Presentation.Controllers
{
    public class EmprunteurController : Controller
    {
        // IoC
        private readonly IEmprunteurService es;

        public EmprunteurController(IEmprunteurService ess)
        {
            this.es = ess;
        }



        // 1- Afficher la liste
        public ActionResult ListeEmrpunteur()
        {
            List<Emprunteur> listeEmprunteurs =  es.GetAllEmprunteurs();
            
            return View(listeEmprunteurs); // On veut passer cette liste à la vue, pourqu'elle soit affichée à la vue
                                           // Passer le model MVC (la liste des etudiants) à la vue en utilisant le mecanisme des vues fortement typées
        }




        // 2- Ajouter Emprunteur
        // La methode d'action pour afficher le formulaire d'ajout
        [HttpGet]
        public ActionResult AjoutEmprunteur()
        {
            return View();
        }

        // La methode pour traiter le formulaire d'ajout
        [HttpPost]
        public ActionResult AjoutEmprunteur(Emprunteur emp)
        {
            // Valider les champs
            if (string.IsNullOrEmpty(emp.Nom)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (personne) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Nom", "Le champs du nom est obligatoire");
            }

            if (string.IsNullOrEmpty(emp.Prenom)) // IsNullOrEmpty est une methode static d'après la doc officielle, et conc faut l'appeller par sa classe (qui est string)
            {
                // Ajouter un message d'erreur dans l'objet ModelState en specifiant la propriété concernée du Model (emprunteur) (Accepte 2 proppriétés en entrée)
                ModelState.AddModelError("Prenom", "Le champs du prenom est obligatoire");
            }


            if (string.IsNullOrEmpty(Convert.ToString(emp.AnneeNaissance)) | emp.AnneeNaissance < 1000 ) 
            { // (string.IsNullOrEmpty(Convert.ToString(emp.AnneeNaissance)) || Convert.ToString(emp.AnneeNaissance).Length < 4 || Convert.ToString(emp.AnneeNaissance).Length > 4) // 

                ModelState.AddModelError("AnnéeNaissance", "L'année de naissance est obligatoire");
            }

            // Verifier l'état du model (si les données aisies sont valides ou pas)
            if (ModelState.IsValid)
            {
                int verif = es.AjoutEmprunteur(emp);
                if (verif != 0)
                {
                    // Si l'ajout est bien passé, on fait une redirection vers la methode d'action ListeEmrpunteur pour afficher la nouvelle liste des emprunteurs
                    return RedirectToAction("ListeEmrpunteur");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View(); // Et reste sur la vue

                }
            }
            
            return View(); // Et reste sur la vue


        }




        // 3. La fonctionnalité rechercher par son Id
        // Methode d'action pour afficher le formulaire de recherche
        public ActionResult FindEmprunteur()
        {
            return View();
        }


        // Une methode dd'action pour traiter le formulaire de recherche
        [HttpPost]
        public ActionResult FindEmprunteur(int id) // Attention: Dans ce parametre, faut donner le même nom que le parametre de la requete id (name de la balise). Sinon il ne va pas le reconnaitre
        {
            // Appel de la méthode service pour rechercher l'emprunteur par son id
            Emprunteur efind = es.GetEmprunteurById(id);
            if (efind != null)
            {
                return View(efind);// IL retourne la vue avec eFind dans la table
            }
            else
            {
                ViewBag.Msg = "L'emprunteur n'existe pas";
                return View();
            }

        }



    /*    // 4. Fonctionnalité supprimer
        // Une methode action pour afficher le formulaire de suppression
        public ActionResult SuppEmprunteur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SuppEmprunteur(int id) // Attention: Dans ce parametre, faut donner le même nom que le parametre de la requete id (name de la balise). Sinon il ne va pas le reconnaitre
        {
            int verif = es.SuppEmprunteur(id);

            if (verif != 0)
            {
                // Redirection vers l'action Index afin d'afficher la nouvelle liste
                return RedirectToAction("ListeEmrpunteur");
            }
            else
            {
                ViewBag.Msg = "La suppression a échouée !!!";
                return View();
            }

        } */




        // 4 Fonctionnalité Modifier
        // Methode d'action pour afficher le formulaire de modification

        public ActionResult ModifEmprunteur()
        {
            return View();
        }


       // Methode d'action pour traiter le formulaire de modification
        [HttpPost]
        public ActionResult ModifEmprunteur(Emprunteur emp)
        {
            int verif = es.ModifEmprunteur(emp);
            if (verif != 0)
            {
                // Redirection vers l'action Index afin d'afficher la nouvelle liste
                return RedirectToAction("ListeEmrpunteur");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }



        // Operateur par lien
        // En cliquant sur "modifier" sur la ligne de l'emprunteur concerné daans la liste, ça va faire appel à cette méthode d'action.
        // Cette méthode va trouver l'emprunteur à travers le id recuperer, et l'envoyer au à la methode d'action get de modifEmprunteur, pour afficher le formulaire (sans param).
        // Lors de la validation du formulaire, ca va faire appel à la méthode post de modifEmprunteur
        public ActionResult ModifEmpLink(int id)
        {
            // Chercher toutes les infos de l'étudiant et les passer à la vue du model
            var eFind = es.GetEmprunteurById(id);

            // Faire une redirection vers l'action Update de ce controller etudiant pour qu'il affiche les infos de l'etudiant sur la vue update. Et là on peut faire les modifications qu'on souhaite.
            return View("ModifEmprunteur", eFind);
        }




        // 5. Fonctionnalité supprimer
        // Operateur par lien: Les operateurs de lien sont des get
        // Ceci n'a pas besoin de post.

        // En cliquant sur "supprimer" sur la ligne de l'emprunteur concerné dans la liste, ça va faire appel à cette méthode d'action.
        // Cette méthode va supprimer directement l'emprunteur, et actualiser la liste en retournant l'action sur Liste d'emprunteur, qui va actualiser la liste via GetAllEmp


        [HttpGet]
        public ActionResult SuppEmpLink(int id)
        {
            // appeler la methode supprimer de service
            int verif = es.SuppEmprunteur(id);

            // Faire une redirection vers l'action index
            return RedirectToAction("ListeEmrpunteur");
        }





        // 6. Liste des livres d'un emprunteur
        public ActionResult FindLivreEmp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListeLivreEmp(int id)
        {
            List<Livre> listeLivresEmp = es.GetAllLivreEmprunteurId(id);

            return View(listeLivresEmp);
        }

        public ActionResult ListeLivreEmp()
        {
            var emp = (Emprunteur)Session["emprunteurSession1"];
            List<Livre> listeLivresEmp = es.GetAllLivreEmprunteurId(emp.Id);

            return View(listeLivresEmp);
        }



        // 6. Ajouter un livre
        // Operateur par lien
        // En cliquant sur "AjoutLivre" sur la ligne de l'emprunteur concerné daans la liste, ça va faire appel à cette méthode d'action.
        // Cette méthode va trouver l'emprunteur à travers le id recuperer, et l'envoyer au à la methode d'action get de modifEmprunteur, pour afficher le formulaire (sans param).
        // Lors de la validation du formulaire, ca va faire appel à la méthode post de modifEmprunteur
        public ActionResult AjoutLivreLink(int id)
        {
            // Chercher toutes les infos de l'étudiant et les passer à la vue du model
            var eFind = es.GetEmprunteurById(id);
            Session["emprunteurSession1"] = eFind;

            // Faire une redirection vers l'action AjoutLivre du controller Livre pour qu'il affiche le formulaire d'ajout d'un livre.
            return RedirectToAction("AjoutLivreEmprunteur", "Livre");
        }



        // Supprimer un livre d'un emprunteur

        public ActionResult SuppLivreLink(int id)
        {
            var eFind = es.GetEmprunteurById(id);
            Session["emprunteurSession2"] = eFind;

            return RedirectToAction("ListeLivresEmprunteur", "Livre"); // Il va afficher la liste des livres, et là on pourra supprimer le livre qu'on souhaite
        }
    }
}