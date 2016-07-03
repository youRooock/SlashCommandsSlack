using Common;
using Connector;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace SlashCommandsService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IContiniousIntegrationCaller, TeamCityCaller>();
            container.RegisterType<ICommandManager, CommandManager>(
                new InjectionConstructor(
                    new ResolvedParameter<IContiniousIntegrationCaller>()));
            container.RegisterType<ICommandHandler, CommandHandler>(
                new InjectionConstructor(
                    new ResolvedParameter<ICommandManager>()));
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}