// <copyright file="GameEventException.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Reflection;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    public class GameEventException : Exception
    {
        public GameEventException(string message, MethodInfo method)
            : base($@"Couldn't game event""{method.DeclaringType.FullName}.{method.Name}"". {message}")
        {
        }
    }
}
