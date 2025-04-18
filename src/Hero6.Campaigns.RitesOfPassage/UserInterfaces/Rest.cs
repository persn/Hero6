// <copyright file="Rest.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.UserInterfaces;

internal class Rest : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<IWindowDesigner>("Rest")
        .WithStackPanel(designer => designer
            .WithOrientation(Orientation.Vertical)
            .WithButton(designer => designer
                .WithText("10 minutes")
                .OnMouseButtonUp(TenBtnOnMouseButtonUp))
            .WithButton(designer => designer
                .WithText("30 minutes")
                .OnMouseButtonUp(ThirtyBtnOnMouseButtonUp))
            .WithButton(designer => designer
                .WithText("60 minutes")
                .OnMouseButtonUp(SixtyBtnOnMouseButtonUp))
            .WithButton(designer => designer
                .WithText("Sleep")
                .OnMouseButtonUp(SleepBtnOnMouseButtonUp))
            .WithButton(designer => designer
                .WithText("Cancel")
                .OnMouseButtonUp(CancelBtnOnMouseButtonUp)))
        ;

    private void TenBtnOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.Rest().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void ThirtyBtnOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.Rest().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void SixtyBtnOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.Rest().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void SleepBtnOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.Rest().IsVisible = false;
        API.ShowMessage("Work In Progress");
    }

    private void CancelBtnOnMouseButtonUp(IButton button, MouseButtonInteraction e)
    {
        API.Windows.Rest().IsVisible = false;
    }
}
