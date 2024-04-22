// <copyright file="HotspotsScriptComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.Scripts;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Rooms;

internal class HotspotsScriptComponent : Component, IScriptComponent<HotspotEvent>
{
    private readonly string source;
    private readonly Dictionary<(HotspotEvent, Color), Func<Task>> events;
    private readonly ICharacterRepository characterRepository;

    private Color[,] buffer;

    public HotspotsScriptComponent(string source, Dictionary<(HotspotEvent, Color), Func<Task>> events, ICharacterRepository characterRepository)
    {
        this.source = source;
        this.events = events;
        this.characterRepository = characterRepository;
    }

    public bool Invoke(int x, int y, HotspotEvent @event)
    {
        var pixel = buffer[x, y];
        var res = events.TryGetValue((@event, pixel), out var scriptMethod);
        if (res)
            _ = scriptMethod?.Invoke();
        return res;
    }

    public override void Update(Microsoft.Xna.Framework.GameTime time)
    {
        base.Update(time);
        var currentRoom = characterRepository.Player.Room;

        foreach (var character in currentRoom.Characters.Cast<Character>().ToArray())
            StandingOn(character);
    }

    internal void Load(Func<string, Texture2D> load)
    {
        var texture = load(source);
        buffer = CopyTextureData(texture);
    }

    private static Color[,] CopyTextureData(Texture2D texture)
    {
        var result = new Color[texture.Width, texture.Height];
        var data = new Microsoft.Xna.Framework.Color[texture.Width * texture.Height];
        texture.GetData(data);

        for (var y = 0; y < texture.Height; y++)
        {
            for (var x = 0; x < texture.Width; x++)
            {
                result[x, y] = data[(y * texture.Width) + x].ToDotNet();
            }
        }

        return result;
    }

    private void StandingOn(Character character)
    {
        Invoke(character.FeetX, character.FeetY, HotspotEvent.StandingOn);
    }
}
