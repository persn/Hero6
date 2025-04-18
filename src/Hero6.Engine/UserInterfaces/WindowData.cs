// <copyright file="WindowData.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

internal class WindowData
{
    public string Name { get; set; }

    public bool IsDialog { get; set; }

    public UIComponentType ChildType { get; set; }

    public Func<IWindow, MouseButtonInteraction, Task> OnMouseButtonUp { get; set; }
}
