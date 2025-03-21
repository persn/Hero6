// <copyright file="CharacterRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Collections;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Characters;

internal class CharacterRepository(IRepository<ICharacter> characters) : ICharacterRepository
{
    private readonly IRepository<ICharacter> characters = characters;

    public ICharacter Player => Character.Player;

    public ICharacter this[string name] => characters[name];

    public void Add(ICharacter item) => characters.Add(item);

    public void AddRange(IEnumerable<ICharacter> items) => characters.AddRange(items);

    public IEnumerator<ICharacter> GetEnumerator() => characters.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => characters.GetEnumerator();
}
