// <copyright file="Hero6EngineConfiguration.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Logging;
using LateStartStudio.Hero6.ModuleController.UserInterfaces;
using LateStartStudio.Hero6.MonoGame;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.ControllerRepository;
using LateStartStudio.Hero6.Services.DotNetWrappers;
using LateStartStudio.Hero6.Services.PlatformInfo;
using LateStartStudio.Hero6.Services.Settings;
using LateStartStudio.Hero6.Services.UserInterfaces;
using LateStartStudio.Hero6.Services.UserInterfaces.Input.Mouse;
using LateStartStudio.Hero6.Threading;
using LateStartStudio.Hero6.Verbs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace LateStartStudio.Hero6.Engine.Extensions
{
    public static class Hero6EngineConfiguration
    {
        /// <summary>
        /// Run the game via the <see cref="IHost"/>, run this instead of <see cref="IHost.StartAsync(CancellationToken)"/>
        /// to start the game.
        /// </summary>
        public static Task RunGameAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            var res = Task.CompletedTask;
            var services = host.Services;

            try
            {
                using (var game = services.GetService<MainGame>())
                {
                    GlobalServiceProvider.Instance = services;

                    // Run the "design" phase of the game and populate the repositories with data
                    services.GetRequiredService<Designer>().Run();

                    // By adding IGameComponent instances to the Game, Initialize/Load/Update/Draw/Unload handling will be deferred to
                    // MonoGame. We can adjust behavior on the GameLoop by changing our GameComponent implementations.
                    foreach (var gameComponent in services.GetRequiredService<IEnumerable<Microsoft.Xna.Framework.IGameComponent>>())
                        game.Components.Add(gameComponent);

                    // First load all game modules
                    // TODO The services should probably load the modules themselves, but circular dependencies needs to be resolved
                    var ui = services.GetService<MonoGameUserInterfaces>();
                    var userInterfaceModules = services.GetServices<IUserInterfaceModule>();
                    userInterfaceModules.ForEach(m => ui.Add(m));

                    // Start the host and all related mechanics, dependency injection, logging, etc.
                    // awaiting this will make the process hang around after the game had been closed, so don't
                    res = host.RunAsync(cancellationToken);

                    // Pre config completed so start the game
                    game.Run();
                }
            }
#if !DEBUG
            catch (System.Exception e)
            {
                var logger = services.GetService<Microsoft.Extensions.Logging.ILogger<Game>>();
                var fileLoggerSettings = services.GetService<FileLoggerSettings>();

                //logger.exce
                logger.LogError(e, "Hero6 has crashed, logging stack trace.");
                fileLoggerSettings.WillDeleteLogOnDispose = false;
                var p = new System.Diagnostics.Process { StartInfo = { UseShellExecute = true, FileName = fileLoggerSettings.FileName } };
                p.Start();
            }
#endif
            finally
            {
                Environment.Exit(0);
            }

            return res;
        }

        public static IHostBuilder ConfigureHero6Engine(this IHostBuilder hostBuilder) => hostBuilder
            .ConfigureServices((context, services) =>
            {
                services

                    // MonoGame constructs
                    .AddSingleton<MainGame>() // Entry point
                    .AddSingleton(s => s.GetService<MainGame>().Content)
                    .AddSingleton(s => s.GetService<MainGame>().GraphicsDeviceManager)
                    .AddSingleton(s => s.GetService<MainGame>().SpriteBatch)
                    .AddSingleton(ConfigureGameComponents)

                    // .NET wrappers
                    .AddSingleton<IFileWrapper, FileWrapper>()
                    .AddSingleton<IDirectoryWrapper, DirectoryWrapper>()

                    // Hero6 services
                    .AddSingleton<MonoGameUserInterfaces>()
                    .AddSingleton<IUserInterfaces>(s => s.GetService<MonoGameUserInterfaces>())

                    .AddSingleton<IGameSettings, GameSettings>()
                    .AddSingleton<IUserSettings, UserSettings>()
                    .AddSingleton<IPlatformInfo, PlatformInfo>()

                    .AddSingleton<IMouseCore, MouseCore>()
                    .AddSingleton<IMouse, Mouse>()

                    .AddSingleton<IVerbHandler, VerbHandler>()

                    .AddSingleton<Designer>()

                    // Logging
                    .AddSingleton<FileLoggerSettings>()
                    .AddHostedService<FileLoggerHostedService>()

                    // Repositories
                    .AddSingleton<IControllerRepository, ControllerRepositoryProvider>()
                    .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                    .AddSingleton<ICharacterRepository, CharacterRepository>()
                    .AddSingleton<IRepository<ICharacter>>(s => s.GetRequiredService<ICharacterRepository>())
                    .AddSingleton<IScriptRepository, ScriptRepository>()
                ;
            })
            .ConfigureLogging(logging =>
            {
                // Microsoft.Extensions.Logging doesn't support file logging out of the box so we need a 3rd party
                // framework and configure it
                var services = logging.Services.BuildServiceProvider();
                var fileLoggerSettings = services.GetService<FileLoggerSettings>();
                var loggingConfig = new LoggingConfiguration();
                loggingConfig.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, new FileTarget("logfile")
                {
                    FileName = fileLoggerSettings.FileName,
                    Layout = "${message} ${exception:format=tostring}$",
                });

                logging.ClearProviders().AddDebug().AddNLog(loggingConfig);
            });

        private static IEnumerable<Microsoft.Xna.Framework.IGameComponent> ConfigureGameComponents(IServiceProvider s)
        {
            yield return s.CreateInstance<UpdateSynchronizationGameComponent>();

            foreach (var c in s.GetRequiredService<IRepository<ICharacter>>().GetAll())
                yield return s.CreateInstance<CharacterGameComponent>(c);

            foreach (var i in s.GetRequiredService<IRepository<IInventoryItem>>().GetAll())
                yield return s.CreateInstance<InventoryItemGameComponent>(i);

            foreach (var i in s.GetRequiredService<IRepository<IItem>>().GetAll())
                yield return s.CreateInstance<ItemGameComponent>(i);

            foreach (var c in s.GetRequiredService<IRepository<IRoom>>().GetAll().Cast<Room>())
            {
                yield return s.CreateInstance<RoomGameComponent>(c);
                yield return s.CreateInstance<WalkAreasGameComponent>(c.WalkAreas);
                yield return s.CreateInstance<HotspotsGameComponent>(c.Get<IScriptComponent<HotspotEvent>>());
            }

            yield return s.CreateInstance<ScriptsGameComponent>();
        }
    }
}
