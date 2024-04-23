// <copyright file="Startup.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureHero6CampaignsRitesOfPassage();
    }
}
