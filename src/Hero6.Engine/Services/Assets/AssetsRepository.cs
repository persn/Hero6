// <copyright file="AssetsRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Services.DependencyInjection;
using Microsoft.Xna.Framework.Content;

namespace LateStartStudio.Hero6.Services.Assets
{
    [Injectable]
    public class AssetsRepository : IAssetsRepository
    {
        private readonly ContentManager assets;

        public AssetsRepository(ContentManager assets)
        {
            this.assets = assets;
        }

        public T Load<T>(string path) => assets.Load<T>(path);
    }
}
