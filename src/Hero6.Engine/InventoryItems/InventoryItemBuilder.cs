// <copyright file="InventoryItemBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.InventoryItems;

internal class InventoryItemBuilder : Builder<IInventoryItem, InventoryItem>, IInventoryItemBuilder
{
    private readonly InventoryItemData data = new();

    public InventoryItemBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    protected override object[] BuildParams() => [data];
}
