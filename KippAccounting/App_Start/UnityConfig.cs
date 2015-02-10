using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using KippAccounting.DataAccess;
using System.Configuration;

namespace KippAccounting
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAccountingQueryHandler, AccountingSqlQueryHandler>();
            var dbConnectionString = ConfigurationManager.ConnectionStrings["KippAccountingDb"];
            container.RegisterType<IConfigurationSettings, KippAccounting.DataAccess.ConfigurationSettings>(new InjectionConstructor(dbConnectionString.Name));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}