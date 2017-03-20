// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WalkBehindArea.cs" company="LateStartStudio">
//   Copyright (C) LateStartStudio
//   This file is subject to the terms and conditions of the MIT license specified
//   in the file 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>
// <summary>
//   Defines the WalkBehindArea type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LateStartStudio.AdventureGame.Game.Regions
{
    /// <summary>
    /// The <see cref="WalkBehindArea"/> class, which represents a area in the room that can be
    /// rendered in front of objects.
    /// </summary>
    public class WalkBehindArea : IWalkBehind
    {
        /// <summary>
        /// Gets or sets the base line of the walk area. If this base line is lower than the base
        /// line of any intersecting objects it will render in front of said object, it will render
        /// in back if else.
        /// </summary>
        /// <value>
        /// The base line of the walk area. If this base line is lower than the base line of any
        /// intersecting objects it will render in front of said object, it will render in back if
        /// else.
        /// </value>
        public int Baseline { get; set; }
    }
}
