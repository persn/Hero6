// <copyright file="LabelComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Gum.Wireframe;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Settings;
using MonoGameGum;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;

namespace LateStartStudio.Hero6.UserInterfaces;

internal class LabelComponent(IGameSettings gameSettings, bool isRoot) : Component, IUIComponent, ILabel
{
    private readonly IGameSettings gameSettings = gameSettings;
    private readonly bool isRoot = isRoot;

    private Label label;
    private TextRuntime label2;

    public event EventHandler<MouseButtonInteraction> MouseButtonUp;

    public bool IsVisible
    {
        get => label.IsVisible;
        set => label.IsVisible = value;
    }

    public bool PauseGame { get; set; }

    public int Width => (int)label.Width;

    public int Height => (int)label.Height;

    public string Text
    {
        get => label.Text;
        set => label.Text = value;
    }

    public override void Initialize()
    {
        label2 = new();

        label = new();
        //label.Text = "efewkwef";
        label.X = (gameSettings.NativeWidth * gameSettings.WindowScale.X) / 2;
        label.Y = (gameSettings.NativeHeight * gameSettings.WindowScale.Y) / 2;
        label.Visual.XOrigin = HorizontalAlignment.Center;
        label.Visual.YOrigin = VerticalAlignment.Center;
        //label.Visual.Font = "Arial";
        //label.Visual.Font = "Gui/Sierra Vga/Fonts/DAYROM_11.25_Regular";
        //label.Visual.FontSize = 18;
        label.Visual.Click += (_, e) =>
        {
            if (MouseButtonUp != null)
            {
                var inputE = (InputEventArgs)e;
                var mouse = (MonoGameGum.Input.Cursor)inputE.InputDevice;
                MouseButton button = MouseButton.Left;
                if (mouse.PrimaryClick)
                    button = MouseButton.Left;
                else if (mouse.SecondaryClick)
                    button = MouseButton.Right;
                else if (mouse.MiddleClick)
                    button = MouseButton.Middle;

                MouseButtonUp(_, new MouseButtonInteraction(mouse.X, mouse.Y, button));
            }
        };

        if (isRoot)
            label.Visual.AddToRoot();

        base.Initialize();
    }
}
