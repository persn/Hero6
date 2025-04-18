// <copyright file="ExtensionBar.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.UserInterfaces;

internal class ExtensionBar : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<IWindowDesigner>("ExtensionBar")
        .IsDialog(true)
        .WithStackPanel(designer => designer
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Left")
                .OnMouseButtonUp(SideOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Run Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Extension Bar/Run Dark"))
                .OnMouseButtonUp(RunOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Sneak Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Extension Bar/Sneak Dark"))
                .OnMouseButtonUp(SneakOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Sleep Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Extension Bar/Sleep Dark"))
                .OnMouseButtonUp(SleepOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Stats Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Extension Bar/Stats Dark"))
                .OnMouseButtonUp(StatsOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Time Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Extension Bar/Time Dark"))
                .OnMouseButtonUp(StatsOnMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Extension Bar/Right")
                .OnMouseButtonUp(SideOnMouseButtonUp)))
        ;

    private void SideOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
    }

    private void RunOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void SneakOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void SleepOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
        API.Windows.Rest().IsVisible = true;
    }

    private void StatsOnMouseButtonUp(object s, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void TimeOnMouseButtonUp(object s, MouseButtonInteraction e)
    {
        API.Windows.ExtensionBar().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }
}
