// <copyright file="Repository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Collections;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Repositories;

internal class Repository<T> : IRepository<T> where T : INameable
{
    private readonly Dictionary<string, T> repository;

    public Repository(Designer designer)
    {
        if (designer is not IBuilder<T> builder)
            throw new InvalidOperationException($@"Builder for type ""{typeof(T).Name}"" has not been registered.");

        repository = new Dictionary<string, T>(builder.Build().Select(t => new KeyValuePair<string, T>(t.Name, t)));
    }

    public T this[string name] => repository[name];

    public void Add(T item) => repository.Add(item.Name, item);

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public IEnumerator<T> GetEnumerator() => repository.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
