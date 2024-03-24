// <copyright file="Component.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Components;

internal interface IComponent
{
    event EventHandler<EventArgs> EnabledChanged;

    bool Enabled { get; set; }
}

internal interface ILoadable
{
    void Load();
}

internal interface IDrawable
{
    void Draw(GameTime gameTime);
}

internal class Component : IComponent
{
    private bool enabled = true;

    public event EventHandler<EventArgs> EnabledChanged;

    public virtual Composite Parent { get; internal set; }

    public virtual bool Enabled
    {
        get => enabled && Parent.Enabled;
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

    public virtual void Update(GameTime time)
    {
    }
}

internal class Composite : IComponent
{
    private readonly List<Component> components = new();

    private bool enabled = false;

    protected Composite(params IComponent[] components)
    {
        foreach (var component in components)
        {
            Add(component);
        }
    }

    public event EventHandler<EventArgs> EnabledChanged;

    public virtual bool Enabled
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

    public virtual void Add(IComponent component)
    {
        if (component is Component c)
        {
            components.Add(c);
            c.Parent = this;
        }
    }

    public virtual void Remove(IComponent component)
    {
        if (component is Component c)
        {
            components.Remove(c);
            c.Parent = null;
        }
    }

    public virtual void Initialize()
    {
        foreach (var component in components)
            component.Initialize();
    }

    public virtual void Load()
    {
        foreach (var component in components.OfType<ILoadable>())
            component.Load();
    }

    public virtual void Update(GameTime time)
    {
        if (Enabled)
        {
            foreach (var component in components.Where(c => c.Enabled))
                component.Update(time);
        }
    }

    public virtual void Draw(GameTime time)
    {
        if (Enabled)
        {
            foreach (var component in components.Where(c => c.Enabled).OfType<IDrawable>())
                component.Draw(time);
        }
    }
}
