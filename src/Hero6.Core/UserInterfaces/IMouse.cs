// <copyright file="IMouse.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces
{
    /// <summary>
    /// A state representing a mouse button.
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// Left mouse button.
        /// </summary>
        Left,

        /// <summary>
        /// Middle mouse button, most commonly the scroll wheel click.
        /// </summary>
        Middle,

        /// <summary>
        /// Right mouse button.
        /// </summary>
        Right,
    }

    /// <summary>
    /// Interface containing basic core functionality of the mouse unit.
    /// </summary>
    public interface IMouse
    {
        /// <summary>
        /// Occurs when the mouse has changed x or y position.
        /// </summary>
        event EventHandler<MouseMove> Move;

        /// <summary>
        /// Occurs when any mouse button is pressed down.
        /// </summary>
        event EventHandler<MouseButtonInteraction> ButtonPress;

        /// <summary>
        /// Occurs when any mouse button is held down.
        /// </summary>
        event EventHandler<MouseButtonInteraction> ButtonHold;

        /// <summary>
        /// Occurs when any mouse button is lifted up.
        /// </summary>
        event EventHandler<MouseButtonInteraction> ButtonLift;

        /// <summary>
        /// Gets or sets mouse cursor.
        /// </summary>
        ICursor Cursor { get; set; }

        /// <summary>
        /// Gets or sets the x coordinate of the mouse.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate of the mouse.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets the scroll wheel value.
        /// </summary>
        int ScrollWheel { get; }

        /// <summary>
        /// Center the mouse in the middle of the window.
        /// </summary>
        void Center();

        /// <summary>
        /// Saves a backup of the cursor.
        /// </summary>
        void SaveCursor();

        /// <summary>
        /// Loads the backup of the cursor.
        /// </summary>
        void LoadCursor();
    }

    /// <summary>
    /// The <see cref="MouseButtonInteraction"/> <see cref="EventArgs"/>.
    /// </summary>
    public class MouseButtonInteraction : EventArgs
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="MouseButtonInteraction"/> <see cref="EventArgs"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="button">The buttons used for this interaction.</param>
        public MouseButtonInteraction(int x, int y, MouseButton button)
        {
            X = x;
            Y = y;
            Y = y;
            Button = button;
        }

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets the button used for this interaction.
        /// </summary>
        public MouseButton Button { get; }
    }

    /// <summary>
    /// Mouse movement <see cref="EventArgs"/>.
    /// </summary>
    public class MouseMove : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MouseMove"/>.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        public MouseMove(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the x position.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the y position.
        /// </summary>
        public int Y { get; }
    }
}
