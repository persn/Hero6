// <copyright file="IVerbHandler.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

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
