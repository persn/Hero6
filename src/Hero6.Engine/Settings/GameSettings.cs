// <copyright file="GameSettings.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;

namespace LateStartStudio.Hero6.Settings
{
    public class GameSettings(IRepository<IWindow> windows) : IGameSettings
    {
        private readonly IRepository<IWindow> windows = windows;

        private bool isPaused = false;

        public int NativeWidth => 320;

        public int NativeHeight => 240;

        public string UserFilesDir => $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Hero6/";

        public bool IsPaused
        {
            get { return isPaused || windows.Where(w => w.IsVisible && w.PauseGame).Any(); }
            set { isPaused = value; }
        }

        public PointF WindowScale { get; set; }
    }
}
