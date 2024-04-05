// <copyright file="HotspotsScriptComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using System.Globalization;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Scripts;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Rooms;

internal class HotspotsScriptComponent : Component, IScriptComponent<HotspotEvent>
{
    private readonly string source;
    private readonly IScriptRepository repository;
    private readonly ICharacterRepository characterRepository;
    private readonly Dictionary<(HotspotEvent, Color), ScriptMethod> events = new();

    private Color[,] buffer;

    public HotspotsScriptComponent(string source, IScriptRepository repository, ICharacterRepository characterRepository)
    {
        this.source = source;
        this.repository = repository;
        this.characterRepository = characterRepository;
    }

    public bool Invoke(int x, int y, HotspotEvent @event) => Invoke(x, y, @event, s => s.Invoke());

    public override void Initialize()
    {
        base.Initialize();

        if (Parent is INameable nameable)
        {
            foreach (var scriptMethod in repository.GetScriptMethods(nameable.Name))
            {
                if (scriptMethod.Attribute is HotspotEventAttribute hotspotAttribute)
                {
                    var color = Color.FromArgb(int.Parse(hotspotAttribute.Id.Replace("#", string.Empty), NumberStyles.HexNumber));
                    events[(hotspotAttribute.Event, color)] = scriptMethod;
                }
            }
        }
    }

    public override void Update(Microsoft.Xna.Framework.GameTime time)
    {
        base.Update(time);
        var currentRoom = characterRepository.Player.Room;

        foreach (var character in currentRoom.Characters.Cast<Character>())
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
        Invoke(character.FeetX, character.FeetY, HotspotEvent.StandingOn, s => s.Invoke(new StandingOn(character)));
    }

    private bool Invoke(int x, int y, HotspotEvent @event, Action<ScriptMethod> doScriptMethod)
    {
        var pixel = buffer[x, y];
        var res = events.TryGetValue((@event, pixel), out var scriptMethod);
        if (res)
            doScriptMethod.Invoke(scriptMethod);
        return res;
    }
}
