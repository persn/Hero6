// <copyright file="ScriptsGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Scripts;

internal class ScriptsGameComponent : GameComponent
{
    private readonly IScriptRepository scriptRepository;

    public ScriptsGameComponent(MainGame game, IScriptRepository scriptRepository) : base(game)
    {
        UpdateOrder = (int)MonoGame.UpdateOrder.Scripts;

        this.scriptRepository = scriptRepository;
    }

    public override void Initialize()
    {
        base.Initialize();

        foreach (var script in scriptRepository)
        {
            script.Initialize();
            script.InitializeAsync().GetAwaiter().GetResult(); // TODO await this properly
        }
    }
}
