// <copyright file="Designer.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using System.Globalization;
using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Verbs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LateStartStudio.Hero6.Scripts;

internal partial class Designer(IServiceProvider serviceProvider, ILogger<Designer> logger)
{
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly ILogger logger = logger;

    public T Add<T>(string name) where T : IDesigner
    {
        if (typeof(T) == typeof(IAnimationDesigner))
            animations.Push(new AnimationData { Name = name });
        else if (typeof(T) == typeof(ICharacterAnimationDesigner))
            characterAnimations.Push(new CharacterAnimationData { Name = name });
        else if (typeof(T) == typeof(ICharacterDesigner))
            characters.Push((new CharacterData { Name = name }, []));
        else if (typeof(T) == typeof(IInventoryItemDesigner))
            inventoryItems.Push(new InventoryItemData { Name = name });
        else if (typeof(T) == typeof(IItemDesigner))
            items.Push((new ItemData { Name = name }, []));
        else if (typeof(T) == typeof(IRoomDesigner))
            rooms.Push((new RoomData { Name = name }, serviceProvider.CreateInstance<HotspotsDesigner>()));
        else
            throw new ArgumentException("Unsupported builder.", typeof(T).Name);

        IDesigner designer = this;
        return (T)designer;
    }

    internal void Design()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var scriptRepository = serviceProvider.GetService<IRepository<Script>>();

        foreach (var script in scriptRepository)
            script.Design(this);

        sw.Stop();
        logger.LogInformation("Design finished after {time}", sw.Elapsed);
    }
}

/// <summary>
/// Build game objects from <see cref="IDesigner"/>.
/// </summary>
internal interface IBuilder<T>
{
    IEnumerable<T> Build();
}

internal partial class Designer : IBuilder<Script>
{
    IEnumerable<Script> IBuilder<Script>.Build() => serviceProvider.GetService<IEnumerable<Script>>();
}

internal partial class Designer : IAnimationDesigner, IBuilder<IAnimation>
{
    private readonly Stack<AnimationData> animations = new();

    IAnimationDesigner IAnimationDesigner.WithColumns(int columns)
    {
        animations.Peek().Columns = columns;
        return this;
    }

    IAnimationDesigner IAnimationDesigner.WithRows(int rows)
    {
        animations.Peek().Rows = rows;
        return this;
    }

    IAnimationDesigner IAnimationDesigner.WithSprite(string sprite)
    {
        animations.Peek().Sprite = sprite;
        return this;
    }

    IEnumerable<IAnimation> IBuilder<IAnimation>.Build()
    {
        while (animations.TryPop(out var data))
        {
            yield return serviceProvider.CreateInstance<Animation>(data);
        }
    }
}

