﻿using System.Linq;
using Autofac;
using Caliburn.Micro;
using Chicken4WP8.Models.Setting;
using Chicken4WP8.Services.Implemention;
using Chicken4WP8.Services.Interface;

namespace Chicken4WP8
{
    public class ChickenBootstrapper : AutofacBootstrapper
    {
        public ChickenBootstrapper()
        {
            base.Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            //register language helper
            builder.RegisterType<LanguageHelper>()
                .As<ILanguageHelper>()
                .SingleInstance();

            //register progress service
            builder.RegisterInstance(new ProgressService(RootFrame))
                .As<IProgressService>()
                .PropertiesAutowired()
                .SingleInstance();

            var assembiles = AssemblySource.Instance.ToArray();
            //register services
            builder.RegisterAssemblyTypes(assembiles)
                // must be a type which name ends with service
                .Where(type => type.Name.EndsWith("Service"))
                // namespace ends with services implemention
                    .Where(type => type.Namespace.EndsWith("Implemention"))
                    .Except<ProgressService>()
                // registered as interface
                    .AsImplementedInterfaces()
                //create new one
                    .InstancePerDependency()
                //auto inject property
                    .PropertiesAutowired();

            //register controllers
            builder.RegisterAssemblyTypes(assembiles)
                //must be a type which name ends with controller
                .Where(type => type.Name.EndsWith("Controller"))
                //starts with base
                .Where(type => type.Name.StartsWith("Base"))
                .AsImplementedInterfaces()
                .WithMetadata<OAuthTypeMetadata>(
                meta => meta.For(
                    m => m.OAuthType, OAuthSettingType.BaseOAuth));
            builder.RegisterAssemblyTypes(assembiles)
                //must be a type which name ends with controller
                .Where(type => type.Name.EndsWith("Controller"))
                //starts with base
                .Where(type => type.Name.StartsWith("Twip"))
                .AsImplementedInterfaces()
                .WithMetadata<OAuthTypeMetadata>(
                meta => meta.For(
                    m => m.OAuthType, OAuthSettingType.TwipOAuth));
        }
    }
}
