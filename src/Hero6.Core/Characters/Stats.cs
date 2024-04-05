// <copyright file="Stats.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Characters;

public class Stats
{
    public const int Cap = 100;

    internal Stats()
    {
    }

    public Stat Health { get; }

    public Stat Stamina { get; }

    public Stat Mana { get; }

    public Stat Humans { get; }

    public Stat Sidhe { get; }

    public Stat Giants { get; }

    public LearningStat Strength { get; }

    public LearningStat Intelligence { get; }

    public LearningStat Agility { get; }

    public LearningStat Vitality { get; }

    public LearningStat Luck { get; }

    public LearningStat WeaponUse { get; }

    public LearningStat Parry { get; }

    public LearningStat Dodge { get; }

    public LearningStat Stealth { get; }

    public LearningStat LockPicking { get; }

    public LearningStat Throwing { get; }

    public LearningStat Climbing { get; }

    public LearningStat Magic { get; }
}
