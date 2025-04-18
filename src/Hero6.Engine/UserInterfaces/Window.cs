// <copyright file="Window.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

internal class Window(WindowData data, IUIComponent child) : Composite(child), IWindow
{
    private readonly WindowData data = data;

    public override string Name { get; } = data.Name;

    public int Width => Get<IUIComponent>().Width;

    public int Height => Get<IUIComponent>().Height;

    public bool IsVisible
    {
        get => Get<IUIComponent>().IsVisible;
        set => Get<IUIComponent>().IsVisible = value;
    }

    public bool PauseGame => data.IsDialog;

    public override void Initialize()
    {
        base.Initialize();
        IsVisible = false;

        if (data.OnMouseButtonUp != null)
            Get<IUIComponent>().MouseButtonUp += (_, e) => data.OnMouseButtonUp(this, e);
    }
}
