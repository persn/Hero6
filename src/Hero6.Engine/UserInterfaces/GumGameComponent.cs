// <copyright file="GumGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Diagnostics;
using Gum.DataTypes;
using LateStartStudio.Hero6.MonoGame;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;

namespace LateStartStudio.Hero6.UserInterfaces;

internal class GumGameComponent(IRepository<IWindow> windows, MainGame game, IServiceProvider serviceProvider, ILogger<GumGameComponent> logger)
    : DrawableEntityBase<IWindow>(MonoGame.UpdateOrder.UserInterface, MonoGame.DrawOrder.UserInterface, game, windows, serviceProvider)
{
    private readonly ILogger logger = logger;
    private readonly GumService gum = GumService.Default;

    public override void Initialize()
    {
        var sw = Stopwatch.StartNew();
        _ = gum.Initialize(Game);
        var screen = new ScreenSave();

        var stackPanel = new StackPanel();
        //stackPanel.Orientation = MonoGameGum.Forms.Controls.Orientation.Horizontal;
        //gum.Root.AddToRoot()
        //gum.Root.Children.Add(stackPanel);
        //stackPanel.Visual.AddToRoot();
        //stackPanel.IsVisible = false;

        var button1 = new Button();
        button1.Text = "Troll 1";
        stackPanel.AddChild(button1);

        var button2 = new Button();
        button2.Text = "Troll 2";
        stackPanel.AddChild(button2);
        //gum.Root.Children.Add(button);



        //stackPanel.AddToRoot();
        //MonoGameGum.
        //GumService.Default.Cursor.
        var image = new SpriteRuntime();
        image.Texture = Game.Content.Load<Texture2D>("Gui/Sierra Vga/Status Bar/Background");
        //stackPanel.AddChild(image);
        ////foreach (var script in scripts)
        ////    script.DesignGUI();

        //var img = new MonoGameGum.Forms.Controls.
        //img.
        var label = new Label { Text = "efjpowefjpowef" };
        //textRuntime.
        stackPanel.AddChild(label);

        base.Initialize();

        sw.Stop();
        logger.LogInformation("GUI design finished after {time}", sw.Elapsed);
    }

    public override void Update(GameTime gameTime)
    {
        gum.Update(Game, gameTime);
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        gum.Draw();
        base.Draw(gameTime);
    }
}
