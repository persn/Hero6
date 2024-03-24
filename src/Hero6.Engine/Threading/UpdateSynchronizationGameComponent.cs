// <copyright file="UpdateSynchronizationGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Threading;

internal class UpdateSynchronizationGameComponent : GameComponent
{
    public UpdateSynchronizationGameComponent(MonoGame.Game game) : base(game)
    {
        UpdateOrder = (int)MonoGame.UpdateOrder.Base;
    }

    public override void Update(GameTime gameTime)
    {
        UpdateSynchronizationContext.Instance.Run();
        base.Update(gameTime);
    }
}
