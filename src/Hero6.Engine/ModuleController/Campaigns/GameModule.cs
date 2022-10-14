// <copyright file="GameModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    /// <summary>
    /// API for game controller.
    /// </summary>
    /// <typeparam name="TController">The Controlle type.</typeparam>
    public abstract class GameModule<TController, TModule> : Module<TController, TModule>
        where TController : IGameController
        where TModule : IGameModule
    {
    }
}
