// <copyright file="MonoGameUserInterfaces.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using LateStartStudio.Hero6.ModuleController.UserInterfaces;
using LateStartStudio.Hero6.MonoGame.GameLoop;
using LateStartStudio.Hero6.Services.DependencyInjection;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Services.UserInterfaces
{
    [Injectable]
    public class MonoGameUserInterfaces : IUserInterfaces, IXnaGameLoop
    {
        private readonly IContainer container;
        private readonly List<UserInterfaceController> controllers = new List<UserInterfaceController>();

        private UserInterfaceController current;

        public MonoGameUserInterfaces(IContainer container)
        {
            this.container = container;
        }

        public IEnumerable<IUserInterfaceModule> UserInterfaces { get; }

        public IUserInterfaceModule Current
        {
            get { return current.Module; }
            set { current = controllers.Find(u => u.Module == value); }
        }

        public void Initialize()
        {
            controllers.AddRange(container
                .Get<IEnumerable<IUserInterfaceModule>>()
                .Select(m => container.Get<UserInterfaceController>(("module", m), ("container", container))));

            current = controllers[0];
            current.Initialize();
        }

        public void Load() => current.Load();

        public void Unload() => current.Unload();

        public void Update(GameTime time) => current.Update(time);

        public void Draw(GameTime time) => current.Draw(time);
    }
}
