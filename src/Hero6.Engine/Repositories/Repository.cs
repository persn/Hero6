// <copyright file="Repository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Repositories;

internal class Repository<T> : IRepository<T> where T : INameable
{
    private readonly Dictionary<string, T> repository = new();

    public Repository(IEnumerable<T> items)
    {
        AddRange(items);
    }

    public T Get(string name)
    {
        repository.TryGetValue(name, out T res);
        return res;
    }

    public IEnumerable<T> GetAll() => repository.Values;

    public void Add(T item) => repository.Add(item.Name, item);

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void Remove(T item) => repository.Remove(item.Name);
}
