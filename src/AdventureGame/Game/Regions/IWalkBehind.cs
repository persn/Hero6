// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWalkBehind.cs" company="LateStartStudio">
//   Copyright (C) LateStartStudio
//   This file is subject to the terms and conditions of the MIT license specified
//   in the file 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>
// <summary>
//   Defines the IWalkBehind interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LateStartStudio.AdventureGame.Game.Regions
{
    /// <summary>
    /// This interface defines common functionality for walk behind logic in rooms.
    /// </summary>
    public interface IWalkBehind
    {
        /// <summary>
        /// Gets or sets the baseline for use with a walk behind area.
        /// </summary>
        /// <value>
        /// The baseline for use with a walk behind area.
        /// </value>
        int Baseline { get; set; }
    }
}
