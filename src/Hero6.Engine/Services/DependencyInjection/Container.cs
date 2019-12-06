// <copyright file="Container.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.ModuleController;
using LateStartStudio.Hero6.ModuleController.Campaigns;
using LateStartStudio.Hero6.ModuleController.UserInterfaces;
using LateStartStudio.Hero6.Services.Assets;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.ControllerRepository;
using LateStartStudio.Hero6.Services.DotNetWrappers;
using LateStartStudio.Hero6.Services.Graphics;
using LateStartStudio.Hero6.Services.Logger;
using LateStartStudio.Hero6.Services.PlatformInfo;
using LateStartStudio.Hero6.Services.Settings;
using LateStartStudio.Hero6.Services.UserInterfaces;
using LateStartStudio.Hero6.Services.UserInterfaces.Input.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IGraphicsDeviceService = LateStartStudio.Hero6.Services.Graphics.IGraphicsDeviceService;

namespace LateStartStudio.Hero6.Services.DependencyInjection
{
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
    public static class RegistrationExtensions
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
    {
        /// <summary>
        /// Determine life cycle of injected type given <see cref="InjectableAttribute"/>.
        /// </summary>
        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> WithInstanceScope<TLimit, TScanningActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> registration, Type type)
        {
            return type.GetCustomAttribute<InjectableAttribute>().LifeCycle == LifeCycle.Singleton
                ? registration.SingleInstance()
                : registration.InstancePerDependency();
        }

        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> WithInstanceScope<TLimit, TScanningActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> registration)
        {
            return registration.WithInstanceScope(typeof(TLimit));
        }
    }

    [Injectable]
    public sealed class Container : IContainer, IDisposable
    {
        private readonly ContainerBuilder builder = new ContainerBuilder();
        private readonly Autofac.IContainer container;
        private readonly ILifetimeScope scope;

        public Container(ContentManager content, GraphicsDeviceManager graphics)
        {
            Add<IContainer>(this);
            Add<IPlatformInfo, PlatformInfo.PlatformInfo>();
            Add<IFileWrapper, FileWrapper>();
            Add<IDirectoryWrapper, DirectoryWrapper>();
            Add<IGameSettings, GameSettings>();
            Add<IUserSettings, UserSettings>();
            Add<ILoggerCore, LoggerCore>();
            Add<ILogger, Logger.Logger>();
            Add<IMouseCore, MouseCore>();
            Add<IMouse, Mouse>();
            Add<IUserInterfaces, MonoGameUserInterfaces>();
            Add<ICampaigns, MonoGameCampaigns>();
            Add<IControllerRepository, ControllerRepositoryProvider>();

            // MonoGame services
            Add<IAssetsRepository, AssetsRepository>(("assets", content));
            Add<IGraphicsDeviceService, GraphicsDeviceService>(("graphics", graphics));
            Add<IRendererService, RendererService>(("spriteBatch", new SpriteBatch(graphics.GraphicsDevice)));

            AddFromAssemblies();

            container = builder.Build();
            scope = container.BeginLifetimeScope();
        }

        public T Get<T>(params (string name, object instance)[] args) => scope
            .Resolve<T>(args.Select(a => new NamedParameter(a.name, a.instance)));

        public T Get<T>(Type type, params (string name, object instance)[] args) => (T)scope
            .Resolve(type, args.Select(a => new NamedParameter(a.name, a.instance)));

        public void Dispose()
        {
            scope?.Dispose();
            container?.Dispose();
        }

        private void Add<T>(T instance) where T : class
        {
            Add<T>(instance.GetType(), () => builder
                .RegisterInstance(instance)
                .As<T>()
                .WithInstanceScope(instance.GetType()));
        }

        private void Add<TService, TProvider>(params (string name, object instance)[] args) where TProvider : class
        {
            var type = typeof(TProvider);
            Add<TProvider>(type, () => builder
                .RegisterType<TProvider>()
                .As<TService>()
                .WithInstanceScope(type)
                .WithParameters(args.Select(a => new NamedParameter(a.name, a.instance))));
        }

        private void AddFromAssemblies()
        {
            var assemblies = Directory
                .GetFiles(Directory.GetCurrentDirectory())
                .Select(f => Path.GetFileName(f))
                .Where(f => f.StartsWith("Hero6.Campaigns.") || f.StartsWith("Hero6.UserInterfaces."))
                .Where(f => f.EndsWith(".dll"))
                .Select(f => Assembly.LoadFrom(f))
                .Concat(new[] { Assembly.GetExecutingAssembly() })
                .ToArray();

            // Gets all singleton modules/controllers dynamically from assemblies individually
            builder
                .RegisterAssemblyTypes(assemblies)
                .PublicOnly()
                .Where(a => a.GetInterfaces().Any(i => i == typeof(IModule)) || a.GetInterfaces().Any(i => i == typeof(IController)))
                .Where(a => !a.GetCustomAttribute<InjectableAttribute>().IgnoreOnInit)
                .Where(a => a.GetCustomAttribute<InjectableAttribute>().LifeCycle == LifeCycle.Singleton)
                .AsSelf()
                .SingleInstance()
                .AsImplementedInterfaces();

            // Gets all transient modules/controllers dynamically from assemblies individually
            builder
                .RegisterAssemblyTypes(assemblies)
                .PublicOnly()
                .Where(a => a.GetInterfaces().Any(i => i == typeof(IModule)) || a.GetInterfaces().Any(i => i == typeof(IController)))
                .Where(a => !a.GetCustomAttribute<InjectableAttribute>().IgnoreOnInit)
                .Where(a => a.GetCustomAttribute<InjectableAttribute>().LifeCycle == LifeCycle.Transient)
                .AsSelf()
                .InstancePerDependency();

            // Gets all user interface modules and stores them to a IEnumerable
            builder
                .RegisterAssemblyTypes(assemblies)
                .PublicOnly()
                .Where(a => a.GetInterfaces().Any(i => i == typeof(IUserInterfaceModule)))
                .Where(a => !a.GetCustomAttribute<InjectableAttribute>().IgnoreOnInit)
                .As<IUserInterfaceModule>();

            // Gets all campaign modules and stores them to a IEnumerable
            builder
                .RegisterAssemblyTypes(assemblies)
                .PublicOnly()
                .Where(a => a.GetInterfaces().Any(i => i == typeof(ICampaignModule)))
                .Where(a => !a.GetCustomAttribute<InjectableAttribute>().IgnoreOnInit)
                .As<ICampaignModule>();
        }

        private void Add<T>(Type type, Action add)
        {
            if (!type.FullName.StartsWith("LateStartStudio"))
            {
                throw new InvalidOperationException($"Type {type.FullName} added to container should not be a third party type. Try making a wrapper for your type.");
            }

            if (type.GetCustomAttribute<InjectableAttribute>().IgnoreOnInit)
            {
                return;
            }

            add();
        }
    }
}
