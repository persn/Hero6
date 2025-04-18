// <copyright file="VerbHandler.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Verbs;

internal class VerbHandler(ICharacterRepository characterRepository, IGameSettings gameSettings) : IVerbHandler
{
    private readonly ICharacterRepository characterRepository = characterRepository;
    private readonly IGameSettings gameSettings = gameSettings;

    public bool Interact(int x, int y, Verb verb)
    {
        if (gameSettings.IsPaused)
            return false;

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
