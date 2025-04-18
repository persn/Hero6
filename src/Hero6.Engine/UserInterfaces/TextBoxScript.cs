// <copyright file="TextBoxScript.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

internal class TextBoxScript(IRepository<IWindow> windows) : Script
{
    private Window window;
    private ILabel label;

    public override void Design(IDesigner designer) => designer
        .Add<IWindowDesigner>(nameof(TextBoxScript))
        .IsDialog(true)
        .WithText(string.Empty) // Assign text later
        .OnMouseButtonUp(MouseButtonLift)
        ;

    public override void Initialize()
    {
        base.Initialize();

        window = (Window)windows[nameof(TextBoxScript)];
        label = window.Get<LabelComponent>();
    }

    public void Show(string text)
    {
        window.IsVisible = true;
        label.Text = text;
    }

    private void MouseButtonLift(IWindow window, MouseButtonInteraction e)
    {
        this.window.IsVisible = false;
        label.Text = string.Empty;
    }
}
