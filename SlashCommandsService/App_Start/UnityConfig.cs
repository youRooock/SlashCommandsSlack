using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Common;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;

namespace SlashCommandsService.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICommandManager, CommandManager>();
            container.RegisterType<ICommandHandler, CommandHandler>(new InjectionConstructor(new ResolvedParameter<ICommandManager>()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
