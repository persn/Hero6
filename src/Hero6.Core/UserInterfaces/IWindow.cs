// <copyright file="IWindow.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.UserInterfaces;

public interface IWindow : IUserInterface, INameable
{
}

public interface IWindowDesigner : IUserInterfaceDesigner<IWindow, IWindowDesigner>, IWithChildDesigner<IWindowDesigner>
{
    IWindowDesigner IsDialog(bool isDialog);
}

public interface IWithChildDesigner<T>
{
    T WithText(string text);

    T WithImage(string sprite, Action<IImageDesigner> designer = null);

    T WithStackPanel(Action<IStackPanelDesigner> designer);

    T WithButton(Action<IButtonDesigner> designer);
}
