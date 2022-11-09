// <copyright file="IScriptRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

public interface IScriptRepository
{
    IEnumerable<Script> GetScripts();

    IEnumerable<ScriptMethod> GetScriptMethods();

    IEnumerable<ScriptMethod> GetScriptMethods(Type type);
}
