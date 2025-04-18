// <copyright file="StatusBar.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.UserInterfaces;

internal class StatusBar(
    IGameSettings gameSettings,
    IMouse mouse) : Script
{
    private readonly IGameSettings gameSettings = gameSettings;
    private readonly IMouse mouse = mouse;

    public override void Design(IDesigner designer) => designer
        .Add<IWindowDesigner>("Status Bar")
        .IsDialog(false)
        .OnMouseEnter(OnMouseEnter)
        .WithImage("Gui/Sierra Vga/Status Bar/Background")
        ;

    private void OnMouseEnter(IWindow statusBar, EventArgs e)
    {
        // If the game is paused it's probably because some other dialog is showing or event is happening
        if (gameSettings.IsPaused)
            return;

        statusBar.IsVisible = false;
        API.Windows.VerbBar().IsVisible = true;
        mouse.SaveCursor();
        mouse.Cursor = API.Cursors.Arrow();
    }
}
