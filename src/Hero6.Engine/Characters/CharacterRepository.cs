// <copyright file="CharacterRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Characters;

internal class CharacterRepository : Repository<ICharacter>, ICharacterRepository
{
    public CharacterRepository(IEnumerable<ICharacter> items) : base(items)
    {
    }

    public ICharacter Player => Character.Player;
}
