// <copyright file="VerbBarButton.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.UserInterfaces.Components;
using LateStartStudio.Hero6.Services.DependencyInjection;

namespace LateStartStudio.Hero6.UserInterfaces.SierraVga.Windows
{
    [Injectable(LifeCycle = LifeCycle.Transient)]
    public class VerbBarButton : WindowModule
    {
        private IStackPanelModule stackPanel;

        public override bool IsDialog => false;

        public override string Name => "Verb Bar Button";

        public override void Initialize()
        {
            base.Initialize();

            IsVisible = false;
            stackPanel = MakeStackPanel(this);
            Child = stackPanel;
        }
    }
}
