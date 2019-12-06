// <copyright file="MonoGameCampaigns.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using LateStartStudio.Hero6.ModuleController.Campaigns;
using LateStartStudio.Hero6.MonoGame.GameLoop;
using LateStartStudio.Hero6.Services.DependencyInjection;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Services.Campaigns
{
    [Injectable]
    public class MonoGameCampaigns : ICampaigns, IXnaGameLoop
    {
        private readonly IContainer container;
        private readonly List<CampaignController> campaigns = new List<CampaignController>();

        public MonoGameCampaigns(IContainer container)
        {
            this.container = container;
        }

        public IEnumerable<ICampaignModule> Campaigns => campaigns.Select(c => c.Module);

        public ICampaignModule Current
        {
            get { return CurrentController.Module; }
            set { CurrentController = campaigns.First(c => c.Module == value); }
        }

        public CampaignController CurrentController { get; set; }

        public bool Interact(int x, int y, Interaction interaction) => CurrentController.Interact(x, y, interaction);

        public void Initialize()
        {
            campaigns.AddRange(container
                .Get<IEnumerable<ICampaignModule>>()
                .Select(m => container.Get<CampaignController>(("module", m), ("container", container))));

            CurrentController = campaigns[0];
            CurrentController.Initialize();
        }

        public void Load() => CurrentController.Load();

        public void Unload() => CurrentController.Unload();

        public void Update(GameTime time) => CurrentController.Update(time);

        public void Draw(GameTime time) => CurrentController.Draw(time);
    }
}
