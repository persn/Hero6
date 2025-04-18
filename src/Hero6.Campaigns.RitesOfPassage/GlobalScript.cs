// <copyright file="GlobalScript.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage;

internal class GlobalScript(IMouse mouse, IVerbHandler verbHandler) : Script
{
    private readonly IMouse mouse = mouse;

    public override void Initialize()
    {
        // Setup game entities for game start
        API.Characters.Hero().SetAsPlayer();

        // Setup UI for game start
        //API.Windows.StatusBar().IsVisible = true;
        mouse.Cursor = API.Cursors.Walk();
        mouse.ButtonPress += MouseOnLeftButtonPress;
        mouse.ButtonPress += MouseOnMiddleButtonPress;
        mouse.ButtonPress += MouseOnRightButtonPress;
    }

    private void MouseOnLeftButtonPress(object s, MouseButtonInteraction e)
    {
        var cursors = API.Cursors;

        if (e.Button == MouseButton.Left)
        {
            if (mouse.Cursor == cursors.Walk())
                verbHandler.Interact(mouse.X, mouse.Y, Verb.Move);
            else if (mouse.Cursor == cursors.Look())
                verbHandler.Interact(mouse.X, mouse.Y, Verb.Look);
            else if (mouse.Cursor == cursors.Hand())
                verbHandler.Interact(mouse.X, mouse.Y, Verb.Grab);
            else if (mouse.Cursor == cursors.Talk())
                verbHandler.Interact(mouse.X, mouse.Y, Verb.Talk);
        }
    }

    private void MouseOnMiddleButtonPress(object s, MouseButtonInteraction e)
    {
        if (e.Button == MouseButton.Middle)
            mouse.Cursor = API.Cursors.Walk();
    }

    private void MouseOnRightButtonPress(object s, MouseButtonInteraction e)
    {
        if (e.Button == MouseButton.Right)
        {
            var cursor = mouse.Cursor;
            var cursors = API.Cursors;

            if (cursor == cursors.Walk())
                mouse.Cursor = cursors.Look();
            else if (cursor == cursors.Look())
                mouse.Cursor = cursors.Hand();
            else if (cursor == cursors.Hand())
                mouse.Cursor = cursors.Talk();
            else if (cursor == cursors.Talk())
                mouse.Cursor = cursors.Walk();
        }
    }
}
