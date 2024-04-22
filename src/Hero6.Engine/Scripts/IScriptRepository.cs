// <copyright file="IScriptRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Scripts;

/// <summary>
/// Wrapper interface because we want to limit access to <see cref="Script"/> instances, game developers
/// doesn't need too much access to the types controlling the scripts.
/// </summary>
internal interface IScriptRepository : IRepository<Script>
{
}

internal class ScriptRepository(IEnumerable<Script> items) : Repository<Script>(items), IScriptRepository
{
}
