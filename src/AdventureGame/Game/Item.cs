// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Item.cs" company="LateStartStudio">
//   Copyright (C) LateStartStudio
//   This file is subject to the terms and conditions of the MIT license specified
//   in the file 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>
// <summary>
//   Defines the Item type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LateStartStudio.AdventureGame.Game
{
    using System;
    using AdventureGame;
    using Engine.Graphics;
    using Regions;

    /// <summary>
    /// A class that represents an item in a game.
    /// </summary>
    public abstract class Item : AdventureGameElement, IWalkBehind
    {
        private readonly string spriteID;

        private Texture2D sprite;
        private int baselineOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="campaign">The campaign this item belongs to.</param>
        /// <param name="spriteID">The ID of the item sprite.</param>
        protected Item(Campaign campaign, string spriteID) : base(campaign)
        {
            this.spriteID = spriteID;
        }

        /// <summary>
        /// Raised when the player interacts with the item by looking, examining, or other
        /// equivalents.
        /// </summary>
        public event EventHandler<EventArgs> Look;

        /// <summary>
        /// Raised when the player interacts with the item by grabbing, taking, or other
        /// equivalents.
        /// </summary>
        public event EventHandler<EventArgs> Grab;

        /// <summary>
        /// Raised when the player interacts with the item by talking, asking, or other
        /// equivalents.
        /// </summary>
        public event EventHandler<EventArgs> Talk;

        /// <inheritdoc />
        public override sealed int Width => this.sprite.Width;

        /// <inheritdoc />
        public override sealed int Height => this.sprite.Height;

        /// <summary>
        /// Gets or sets the item's base line for walk behind areas. If the item's base line is
        /// below the base line of the walk behind area, then the item should render in front, if
        /// else, the walk behind area should render in front.
        /// </summary>
        /// <value>
        /// The item's base line.
        /// </value>
        public int Baseline
        {
            get { return this.Location.Y + this.baselineOffset; }
            set { this.baselineOffset = value; }
        }

        /// <inheritdoc />
        public override sealed bool Interact(int x, int y, Interaction interaction)
        {
            if (!this.IsVisible)
            {
                return false;
            }

            Rectangle rect = new Rectangle(
                this.Location.X,
                this.Location.Y,
                this.Width,
                this.Height);

            if (!rect.Contains(x, y))
            {
                return false;
            }

            switch (interaction)
            {
                case Interaction.Eye:
                    this.Look?.Invoke(this, EventArgs.Empty);
                    break;
                case Interaction.Hand:
                    this.Grab?.Invoke(this, EventArgs.Empty);
                    break;
                case Interaction.Mouth:
                    this.Talk?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    throw new NotSupportedException(
                              $"Interaction {interaction} is not supported on items.");
            }

            return true;
        }

        /// <inheritdoc />
        public override sealed void Load()
        {
            this.sprite = this.Content.LoadTexture2D(this.spriteID);
        }

        /// <inheritdoc />
        public override sealed void Unload()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override sealed void Update(
            TimeSpan totalTime,
            TimeSpan elapsedTime,
            bool isRunningSlowly)
        {
        }

        /// <inheritdoc />
        public override sealed void Draw(
            TimeSpan totalTime,
            TimeSpan elapsedTime,
            bool isRunningSlowly)
        {
            if (this.IsVisible)
            {
                Campaign.Engine.Graphics.Draw(this.sprite, this.Location);
            }
        }
    }
}
