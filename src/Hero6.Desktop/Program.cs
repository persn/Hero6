// <copyright file="Program.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6;

await using var game = Hero6GameBuilder
    .Create()
    .ConfigureServices(LateStartStudio.Hero6.Campaigns.RitesOfPassage.Startup.ConfigureServices)
    .Build();

await game
    ////.Configure()
    .RunAsync();
