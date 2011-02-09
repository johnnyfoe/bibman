using System.Web.Mvc;
using System.Web.Routing;
using BibtexEntryManager.Data;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;

namespace BibtexEntryManager
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            DataPersistence.Prepare(); // sets up NHibernate mappings and database session.
            //InsertDefaultData();
        }

        private static void InsertDefaultData()
        {
            // Adds some default instances to each table.
            DataPersistence.GetSession().Save(ObjectBuilder.BuildDefaultPublication());

        }
    }
}