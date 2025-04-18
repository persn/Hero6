// <copyright file="Cursors.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.UserInterfaces;

internal class Cursors : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<ICursorDesigner>("Arrow")
        .WithSprite("Gui/Sierra Vga/Cursors/Arrow")

        .Add<ICursorDesigner>("Hand")
        .WithSprite("Gui/Sierra Vga/Cursors/Hand")

        .Add<ICursorDesigner>("Look")
        .WithSprite("Gui/Sierra Vga/Cursors/Look")

        .Add<ICursorDesigner>("Talk")
        .WithSprite("Gui/Sierra Vga/Cursors/Talk")

        .Add<ICursorDesigner>("Wait")
        .WithSprite("Gui/Sierra Vga/Cursors/Wait")

        .Add<ICursorDesigner>("Walk")
        .WithSprite("Gui/Sierra Vga/Cursors/Walk")
        ;
}
