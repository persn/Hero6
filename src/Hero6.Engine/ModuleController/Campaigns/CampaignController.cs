// <copyright file="CampaignController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Rooms;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    /// <summary>
    /// API for a campaign controller.
    /// </summary>
    public class CampaignController : GameController<ICampaignController, ICampaignModule>, ICampaignController
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CampaignController"/> instance.
        /// </summary>
        /// <param name="module">The module corresponding to this controller.</param>
        public CampaignController(ICampaignModule module, IServiceProvider services)
            : base(module, services)
        {
        }

        public override int Width => 0;

        public override int Height => 0;

        //public ICharacter Player { get; set; }

        public override void Initialize()
        {
            PreInitialize();

            base.Initialize();
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
        }

        public override void Draw(GameTime time)
        {
        }
    }
}
