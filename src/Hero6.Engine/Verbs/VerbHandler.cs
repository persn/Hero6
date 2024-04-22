// <copyright file="VerbHandler.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Verbs;

public interface IVerbHandler
{
    /// <summary>
    /// Finds and interact with a spot using input coordinates and the verb.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="verb"></param>
    bool Interact(int x, int y, Verb verb);
}

internal class VerbHandler(ICharacterRepository characterRepository) : IVerbHandler
{
    private readonly ICharacterRepository characterRepository = characterRepository;

    public bool Interact(int x, int y, Verb verb)
    {
        if (verb == Verb.Move)
        {
            _ = characterRepository.Player.WalkAsync(x, y);
            return true;
        }

        var currentRoom = (Room)characterRepository.Player.Room;

        if (currentRoom.Characters.Cast<Character>().Any(c => c.Interact(x, y, verb)))
            return true;

        if (currentRoom.Items.Cast<Item>().Any(i => i.Interact(x, y, verb)))
            return true;

        if (currentRoom.Get<IScriptComponent<HotspotEvent>>().Invoke(x, y, verb.ToHotspotEvent()))
            return true;

        return false;
    }
}
