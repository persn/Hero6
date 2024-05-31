// <copyright file="Designer.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LateStartStudio.Hero6.Scripts;

internal class Designer : IDesigner
{
    private readonly IServiceProvider serviceProvider;
    private readonly IScriptRepository scriptRepository;
    private readonly ILogger logger;
    private readonly Dictionary<Type, Type> builderMaps;
    private readonly List<IBuilderInternal> builders = [];

    public Designer(IServiceProvider serviceProvider, IScriptRepository scriptRepository, ILogger<Designer> logger)
    {
        this.serviceProvider = serviceProvider;
        this.scriptRepository = scriptRepository;
        this.logger = logger;

        builderMaps = new Dictionary<Type, Type>
        {
            { typeof(IAnimationBuilder), typeof(AnimationBuilder) },
            { typeof(ICharacterAnimationBuilder), typeof(CharacterAnimationBuilder) },
            { typeof(ICharacterBuilder), typeof(CharacterBuilder) },
            { typeof(IInventoryItemBuilder), typeof(InventoryItemBuilder) },
            { typeof(IItemBuilder), typeof(ItemBuilder) },
            { typeof(IRoomBuilder), typeof(RoomBuilder) },
        };
    }

    public IDesigner Add<TBuilder>(string name, Action<TBuilder> onBuild)
        where TBuilder : IBuilder
    {
        var implementationType = builderMaps[typeof(TBuilder)];
        var builder = (IBuilderInternal)serviceProvider.CreateInstance(implementationType, name);
        builders.Add(builder);
        onBuild?.Invoke((TBuilder)builder);

        return this;
    }

    internal void Run()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        foreach (var script in scriptRepository)
            script.Design(this); // Populate the builders

        foreach (var build in builders.OrderBy(b => b is not IAnimationBuilder))
            build.Build(); // Then build

        sw.Stop();
        logger.LogInformation("Design finished after {time}", sw.Elapsed);
    }
}

public abstract class Builder<T, TImplementation>(IServiceProvider serviceProvider) : IBuilderInternal
    where T : INameable
    where TImplementation : T
{
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    public void Build()
    {
        var repository = ServiceProvider.GetRequiredService<IRepository<T>>();
        var res = ServiceProvider.CreateInstance<TImplementation>(BuildParams());
        repository.Add(res);
    }

    protected abstract object[] BuildParams();
}

internal interface IBuilderInternal : IBuilder
{
    void Build();
}
