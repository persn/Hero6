// <copyright file="GameController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    /// <summary>
    /// API for all game enttities.
    /// </summary>
    /// <typeparam name="TController">The Controlle type.</typeparam>
    /// <typeparam name="TModule">The Module type.</typeparam>
    public abstract class GameController<TController, TModule> : Controller<TController, TModule>, IGameController
        where TController : IGameController
        where TModule : IGameModule
    {
        /// <summary>
        /// Makes a new instance of the <see cref="GameController{TController,TModule}"/> class.
        /// </summary>
        /// <param name="module">The module corresponding to this controller.</param>
        protected GameController(TModule module, IServiceProvider services)
            : base(module, services)
        {
        }

        /// <summary>
        /// Gets or sets the look event.
        /// </summary>
        public Action Look { get; protected set; }

        /// <summary>
        /// Gets or sets the grab event.
        /// </summary>
        public Action Grab { get; protected set; }

        /// <summary>
        /// Gets or sets the talk event.
        /// </summary>
        public Action Talk { get; protected set; }

        /// <summary>
        /// User interaction with this <see cref="GameController{TController,TModule}"/> instance. Should succeed if
        /// interaction coordinates intersects with this instance and if interation is relevant for this type.
        /// </summary>
        /// <param name="x">The x coordinate of the interaction.</param>
        /// <param name="y">The y coordinate of the interaction.</param>
        /// <param name="interaction">The type of interaction.</param>
        /// <returns>True if interaction was succesful, false if not.</returns>
        public abstract bool Interact(int x, int y, Interaction interaction);

        /// <summary>
        /// Goes through the <see cref="IGameController"/> methods and makes sure they can be invoked
        /// if the return type is void or Task. A game event will always fire off on a new thread.
        /// </summary>
        protected void InitializeGameEvents(ILogger logger, Action<MethodInfo, GameEventAttribute> eventAttribute)
        {
            var scriptEvents = Module
                .GetType()
                .GetMethods()
                .Select(m => (method: m, attribute: m.GetCustomAttribute<GameEventAttribute>()))
                .Where(m => m.attribute != null);

            foreach (var (method, attribute) in scriptEvents)
            {
                if (!(method.ReturnType == typeof(void) || method.ReturnType == typeof(Task)))
                {
                    var message = $@"Return type must be ""void"" or ""{nameof(Task)}"" but currently it's ""{method.ReturnType}"".";
                    var exception = new GameEventException(message, method);
                    logger.LogError(exception, message);

#if DEBUG
                    throw exception;
#else
                    continue;
#endif
                }

                eventAttribute(method, attribute);
            }
        }

        protected void InvokeGameEvent(MethodInfo method, object[] args) => Task.Run(async () =>
        {
            if (method.ReturnType == typeof(void))
                method.Invoke(Module, args);
            else
                await (Task)method.Invoke(Module, args);
        });
    }
}
