// <copyright file="CharacterController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.ModuleController.Campaigns.Animations;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters.Stats;
using LateStartStudio.Hero6.ModuleController.Campaigns.InventoryItems;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms.Regions;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Characters
{
    /// <summary>
    /// API for the character controller.
    /// </summary>
    public class CharacterController : GameController<ICharacterController, ICharacterModule>, ICharacterController
    {
        private readonly ICampaigns campaigns;
        private readonly IScriptRepository scriptRepository;
        private readonly ILogger<CharacterController> logger;
        private readonly StatsController stats;
        private readonly List<InventoryItemController> inventory = new List<InventoryItemController>();
        private readonly Queue<Point> path = new Queue<Point>();

        private TaskCompletionSource currentWalkTask = new();
        private double pixelsToMove;
        private CharacterAnimationController currentAnimation;
        private CharacterAnimationController idleAnimation;
        private CharacterAnimationController moveAnimation;

        /// <summary>
        /// Makes a new instance of the <see cref="CharacterController"/> class.
        /// </summary>
        /// <param name="module">The module corresponding to this character.</param>
        public CharacterController(ICharacterModule module, IServiceProvider services, ICampaigns campaigns, IScriptRepository scriptRepository, ILogger<CharacterController> logger, StatsController stats)
            : base(module, services)
        {
            this.campaigns = campaigns;
            this.scriptRepository = scriptRepository;
            this.logger = logger;
            this.stats = stats;
        }

        public override int Width => currentAnimation?.Width ?? 0;

        public override int Height => currentAnimation?.Height ?? 0;

        public bool IsPlayer => campaigns.Current.Player == Module;

        public int Speed { get; set; } = 60;

        public IRoomController Room
        {
            get
            {
                return campaigns.AsMonoGame().CurrentController.Rooms.Values.Single(r => r.Characters.Contains(this));
            }
        }

        public IStatsController Stats => stats;

        public ICharacterAnimationController IdleAnimation
        {
            get { return idleAnimation; }
            set { idleAnimation = campaigns.AsMonoGame().CurrentController.CharacterAnimations[value.Module.GetType()]; }
        }

        public ICharacterAnimationController MoveAnimation
        {
            get { return moveAnimation; }
            set { moveAnimation = campaigns.AsMonoGame().CurrentController.CharacterAnimations[value.Module.GetType()]; }
        }

        public IEnumerable<IInventoryItemController> Inventory => inventory;

        public void AddInventoryItem<T>() where T : IInventoryItemModule
        {
            inventory.Add(campaigns.AsMonoGame().CurrentController.InventoryItems[typeof(T)]);
        }

        public void RemoveInventoryItem<T>() where T : IInventoryItemModule
        {
            inventory.Remove(campaigns.AsMonoGame().CurrentController.InventoryItems[typeof(T)]);
        }

        public bool HasInventoryItem<T>() where T : IInventoryItemModule
        {
            return inventory.Contains(campaigns.AsMonoGame().CurrentController.InventoryItems[typeof(T)]);
        }

        public Task WalkAsync(int x, int y)
        {
            // Clearing current path must happen before we calculate a new walk path asynchronously, because
            // if the character moves even a single unit from the current position while the calculation is
            // working, the logic can encounter unexpected situations, and in worst case crash.
            path.Clear();
            currentWalkTask.TrySetResult();
            var walkTask = currentWalkTask = new();
            Point from = new(X, Y);
            Point to = new(x, y);

            // Calculating paths can be computation intensive, especially if we want to do it for multiple
            // characters at the same time, so doing it asynchronously will help prevent stuttering
            _ = Task.Run(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = $"{nameof(WalkAsync)}-{Module.Name}";
                    var newPath = ((WalkAreasController)Room.WalkAreas).GetPath(from, to);

                    if (newPath != null && currentWalkTask == walkTask)
                        newPath.Skip(1).ForEach(p => path.Enqueue(p));
                    else
                    {
                        walkTask.SetException(new Exception(
                            $"Path from {from} to {to} for character {Module.Name} could not be calculated. Are both pixels in the same walkable region?"));
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"{e.GetType()} when calculating walkable path for character {Module.Name} from {from} to {to}.");
                    walkTask.SetException(e);
                }
            });

            return walkTask.Task;
        }

        public void Face(CharacterDirection direction)
        {
            if (idleAnimation != null)
            {
                idleAnimation.Direction = direction;
            }

            if (moveAnimation != null)
            {
                moveAnimation.Direction = direction;
            }
        }

        public void Face(int locationX, int locationY)
        {
            var from = new Point(X, Y);
            var to = new Point(locationX, locationY);
            var direction = to - from;
            Face(direction.ToVector2().ToCharacterDirection());
        }

        public void ChangeRoom<T>(int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown) where T : IRoomModule
        {
            path.Clear();
            X = x;
            Y = y;
            Face(direction);
            Room.RemoveCharacter(Module);
            campaigns.AsMonoGame().CurrentController.Rooms[typeof(T)].AddCharacter(this);
        }

        public void SetAsPlayer() => campaigns.Current.Player = Module;

        public override bool Interact(int x, int y, Interaction interaction)
        {
            if (currentAnimation.Interact(x, y, interaction))
            {
                switch (interaction)
                {
                    case Interaction.Eye:
                        Look?.Invoke();
                        break;
                    case Interaction.Mouth:
                        Talk?.Invoke();
                        break;
                    case Interaction.Hand:
                        Grab?.Invoke();
                        break;
                    case Interaction.Move:
                    default:
                        return false;
                }

                return true;
            }

            return false;
        }

        public override void Initialize()
        {
            base.Initialize();

            InitializeGameEvents();
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
            Move(time);
            currentAnimation = path.Count > 0 ? moveAnimation : idleAnimation;
            currentAnimation.X = X;
            currentAnimation.Y = Y;
            currentAnimation.Update(time);
        }

        public override void Draw(GameTime time)
        {
            currentAnimation.Draw(time);
        }

        private void Move(GameTime time)
        {
            if (path.Count == 0)
            {
                return;
            }

            pixelsToMove += Speed * time.ElapsedGameTime.TotalSeconds;
            var pixelsToMoveFloor = (int)pixelsToMove;
            pixelsToMove -= pixelsToMoveFloor;

            for (var i = 0; i < pixelsToMoveFloor; i++)
            {
                var newLocation = path.Dequeue();
                Face(newLocation.X, newLocation.Y);
                X = newLocation.X;
                Y = newLocation.Y;

                if (!path.Any())
                {
                    pixelsToMove = 0.0;
                    currentWalkTask.TrySetResult();
                    break;
                }
            }
        }

        private void InitializeGameEvents()
        {
            foreach (var scriptMethod in scriptRepository.GetScriptMethods(Module.Name))
            {
                if (scriptMethod.Attribute is CharacterEventAttribute characterAttribute)
                {
                    if (characterAttribute.Event == CharacterEvent.Grab)
                        Grab = () => scriptMethod.Invoke();
                    else if (characterAttribute.Event == CharacterEvent.Talk)
                        Talk = () => scriptMethod.Invoke();
                    else if (characterAttribute.Event == CharacterEvent.Look)
                        Look = () => scriptMethod.Invoke();
                    else
                    {
                        var message = $@"Unrecognized character event ""{characterAttribute.Event}"".";
                        var exception = new ScriptEventException(message, scriptMethod.Method);
                        logger.LogWarning(exception, message);
#if DEBUG
                        throw exception;
#endif
                    }
                }
            }
        }
    }
}
