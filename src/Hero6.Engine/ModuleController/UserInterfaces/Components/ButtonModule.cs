// <copyright file="ButtonModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Services.DependencyInjection;

namespace LateStartStudio.Hero6.ModuleController.UserInterfaces.Components
{
    [Injectable(LifeCycle = LifeCycle.Transient)]
    public class ButtonModule : ComponentModule<IButtonController, IButtonModule>, IButtonModule
    {
        public override string Name => "Image";

        public IComponent Child { get; set; }
    }
}
