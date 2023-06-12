// <copyright file="IScriptRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

public interface IScriptRepository
{
    /// <summary>
    /// Gets all the <see cref="Script"/> instances.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Script> GetScripts();

    /// <summary>
    /// Gets all the <see cref="ScriptMethod"/> instances.
    /// </summary>
    /// <returns></returns>
    IEnumerable<ScriptMethod> GetScriptMethods();

    /// <summary>
    /// Gets all the <see cref="ScriptMethod"/> instances associated with the
    /// entity of the input name.
    /// </summary>
    /// <param name="entityName">The name of the entity associated with this script.</param>
    /// <returns></returns>
    IEnumerable<ScriptMethod> GetScriptMethods(string entityName);
}
