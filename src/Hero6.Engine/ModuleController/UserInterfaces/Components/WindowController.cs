// <copyright file="WindowController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.Services.ControllerRepository;
using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.Graphics;
using LateStartStudio.Hero6.Services.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IGraphicsDeviceService = LateStartStudio.Hero6.Services.Graphics.IGraphicsDeviceService;

namespace LateStartStudio.Hero6.ModuleController.UserInterfaces.Components
{
    [Injectable(LifeCycle = LifeCycle.Transient)]
    public class WindowController : ComponentController<IWindowController, IWindowModule>, IWindowController
    {
        private readonly IContainer container;
        private readonly IGameSettings gameSettings;
        private readonly IControllerRepository controllerRepository;
        private readonly IRendererService renderer;
        private readonly IGraphicsDeviceService graphicsDevice;

        private Texture2D background;
        private Rectangle destination;

        public WindowController(
            IWindowModule module,
            IContainer container,
            IGameSettings gameSettings,
            IControllerRepository controllerRepository,
            IRendererService renderer,
            IGraphicsDeviceService graphicsDevice)
            : base(module, container)
        {
            this.container = container;
            this.gameSettings = gameSettings;
            this.controllerRepository = controllerRepository;
            this.renderer = renderer;
            this.graphicsDevice = graphicsDevice;
        }

        public override int X
        {
            get
            {
                return base.X;
            }

            set
            {
                base.X = value;

                if (Module.Child != null)
                {
                    Module.Child.X = value;
                }
            }
        }

        public override int Y
        {
            get
            {
                return base.Y;
            }

            set
            {
                base.Y = value;

                if (Module.Child != null)
                {
                    Module.Child.Y = value;
                }
            }
        }

        public override int Width => Module.Child?.Width ?? 0;

        public override int Height => Module.Child?.Height ?? 0;

        public override bool IsVisible
        {
            get
            {
                return base.IsVisible;
            }

            set
            {
                base.IsVisible = value;

                if (IsVisible && Module.IsDialog)
                {
                    X = (gameSettings.NativeWidth / 2) - (Width / 2);
                    Y = (gameSettings.NativeHeight / 2) - (Height / 2);
                }
            }
        }

        public bool PauseGame => Module.PauseGame;

        private IController ChildToController => controllerRepository[Module.Child];

        public IImageController MakeImage(IComponent parent, string source)
        {
            var module = container.Get<ImageModule>();
            var image = container.Get<ImageController>(("module", module), ("source", source));
            image.PreInitialize();
            image.Module.Parent = parent;
            controllerRepository[image.Module] = image;
            return image;
        }

        public IStackPanelController MakeStackPanel(IComponent parent)
        {
            var module = container.Get<StackPanelModule>();
            var stackPanel = container.Get<StackPanelController>(("module", module));
            stackPanel.PreInitialize();
            stackPanel.Module.Parent = parent;
            controllerRepository[stackPanel.Module] = stackPanel;
            return stackPanel;
        }

        public IButtonController MakeButton(IComponent parent)
        {
            var module = container.Get<ButtonModule>();
            var button = container.Get<ButtonController>(("module", module));
            button.PreInitialize();
            button.Module.Parent = parent;
            controllerRepository[button.Module] = button;
            return button;
        }

        public ILabelController MakeLabel(IComponent parent, string text)
        {
            var module = container.Get<LabelModule>();
            var label = container.Get<LabelController>(("module", module));
            label.PreInitialize();
            label.Module.Text = text;
            label.Module.Parent = parent;
            controllerRepository[label.Module] = label;
            return label;
        }

        public override void Load()
        {
            background = graphicsDevice.MakeTexture(Module.Background.ToMonoGame());
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
            ChildToController.ToXnaGameLoop().Update(time);
            destination = new Rectangle(X, Y, Width, Height);
        }

        public override void Draw(GameTime time)
        {
            if (IsVisible)
            {
                renderer.Draw(background, destination, Module.Background.ToMonoGame());
                ChildToController.ToXnaGameLoop().Draw(time);
            }
        }
    }
}
