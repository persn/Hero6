// <copyright file="IUIComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;

namespace LateStartStudio.Hero6.UserInterfaces;

internal interface IUIComponent : IComponent, IUserInterface
{
    event EventHandler<MouseButtonInteraction> MouseButtonUp;
}
