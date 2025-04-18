// <copyright file="VerbBar.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.UserInterfaces;

internal class VerbBar(IMouse mouse) : Script
{
    private readonly IMouse mouse = mouse;

    public override void Design(IDesigner designer) => designer
        .Add<IWindowDesigner>("Verb Bar")
        .WithStackPanel(designer => designer
            .WithImage("Gui/Sierra Vga/Verb Bar/Side Left")
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Walk Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Walk Dark"))
                .OnMouseButtonUp(WalkBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Look Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Look Dark"))
                .OnMouseButtonUp(LookBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Hand Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Hand Dark"))
                .OnMouseButtonUp(HandBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Talk Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Talk Dark"))
                .OnMouseButtonUp(TalkBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Sub Menu Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Sub Menu Dark"))
                .OnMouseButtonUp(SubMenuBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Magic Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Magic Dark"))
                .OnMouseButtonUp(MagicBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Current Item Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Current Item Dark"))
                .OnMouseButtonUp(CurrentItemBtnOnLeftMouseButtonUp))
            .WithButton(designer => designer
                .WithImage("Gui/Sierra Vga/Verb Bar/Inventory Light", designer => designer
                    .WithHoverImage("Gui/Sierra Vga/Verb Bar/Inventory Dark"))
                .OnMouseButtonUp(InventoryBtnOnLeftMouseButtonUp))
            .WithImage("Gui/Sierra Vga/Verb Bar/Side Right"))
        ;

    private void WalkBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        ChangeMouseCursor(API.Cursors.Walk());
    }

    private void LookBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        ChangeMouseCursor(API.Cursors.Look());
    }

    private void HandBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        ChangeMouseCursor(API.Cursors.Hand());
    }

    private void TalkBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        ChangeMouseCursor(API.Cursors.Talk());
    }

    private void MagicBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        mouse.Center();
        API.ShowMessage("Work In Progress");
    }

    private void CurrentItemBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        mouse.Center();
        API.ShowMessage("Work In Progress");
    }

    private void SubMenuBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        mouse.Center();
        API.Windows.ExtensionBar().IsVisible = true;
    }

    private void InventoryBtnOnLeftMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        if (e.Button != MouseButton.Left)
            return;

        mouse.Center();
        API.ShowMessage("Work In Progress");
    }

    private void ChangeMouseCursor(ICursor cursor)
    {
        mouse.Cursor = cursor;
        mouse.SaveCursor();
        mouse.Y = API.Windows.VerbBar().Height;
    }
}
