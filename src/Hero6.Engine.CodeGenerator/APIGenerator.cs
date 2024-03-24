// <copyright file="APIGenerator.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;

namespace LateStartStudio.Hero6.Generators
{
    [Generator]
    public class APIGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            GenerateEntityAPI(context, "Animation");
            GenerateEntityAPI(context, "CharacterAnimation", "Animation");
            GenerateEntityAPI(context, "Character");
            GenerateEntityAPI(context, "InventoryItem");
            GenerateEntityAPI(context, "Item");
            GenerateEntityAPI(context, "Room");
        }

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity)
            => GenerateEntityAPI(context, entity, entity);

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity, string namespaceOverride)
        {
            var fileExtension = $".{entity.ToLower()}.json";
            var entityData = context.AdditionalTextsProvider
                .Where(t => t.Path.EndsWith(fileExtension))
                .Select((text, cancellationToken) => (
                    fileName: Path.GetFileNameWithoutExtension(text.Path.Replace(fileExtension, string.Empty)),
                    content: JsonConvert.DeserializeObject<ExpandoObject>(text.GetText().ToString())))
                .Collect();

            context.RegisterSourceOutput(entityData, (spc, entities) =>
            {
                var sb = new StringBuilder();
                sb.AppendLine(
$@"using Microsoft.Extensions.DependencyInjection;
using LateStartStudio.Hero6.{namespaceOverride}s;
using LateStartStudio.Hero6.DependencyInjection;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.API;

public static class {entity}s
{{");
                foreach (var (fileName, content) in entities)
                {
                    dynamic dynamicContent = content;

                    try
                    {
                        _ = dynamicContent.Name;
                    }
                    catch
                    {
                        throw new Exception($@"Entity {fileName} is missing a valid Name from the json.");
                    }

                    sb.AppendLine(
$@"     public const string {fileName}Name = ""{dynamicContent.Name}"";
     public static I{entity} {fileName} => {entity}Repository[{fileName}Name];");
                }

                sb.AppendLine(
$@"     private static IRepository<I{entity}> {entity}Repository => GlobalServiceProvider.Instance.GetService<IRepository<I{entity}>>();
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
