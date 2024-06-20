// <copyright file="Hero6GameBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace LateStartStudio.Hero6;

public class Hero6GameBuilder
{
    private readonly IHostBuilder hostBuilder;

    private Hero6GameBuilder()
    {
        hostBuilder = Host.CreateDefaultBuilder();

        hostBuilder
            .ConfigureServices(serviceCollection => serviceCollection

                // MonoGame constructs
                .AddSingleton<MainGame>() // Entry point
                .AddSingleton(s => s.GetService<MainGame>().Content)
                .AddSingleton(s => s.GetService<MainGame>().GraphicsDevice)
                .AddSingleton(s => s.GetService<MainGame>().GraphicsDeviceManager)
                .AddScoped<SpriteBatch>()

                // .NET wrappers
                .AddSingleton<IFileWrapper, FileWrapper>()
                .AddSingleton<IDirectoryWrapper, DirectoryWrapper>()

                // Hero6 services
                .AddSingleton<MonoGameUserInterfaces>()
                .AddSingleton<IUserInterfaces>(s => s.GetService<MonoGameUserInterfaces>())

                .AddSingleton<IMouseCore, MouseCore>()
                .AddSingleton<IMouse, Mouse>()

                .AddSingleton<IGameSettings, GameSettings>()
                .AddSingleton<IUserSettings, UserSettings>()
                .AddSingleton<IPlatformInfo, PlatformInfo>()

                .AddSingleton<IVerbHandler, VerbHandler>()

                // Logging
                .AddSingleton<FileLoggerSettings>()
                .AddHostedService<FileLoggerHostedService>()

                // Repositories
                .AddSingleton<IControllerRepository, ControllerRepositoryProvider>()
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<ICharacterRepository, CharacterRepository>()
                .AddSingleton<IRepository<ICharacter>>(s => s.GetRequiredService<ICharacterRepository>())
                .AddSingleton<IScriptRepository, ScriptRepository>())

            .ConfigureLogging(loggingBuilder =>
            {
                // Microsoft.Extensions.Logging doesn't support file logging out of the box so we need a 3rd party
                // framework and configure it
                var services = loggingBuilder.Services.BuildServiceProvider();
                var fileLoggerSettings = services.GetService<FileLoggerSettings>();
                var loggingConfig = new LoggingConfiguration();
                loggingConfig.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, new FileTarget("logfile")
                {
                    FileName = fileLoggerSettings.FileName,
                    Layout = "${message} ${exception:format=tostring}$",
                });

                loggingBuilder
#if DEBUG
                    .SetMinimumLevel(LogLevel.Debug)
#endif
                    .ClearProviders()
                    .AddDebug()
                    .AddNLog(loggingConfig);
            })
        ;
    }

    public static Hero6GameBuilder Create() => new();

    public Hero6GameBuilder ConfigureServices(Action<IServiceCollection> configure = null)
    {
        if (configure != null)
            hostBuilder.ConfigureServices(configure);

        return this;
    }

    public Hero6Game Build() => new(hostBuilder.Build());
}

public class Hero6Game : IAsyncDisposable
{
    private readonly IHost host;
    private readonly Queue<Action<IServiceProvider>> configureDelegates = [];

    internal Hero6Game(IHost host)
    {
        this.host = host;
    }

    public ValueTask DisposeAsync()
    {
        host.Dispose();
        return ValueTask.CompletedTask;
    }

    public Hero6Game Configure(Action<IServiceProvider> configure)
    {
        if (configure != null)
            configureDelegates.Enqueue(configure);
        return this;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
#if DESKTOP
            var serviceProvider = host.Services;
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await host.StartAsync(cts.Token);

            while (configureDelegates.TryDequeue(out var configure))
                configure(serviceProvider);

            // Run the "design" phase of the game and populate the repositories with data
            serviceProvider.CreateInstance<Designer>().Run();
            Script.API = serviceProvider.CreateInstance<ScriptAPI>();
            RunGame(serviceProvider, cts);

            await host.StopAsync(cts.Token);
#endif
        }
#if !DEBUG
            catch (System.Exception e)
            {
                var logger = services.GetService<Microsoft.Extensions.Logging.ILogger<Game>>();
                var fileLoggerSettings = services.GetService<FileLoggerSettings>();

                logger.LogError(e, "Hero6 has crashed, logging stack trace.");
                fileLoggerSettings.WillDeleteLogOnDispose = false;
                var p = new System.Diagnostics.Process { StartInfo = { UseShellExecute = true, FileName = fileLoggerSettings.FileName } };
                p.Start();
            }
#endif
        finally
        {
        }

        static void RunGame(IServiceProvider serviceProvider, CancellationTokenSource cts)
        {
            // !!! Game instance is thread sensitive so it must run from creation to Dispose() synchronously
            // Do not use awaits in this method
            using var game = serviceProvider.GetRequiredService<MainGame>();
            game.Exiting += (s, e) => cts.Cancel();

            // By adding IGameComponent instances to the Game, Initialize/Load/Update/Draw/Unload handling will be deferred to
            // MonoGame. We can adjust behavior on the GameLoop by changing our GameComponent implementations.
            foreach (var component in CreateGameComponents(serviceProvider))
                game.Components.Add(component);

            // TODO Refactor user interfaces to be setup with Builder Wrapper over a 3rd party framework
            var ui = serviceProvider.GetService<MonoGameUserInterfaces>();
            var userInterfaceModules = serviceProvider.GetServices<IUserInterfaceModule>();
            userInterfaceModules.ForEach(m => ui.Add(m));

            game.Run();
        }

        static IEnumerable<IGameComponent> CreateGameComponents(IServiceProvider s)
        {
            yield return s.CreateInstance<UpdateSynchronizationGameComponent>();

            yield return s.CreateInstance<CharacterGameComponent>();
            yield return s.CreateInstance<ItemGameComponent>();
            yield return s.CreateInstance<RoomGameComponent>();
            yield return s.CreateInstance<WalkAreasGameComponent>();
            yield return s.CreateInstance<HotspotsGameComponent>();

            foreach (var i in s.GetRequiredService<IRepository<IInventoryItem>>())
                yield return s.CreateInstance<InventoryItemGameComponent>(i);

            yield return s.CreateInstance<ScriptsGameComponent>();
        }
    }
}
