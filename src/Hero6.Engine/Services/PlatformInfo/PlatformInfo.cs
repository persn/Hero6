// <copyright file="PlatformInfo.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Services.DependencyInjection;

namespace LateStartStudio.Hero6.Services.PlatformInfo
{
    [Injectable]
    public class PlatformInfo : IPlatformInfo
    {
        public Platform Platform
        {
            get
            {
#if DESKTOP
                return Platform.Desktop;
#elif ANDROID
                return Platform.Android
#else
                throw new System.NotSupportedException("Reached unsopported case, does your project config has a preprocessor directive?");
#endif
            }
        }
    }
}
