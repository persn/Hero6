// <copyright file="Component.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Components;

internal interface IComponent
{
    event EventHandler<EventArgs> EnabledChanged;

    bool Enabled { get; set; }
}

internal class Component : IComponent
{
    private bool enabled;

    public event EventHandler<EventArgs> EnabledChanged;

    public Composite Parent { get; internal set; }

    public bool Enabled
    {
        get => enabled;
        set
        {
            if (enabled != value)
            {
                enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public virtual void Initialize()
    {
    }

    public virtual void Update()
    {
    }
}

internal class Composite : IComponent
{
    private readonly List<Component> components = new();

    private bool enabled;

    protected Composite(params IComponent[] components)
    {
        foreach (var component in components.Cast<Component>())
        {
            this.components.Add(component);
            component.Parent = this;
        }
    }

    public event EventHandler<EventArgs> EnabledChanged;

    public bool Enabled
    {
        get => enabled;
        set
        {
            if (enabled != value)
            {
                enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public IEnumerable<IComponent> Components => components;

    public TComponent Get<TComponent>() where TComponent : IComponent
    {
        return components.OfType<TComponent>().SingleOrDefault();
    }

    public virtual void Initialize()
    {
        foreach (var component in components)
            component.Initialize();
    }

    public virtual void Update()
    {
        if (Enabled)
        {
            foreach (var component in components.Where(c => c.Enabled))
                component.Update();
        }
    }
}
