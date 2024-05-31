// <copyright file="IRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Repositories;

public interface IRepository<T> : IEnumerable<T> where T : INameable
{
    T this[string name] { get; }

    void Add(T item);

    void AddRange(params T[] items) => AddRange(items.AsEnumerable());

    void AddRange(IEnumerable<T> items);

    void Remove(T item);
}
