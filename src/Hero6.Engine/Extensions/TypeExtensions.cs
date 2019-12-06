// <copyright file="TypeExtensions.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Linq;

namespace LateStartStudio.Hero6.Extensions
{
    public static class TypeExtensions
    {
        public static T GetCustomAttribute<T>(this Type type) where T : Attribute
        {
            var result = (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();

            if (result == null)
            {
                throw new NullReferenceException($"{typeof(T).Name} could not be found on {type.FullName}. Make sure it is set to the class.");
            }

            return result;
        }
    }
}
