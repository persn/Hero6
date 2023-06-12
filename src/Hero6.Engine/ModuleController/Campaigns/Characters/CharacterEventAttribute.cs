// <copyright file="CharacterEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Characters;

public class CharacterEventAttribute : ScriptEventAttribute
{
    public CharacterEventAttribute(string name, CharacterEvent @event) : base(name)
    {
        Event = @event;
    }

    public CharacterEvent Event { get; }
}
