// <copyright file="VisibleComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Components;

internal interface IVisibleComponent : IComponent
{
    event EventHandler IsVisibleChanged;

    bool IsVisible { get; set; }
}

internal class VisibleComponent : Component, IVisibleComponent
{
    private bool isVisible;

    public event EventHandler IsVisibleChanged;

    public bool IsVisible
    {
        get => isVisible;
        set
        {
            if (isVisible != value)
            {
                isVisible = value;
                IsVisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
