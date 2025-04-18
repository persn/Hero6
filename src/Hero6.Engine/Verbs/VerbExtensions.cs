// <copyright file="VerbExtensions.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Rooms;

namespace LateStartStudio.Hero6.Verbs;

public static class VerbExtensions
{
    public static ItemEvent ToItemEvent(this Verb verb) => verb switch
    {
        Verb.Look => ItemEvent.Look,
        Verb.Grab => ItemEvent.Grab,
        Verb.Talk => ItemEvent.Talk,
        _ => throw new Exception(@$"""{nameof(Verb)}"" with value ""{verb}"" cannot be converted to a ""{nameof(ItemEvent)}""."),
    };

    public static CharacterEvent ToCharacterEvent(this Verb interaction) => interaction switch
    {
        Verb.Look => CharacterEvent.Look,
        Verb.Grab => CharacterEvent.Grab,
        Verb.Talk => CharacterEvent.Talk,
        _ => throw new Exception(@$"""{nameof(Verb)}"" with value ""{interaction}"" cannot be converted to a ""{nameof(CharacterEvent)}""."),
    };

    public static HotspotEvent ToHotspotEvent(this Verb interaction) => interaction switch
    {
        Verb.Look => HotspotEvent.Look,
        Verb.Grab => HotspotEvent.Grab,
        Verb.Talk => HotspotEvent.Talk,
        _ => throw new Exception(@$"""{nameof(Verb)}"" with value ""{interaction}"" cannot be converted to a ""{nameof(HotspotEvent)}""."),
    };
}