internal partial class Designer : ICharacterAnimationDesigner, IBuilder<ICharacterAnimation>
{
    private readonly Stack<CharacterAnimationData> characterAnimations = [];

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithCenterDownAnimation(string animation)
    {
        characterAnimations.Peek().CenterDown = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithCenterUpAnimation(string animation)
    {
        characterAnimations.Peek().CenterUp = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithLeftCenterAnimation(string animation)
    {
        characterAnimations.Peek().LeftCenter = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithLeftDownAnimation(string animation)
    {
        characterAnimations.Peek().LeftDown = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithLeftUpAnimation(string animation)
    {
        characterAnimations.Peek().LeftUp = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithRightCenterAnimation(string animation)
    {
        characterAnimations.Peek().RightCenter = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithRightDownAnimation(string animation)
    {
        characterAnimations.Peek().RightDown = animation;
        return this;
    }

    ICharacterAnimationDesigner ICharacterAnimationDesigner.WithRightUpAnimation(string animation)
    {
        characterAnimations.Peek().RightUp = animation;
        return this;
    }

    IEnumerable<ICharacterAnimation> IBuilder<ICharacterAnimation>.Build()
    {
        while (characterAnimations.TryPop(out var data))
        {
            yield return serviceProvider.CreateInstance<CharacterAnimation>(data);
        }
    }
}

internal partial class Designer : ICharacterDesigner, IBuilder<ICharacter>
{
    private readonly Stack<(CharacterData Data, Dictionary<CharacterEvent, Func<Task>> Events)> characters = [];

    ICharacterDesigner IInteractionDesigner<ICharacterDesigner>.OnGrab(Func<Task> interaction)
    {
        characters.Peek().Events[CharacterEvent.Grab] = interaction;
        return this;
    }

    ICharacterDesigner IInteractionDesigner<ICharacterDesigner>.OnLook(Func<Task> interaction)
    {
        characters.Peek().Events[CharacterEvent.Look] = interaction;
        return this;
    }

    ICharacterDesigner IInteractionDesigner<ICharacterDesigner>.OnTalk(Func<Task> interaction)
    {
        characters.Peek().Events[CharacterEvent.Talk] = interaction;
        return this;
    }

    ICharacterDesigner ICharacterDesigner.WithIdleAnimation(string animation)
    {
        characters.Peek().Data.IdleAnimation = animation;
        return this;
    }

    ICharacterDesigner ICharacterDesigner.WithMoveAnimation(string animation)
    {
        characters.Peek().Data.MoveAnimation = animation;
        return this;
    }

    IEnumerable<ICharacter> IBuilder<ICharacter>.Build()
    {
        while (characters.TryPop(out var c))
        {
            var position = serviceProvider.CreateInstance<SystemDrawingPositionComponent>();
            var visibility = serviceProvider.CreateInstance<VisibleComponent>();
            var scriptable = serviceProvider.CreateInstance<ScriptComponent<CharacterEvent>>(c.Events);
            var moveable = serviceProvider.CreateInstance<MoveableComponent>();

            yield return serviceProvider.CreateInstance<Character>(c.Data, position, visibility, scriptable, moveable);
        }
    }
}

internal partial class Designer : IInventoryItemDesigner, IBuilder<IInventoryItem>
{
    private readonly Stack<InventoryItemData> inventoryItems = [];

    IEnumerable<IInventoryItem> IBuilder<IInventoryItem>.Build()
    {
        while (inventoryItems.TryPop(out var data))
        {
            yield return serviceProvider.CreateInstance<InventoryItem>(data);
        }
    }
}

internal partial class Designer : IItemDesigner, IBuilder<IItem>
{
    private readonly Stack<(ItemData Data, Dictionary<ItemEvent, Func<Task>> Events)> items = [];

    IItemDesigner IItemDesigner.WithSprite(string sprite)
    {
        items.Peek().Data.Sprite = sprite;
        return this;
    }

    IItemDesigner IInteractionDesigner<IItemDesigner>.OnLook(Func<Task> interaction)
    {
        items.Peek().Events[ItemEvent.Look] = interaction;
        return this;
    }

    IItemDesigner IInteractionDesigner<IItemDesigner>.OnGrab(Func<Task> interaction)
    {
        items.Peek().Events[ItemEvent.Grab] = interaction;
        return this;
    }

    IItemDesigner IInteractionDesigner<IItemDesigner>.OnTalk(Func<Task> interaction)
    {
        items.Peek().Events[ItemEvent.Talk] = interaction;
        return this;
    }

    IEnumerable<IItem> IBuilder<IItem>.Build()
    {
        while (items.TryPop(out var i))
        {
            var position = serviceProvider.CreateInstance<SystemDrawingPositionComponent>();
            var visibility = serviceProvider.CreateInstance<VisibleComponent>();
            var scriptable = serviceProvider.CreateInstance<ScriptComponent<ItemEvent>>(i.Events);
            var spriteRenderer = serviceProvider.CreateInstance<SpriteRendererComponent>();

            yield return serviceProvider.CreateInstance<Item>(i.Data, position, visibility, scriptable, spriteRenderer);
        }
    }
}

internal partial class Designer : IRoomDesigner, IBuilder<IRoom>
{
    private readonly Stack<(RoomData Data, HotspotsDesigner Hotspots)> rooms = [];

    IRoomDesigner IRoomDesigner.WithBackground(string background)
    {
        rooms.Peek().Data.Background = background;
        return this;
    }

    IRoomDesigner IRoomDesigner.WithHotspots(string hotspotsMask, Action<IHotspotsDesigner> build)
    {
        var r = rooms.Peek();
        r.Data.HotspotsMask = hotspotsMask;
        build?.Invoke(r.Hotspots);
        return this;
    }

    IRoomDesigner IRoomDesigner.WithWalkAreas(string walkAreasMask)
    {
        rooms.Peek().Data.WalkAreasMask = walkAreasMask;
        return this;
    }

    IEnumerable<IRoom> IBuilder<IRoom>.Build()
    {
        while (rooms.TryPop(out var r))
        {
            var visibility = serviceProvider.CreateInstance<VisibleComponent>();
            var scriptable = serviceProvider.CreateInstance<HotspotsScriptComponent>(
                r.Data.HotspotsMask,
                r.Hotspots.Build().ToDictionary(h => (h.Event, h.Color), h => h.Delegate));
            var spriteRenderer = serviceProvider.CreateInstance<SpriteRendererComponent>();

            yield return serviceProvider.CreateInstance<Room>(r.Data, visibility, scriptable, spriteRenderer);
        }
    }
}

internal class HotspotDesigner : IHotspotDesigner
{
    private readonly Dictionary<HotspotEvent, Func<Task>> interactions = [];

    public IHotspotDesigner OnGrab(Func<Task> interaction)
    {
        interactions[HotspotEvent.Grab] = interaction;
        return this;
    }

    public IHotspotDesigner OnLook(Func<Task> interaction)
    {
        interactions[HotspotEvent.Look] = interaction;
        return this;
    }

    public IHotspotDesigner OnStandingOn(Func<Task> interaction)
    {
        interactions[HotspotEvent.StandingOn] = interaction;
        return this;
    }

    public IHotspotDesigner OnTalk(Func<Task> interaction)
    {
        interactions[HotspotEvent.Talk] = interaction;
        return this;
    }

    public IEnumerable<(HotspotEvent Event, Func<Task> Delegate)> Build()
    {
        foreach (var interaction in interactions)
            yield return (interaction.Key, interaction.Value);
    }
}

internal class HotspotsDesigner(IServiceProvider serviceProvider) : IHotspotsDesigner
{
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly Dictionary<Color, HotspotDesigner> hotspots = [];

    public IHotspotsDesigner For(string color, Action<IHotspotDesigner> build)
    {
        var c = Color.FromArgb(int.Parse(color.Replace("#", string.Empty), NumberStyles.HexNumber));
        var hotspotDesigner = serviceProvider.CreateInstance<HotspotDesigner>();
        build?.Invoke(hotspotDesigner);
        hotspots[c] = hotspotDesigner;
        return this;
    }

    public IEnumerable<(HotspotEvent Event, Color Color, Func<Task> Delegate)> Build()
    {
        foreach (var hotspot in hotspots)
        {
            foreach (var interaction in hotspot.Value.Build())
            {
                yield return (interaction.Event, hotspot.Key, interaction.Delegate);
            }
        }
    }
}
