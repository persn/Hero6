// <copyright file="Character.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Threading;
using LateStartStudio.Hero6.Verbs;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Characters;

internal static class CharacterExtensions
{
    public static void Face(this ICharacter character, Vector2 vector)
        => character.Face((int)vector.X, (int)vector.Y);

    public static void Face(this ICharacter character, Point point)
        => character.Face(point.X, point.Y);

    public static void Face(this ICharacter character, System.Drawing.Point point)
        => character.Face(point.X, point.Y);
}

internal class Character : Composite, ICharacter
{
    private readonly ICampaigns campaigns;
    private readonly List<IInventoryItem> inventory = new();

    private Room room;
    private CharacterAnimation idleAnimation;
    private CharacterAnimation moveAnimation;

    public Character(
        CharacterData data,
        IPositionComponent position,
        IVisibleComponent visibility,
        IScriptComponent<CharacterEvent> scriptable,
        IMoveableComponent moveable,
        ICampaigns campaigns,
        IRepository<ICharacterAnimation> characterAnimations)
        : base(position, visibility, scriptable, moveable)
    {
        this.campaigns = campaigns;
        Name = data.Name;
        moveable.IsMovingChanged += OnIsMovingChanged;

        if (!string.IsNullOrWhiteSpace(data.IdleAnimation))
        {
            IdleAnimation = characterAnimations[data.IdleAnimation];
            Add((IComponent)IdleAnimation);
        }

        if (!string.IsNullOrWhiteSpace(data.MoveAnimation))
        {
            MoveAnimation = characterAnimations[data.MoveAnimation];
            Add((IComponent)MoveAnimation);
        }
    }

    public string Name { get; }

    public int X
    {
        get => Get<IPositionComponent>().X;
        set => Get<IPositionComponent>().X = value;
    }

    public int Y
    {
        get => Get<IPositionComponent>().Y;
        set => Get<IPositionComponent>().Y = value;
    }

    public bool IsVisible
    {
        get => Get<IVisibleComponent>().IsVisible;
        set
        {
            Get<IVisibleComponent>().IsVisible = value;
            ToggleAnimationVisibility();
        }
    }

    public override bool Enabled
    {
        get => base.Enabled;
        set
        {
            base.Enabled = value;

            if (value)
            {
                if (Get<IMoveableComponent>().IsMoving)
                    moveAnimation.Enabled = true;
                else
                    idleAnimation.Enabled = true;
            }
            else
            {
                if (idleAnimation != null)
                    idleAnimation.Enabled = false;
                if (moveAnimation != null)
                    moveAnimation.Enabled = false;
            }
        }
    }

    public bool IsPlayer => campaigns.Current.Player == this;

    public int Speed
    {
        get => Get<IMoveableComponent>().Speed;
        set => Get<IMoveableComponent>().Speed = value;
    }

    public IRoom Room
    {
        get => room;
        private set => room = (Room)value;
    }

    public Stats Stats { get; } = new Stats();

    public ICharacterAnimation IdleAnimation
    {
        get => idleAnimation;
        set => idleAnimation = (CharacterAnimation)value;
    }

    public ICharacterAnimation MoveAnimation
    {
        get => moveAnimation;
        set => moveAnimation = (CharacterAnimation)value;
    }

    public IEnumerable<IInventoryItem> Inventory => inventory;

    internal int FeetX
    {
        get
        {
            var pos = Get<IPositionComponent>();
            return pos.X + (pos.Width / 2);
        }

        set
        {
            var pos = Get<IPositionComponent>();
            pos.X = value - (pos.Width / 2);
        }
    }

    internal int FeetY
    {
        get
        {
            var pos = Get<IPositionComponent>();
            return pos.Y + pos.Height;
        }

        set
        {
            var pos = Get<IPositionComponent>();
            pos.Y = value - pos.Height;
        }
    }

    public void Add(IInventoryItem item) => inventory.Add(item);

    public void Remove(IInventoryItem item) => inventory.Remove(item);

    public bool Has(IInventoryItem item) => inventory.Contains(item);

    public Task<bool> WalkAsync(int x, int y) => Get<IMoveableComponent>().MoveAsync(x, y);

    public void Face(CharacterDirection direction)
    {
        var animation = Get<IMoveableComponent>()?.IsMoving ?? false
            ? moveAnimation
            : idleAnimation;
        animation.Direction = direction;
    }

    public void Face(int locationX, int locationY)
    {
        var from = new Point(FeetX, FeetY);
        var to = new Point(locationX, locationY);
        var direction = to - from;
        Face(direction.ToVector2().ToCharacterDirection());
    }

    public void SetAsPlayer()
    {
        if (campaigns.Current.Player is Character current)
            current.Get<IMoveableComponent>().Enabled = false;

        campaigns.Current.Player = this;
        Get<IMoveableComponent>().Enabled = true;
    }

    public void ChangeRoom(IRoom room, int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown)
        => ChangeRoom((Room)room, x, y, direction);

    public bool Interact(int x, int y, Verb interaction)
    {
        if (interaction != Verb.Move)
        {
            return Get<IScriptComponent<CharacterEvent>>().Invoke(x, y, interaction.ToCharacterEvent());
        }

        return false;
    }

    private void ChangeRoom(Room room, int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown)
    {
        Get<IMoveableComponent>().Stop();
        FeetX = x;
        FeetY = y;
        Face(direction);

        if (IsPlayer && this.room != null)
        {
            foreach (var r in this.room.Items.Cast<Item>())
                r.Enabled = r.IsVisible = false;
            foreach (var c in this.room.Characters.Cast<Character>())
                c.Enabled = c.IsVisible = false;

            this.room.Enabled = this.room.IsVisible = false;
        }

        this.room?.Remove(this);
        room.Add(this);
        Room = room;

        if (IsPlayer)
        {
            foreach (var r in room.Items.Cast<Item>())
                r.Enabled = r.IsVisible = true;
            foreach (var c in room.Characters.Cast<Character>())
                c.Enabled = c.IsVisible = true;

            room.Enabled = room.IsVisible = true;
        }
    }

    private async Task OnIsMovingChanged(object sender, EventArgs e)
    {
        UpdateSynchronizationContext.Instance.Post(ToggleAnimationVisibility);
        await Task.CompletedTask;
    }

    private void ToggleAnimationVisibility()
    {
        if (idleAnimation != null)
            idleAnimation.Enabled = IsVisible && !Get<IMoveableComponent>().IsMoving;
        if (moveAnimation != null)
            moveAnimation.Enabled = IsVisible && Get<IMoveableComponent>().IsMoving;
    }
}
