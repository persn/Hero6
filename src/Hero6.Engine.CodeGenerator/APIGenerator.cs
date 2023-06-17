// <copyright file="APIGenerator.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace LateStartStudio.Hero6.Generators
{
    [Generator]
    public class APIGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            GenerateEntityAPI(context, "InventoryItem");
            GenerateEntityAPI(context, "Item");
        }

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity)
        {
            var fileExtension = $".{entity.ToLower()}.json";
            var entityData = context.AdditionalTextsProvider
                .Where(t => t.Path.EndsWith(fileExtension))
                .Select((text, cancellationToken) => Path.GetFileNameWithoutExtension(text.Path.Replace(fileExtension, string.Empty)))
                .Collect();

            context.RegisterSourceOutput(entityData, (spc, entities) =>
            {
                var sb = new StringBuilder();
                sb.AppendLine(
$@"using Microsoft.Extensions.DependencyInjection;
using LateStartStudio.Hero6.{entity}s;
using LateStartStudio.Hero6.DependencyInjection;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.API
{{
    public static class {entity}s
    {{");

                foreach (var e in entities)
                {
                    sb.AppendLine(
$@"         public static I{entity} {Path.GetFileName(e).Replace($".{entity.ToLower()}.json", string.Empty)} => {entity}Repository[@""{e}""];");
                }

                sb.AppendLine(
$@"         private static IRepository<I{entity}> {entity}Repository => GlobalServiceProvider.Instance.GetService<IRepository<I{entity}>>();
    }}
}}");

                spc.AddSource($"{entity}sAPI.g.cs", sb.ToString());
            });
        }
    }

    [Generator]
    public class GlobalUsingsAPIGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(callback =>
            {
                callback.AddSource($"GlobalUsings.API.g.cs", SourceText.From("global using LateStartStudio.Hero6.API;", Encoding.UTF8));
            });
        }
    }
}
