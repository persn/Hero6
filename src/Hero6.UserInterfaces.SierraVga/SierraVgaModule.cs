// <copyright file="SierraVgaModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.UserInterfaces;
using LateStartStudio.Hero6.Services.UserInterfaces.Input.Mouse;
using LateStartStudio.Hero6.UserInterfaces.SierraVga.Input.Mouse;
using LateStartStudio.Hero6.UserInterfaces.SierraVga.Windows;
using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.UserInterfaces.SierraVga
{
    public class SierraVgaModule : UserInterfaceModule
    {
        private readonly IMouse mouse;
        private readonly IVerbHandler verbHandler;

        public SierraVgaModule(IMouse mouse, IVerbHandler verbHandler)
        {
            this.mouse = mouse;
            this.verbHandler = verbHandler;
        }

        public override string Name => "Sierra VGA";

        public override void Initialize()
        {
            base.Initialize();
            GetWindow<StatusBar>().IsVisible = true;
            mouse.Cursor = GetCursor<Walk>();
            mouse.ButtonPress += MouseOnLeftButtonPress;
            mouse.ButtonPress += MouseOnMiddleButtonPress;
            mouse.ButtonPress += MouseOnRightButtonPress;
        }

        public override void ShowTextBox(string text)
        {
            var textBox = (TextBox)GetWindow<TextBox>();
            textBox.Text = text;
            textBox.IsVisible = true;
        }

        private void MouseOnLeftButtonPress(object s, MouseButtonInteraction e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (mouse.Cursor.Equals<Walk>())
                {
                    verbHandler.Interact(mouse.X, mouse.Y, Verb.Move);
                }
                else if (mouse.Cursor.Equals<Look>())
                {
                    verbHandler.Interact(mouse.X, mouse.Y, Verb.Look);
                }
                else if (mouse.Cursor.Equals<Hand>())
                {
                    verbHandler.Interact(mouse.X, mouse.Y, Verb.Grab);
                }
                else if (mouse.Cursor.Equals<Talk>())
                {
                    verbHandler.Interact(mouse.X, mouse.Y, Verb.Talk);
                }
            }
        }

        private void MouseOnMiddleButtonPress(object s, MouseButtonInteraction e)
        {
            if (e.Button == MouseButton.Middle)
            {
                mouse.Cursor = GetCursor<Walk>();
            }
        }

        private void MouseOnRightButtonPress(object s, MouseButtonInteraction e)
        {
            if (e.Button == MouseButton.Right)
            {
                var cursor = mouse.Cursor;

                if (cursor.Equals<Walk>())
                {
                    mouse.Cursor = GetCursor<Look>();
                }
                else if (cursor.Equals<Look>())
                {
                    mouse.Cursor = GetCursor<Hand>();
                }
                else if (cursor.Equals<Hand>())
                {
                    mouse.Cursor = GetCursor<Talk>();
                }
                else if (cursor.Equals<Talk>())
                {
                    mouse.Cursor = GetCursor<Walk>();
                }
            }
        }
    }
}
