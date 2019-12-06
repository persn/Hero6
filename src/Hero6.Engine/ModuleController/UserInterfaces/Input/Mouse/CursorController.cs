// <copyright file="CursorController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Services.Assets;
using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.Graphics;
using LateStartStudio.Hero6.Services.UserInterfaces.Input.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.ModuleController.UserInterfaces.Input.Mouse
{
    [Injectable(
        LifeCycle = LifeCycle.Transient)]
    public class CursorController : Controller<ICursorController, ICursorModule>, ICursorController
    {
        private readonly IMouse mouse;
        private readonly IAssetsRepository assets;
        private readonly IRendererService renderer;

        private Texture2D cursor;

        public CursorController(ICursorModule module, IContainer container, IMouse mouse, IAssetsRepository assets, IRendererService renderer) : base(module, container)
        {
            this.mouse = mouse;
            this.assets = assets;
            this.renderer = renderer;
        }

        public override int Width => cursor.Width;

        public override int Height => cursor.Height;

        public override void Load() => cursor = assets.Load<Texture2D>(Module.Source);

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
            X = mouse.X;
            Y = mouse.Y;
        }

        public override void Draw(GameTime time) => renderer.Draw(cursor, new Vector2(X, Y));

        public bool Equals<T>() => typeof(T) == Module.GetType();
    }
}
