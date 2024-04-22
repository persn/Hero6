// <copyright file="IDesigner.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

/// <summary>
/// Used for adding and editing entities at design-time which will then be populated into
/// <see cref="LateStartStudio.Hero6.Repositories.IRepository{T}"/> instances that can be used to fetch them later.
/// Because this is used to compose all the data that the game uses it must be run before the game starts.
/// </summary>
public interface IDesigner
{
    IDesigner Add<TBuilder>(string name, Action<TBuilder> builder) where TBuilder : IBuilder;
}

public interface IBuilder
{
}
