// <copyright file="ItemController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Items
{
    /// <summary>
    /// API for item controller.
    /// </summary>
    public class ItemController : GameController<IItemController, IItemModule>, IItemController
    {
        private readonly IScriptRepository scriptRepository;
        private readonly ILogger<ItemController> logger;
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;

        private Texture2D sprite;
        private Vector2 position;

        /// <summary>
        /// Makes a new instance of the <see cref="ItemController"/> class.
        /// </summary>
        /// <param name="module">The module for this controller.</param>
        public ItemController(IItemModule module, IServiceProvider services, IScriptRepository scriptRepository, ILogger<ItemController> logger, ContentManager content, SpriteBatch spriteBatch)
            : base(module, services)
        {
            this.scriptRepository = scriptRepository;
            this.logger = logger;
            this.content = content;
            this.spriteBatch = spriteBatch;
        }

        public override int Width => sprite.Width;

        public override int Height => sprite.Height;

        public override bool Interact(int x, int y, Interaction interaction)
        {
            if (Intersects(x, y))
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
            sprite = content.Load<Texture2D>(Module.Sprite);
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
            position.X = X;
            position.Y = Y;
        }

        public override void Draw(GameTime time)
        {
            if (Module.IsVisible)
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }

        private void InitializeGameEvents()
        {
            foreach (var scriptMethod in scriptRepository.GetScriptMethods(Module.GetType()))
            {
                if (scriptMethod.Attribute is ItemEventAttribute itemAttribute)
                {
                    if (itemAttribute.Event == ItemEvent.Grab)
                        Grab = () => scriptMethod.Invoke();
                    else if (itemAttribute.Event == ItemEvent.Talk)
                        Talk = () => scriptMethod.Invoke();
                    else if (itemAttribute.Event == ItemEvent.Look)
                        Look = () => scriptMethod.Invoke();
                    else
                    {
                        var message = $@"Unrecognized item event ""{itemAttribute.Event}"".";
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
