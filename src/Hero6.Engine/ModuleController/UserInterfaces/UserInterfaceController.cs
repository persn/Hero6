// <copyright file="UserInterfaceController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.ModuleController.UserInterfaces.Components;
using LateStartStudio.Hero6.ModuleController.UserInterfaces.Input.Mouse;
using LateStartStudio.Hero6.Services.ControllerRepository;
using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.UserInterfaces.Input.Mouse;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.ModuleController.UserInterfaces
{
    [Injectable(LifeCycle = LifeCycle.Transient)]
    public class UserInterfaceController : Controller<IUserInterfaceController, IUserInterfaceModule>, IUserInterfaceController
    {
        private readonly IContainer container;
        private readonly IMouse mouse;
        private readonly IControllerRepository controllerRepository;
        private readonly Dictionary<Type, WindowController> windows = new Dictionary<Type, WindowController>();
        private readonly Dictionary<Type, CursorController> cursors = new Dictionary<Type, CursorController>();

        public UserInterfaceController(IUserInterfaceModule module, IContainer container, IMouse mouse, IControllerRepository controllerRepository) : base(module, container)
        {
            this.container = container;
            this.mouse = mouse;
            this.controllerRepository = controllerRepository;
        }

        public override int Width { get; }

        public override int Height { get; }

        public IEnumerable<IWindowController> Windows => windows.Values;

        public IWindowController GetWindow<T>() where T : IWindowModule => windows[typeof(T)];

        public ICursorController GetCursor<T>() where T : ICursorModule => cursors[typeof(T)];

        public override void Initialize()
        {
            PreInitialize();
            FindModules<WindowModule>().ForEach(w =>
            {
                var module = container.Get<WindowModule>(w);
                windows.Add(w, container.Get<WindowController>(("module", module)));
            });
            FindModules<CursorModule>().ForEach(c =>
            {
                var module = container.Get<CursorModule>(c);
                cursors.Add(c, container.Get<CursorController>(("module", module)));
            });
            controllerRepository.Controllers.ForEach(c => c.Initialize());
            windows.Values.ForEach(w => w.PreInitialize());
            cursors.Values.ForEach(c => c.PreInitialize());
            windows.Values.ForEach(w => w.Initialize());
            cursors.Values.ForEach(c => c.Initialize());
            base.Initialize();
            mouse.AsXnaGameLoop()?.Initialize();
        }

        public override void Load()
        {
            controllerRepository.Controllers.ForEach(c => c.ToXnaGameLoop().Load());
            windows.Values.ForEach(w => w.Load());
            cursors.Values.ForEach(c => c.Load());
            mouse.AsXnaGameLoop()?.Load();
        }

        public override void Unload()
        {
            controllerRepository.Controllers.ForEach(c => c.ToXnaGameLoop().Unload());
            windows.Values.ForEach(w => w.Unload());
            mouse.AsXnaGameLoop()?.Unload();
        }

        public override void Update(GameTime time)
        {
            windows.Values.ForEach(w => w.Update(time));
            mouse.AsXnaGameLoop()?.Update(time);
        }

        public override void Draw(GameTime time)
        {
            windows.Values.ForEach(w => w.Draw(time));
            mouse.AsXnaGameLoop()?.Draw(time);
        }
    }
}
