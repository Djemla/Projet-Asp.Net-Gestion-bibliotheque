using Bll_Service.Service;
using Dal_Dao.Data;
using Dal_Dao.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace UILayer_Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // 1 Configurer le conteneur d'injection de dependance Unity

            var container = new UnityContainer();

            // 2 Enregistrer les composants du container (Ne pas oublier de créer d'abord les classes Dal et Service avec leurs interfaces)
            // Lui specifier quels sont les objets qu'il doit instancier

                //A-  DbContext
                container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());

                //B-   DAL
                container.RegisterType<IEmprunteurRepository, EmprunteurRepository>();
                container.RegisterType<ILivreRepository, LivreRepository>();

                // C- BLL
                container.RegisterType<IEmprunteurService, EmprunteurService>();
                container.RegisterType<ILivreService, LivreService>();

            // 3 Choisir le conteneur Unity comme le conteneur d'injection de dépendance de l'ASP.net.MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
       
        }
    }
}
