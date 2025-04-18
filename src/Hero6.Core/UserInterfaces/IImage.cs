// <copyright file="IImage.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

public interface IImage : IUserInterface
{
}

public interface IImageDesigner : IUserInterfaceDesigner<IImage, IImageDesigner>
{
    IImageDesigner WithHoverImage(string image);
}
