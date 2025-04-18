// <copyright file="Cursor.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

internal class Cursor : Composite, ICursor
{
    public Cursor(
        CursorData data,
        IVisibleComponent visible,
        IPositionComponent position,
        ISpriteRendererComponent spriteRenderer)
        : base(position, visible, spriteRenderer)
    {
        Name = data.Name;
        spriteRenderer.Sprite = data.Sprite;
    }

    public override string Name { get; }

    public bool IsVisible
    {
        get => Get<IVisibleComponent>().IsVisible;
        set => Get<IVisibleComponent>().IsVisible = value;
    }
}
