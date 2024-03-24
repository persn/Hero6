// <copyright file="EntitiesGenerator.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace LateStartStudio.Hero6.Generators
{
    [Generator]
    public class EntitiesGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var assemblyName = context.CompilationProvider.Select((c, _) => c.AssemblyName.Replace(".", string.Empty));
            var entitiesData = context.AdditionalTextsProvider
                .Where(f => f.Path.EndsWith(".json"))
                .Select((text, cancellationToken) => (
                    fileName: text.Path,
                    content: text.GetText(cancellationToken).ToString()))
                .Combine(assemblyName)
                .Collect();

            context.RegisterSourceOutput(entitiesData, (spc, entities) =>
            {
                if (!entities.Any())
                    return;

                var sb = new StringBuilder();
                sb.AppendLine(
$@"using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Rooms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace LateStartStudio.Hero6;

public static class GeneratedEntities
{{
    public static IHostBuilder ConfigureEntities{entities.Select(e => e.Right).First()}(this IHostBuilder hostBuilder) => hostBuilder
        .ConfigureServices(services =>
        {{");

                foreach (var (fileName, content) in entities.Select(e => e.Left))
                {
                    var dataType = string.Empty;
                    var name = string.Empty;

                    if (fileName.EndsWith(".animation.json"))
                    {
                        dataType = "AnimationData";
                        name = fileName.Replace(".animation.json", string.Empty);
                    }
                    else if (fileName.EndsWith(".characteranimation.json"))
                    {
                        dataType = "CharacterAnimationData";
                        name = fileName.Replace(".characteranimation.json", string.Empty);
                    }
                    else if (fileName.EndsWith(".character.json"))
                    {
                        dataType = "CharacterData";
                        name = fileName.Replace(".character.json", string.Empty);
                    }
                    else if (fileName.EndsWith(".inventoryitem.json"))
                    {
                        dataType = "InventoryItemData";
                        name = fileName.Replace(".inventoryitem.json", string.Empty);
                    }
                    else if (fileName.EndsWith(".item.json"))
                    {
                        dataType = "ItemData";
                        name = fileName.Replace(".item.json", string.Empty);
                    }
                    else if (fileName.EndsWith(".room.json"))
                    {
                        dataType = "RoomData";
                        name = fileName.Replace(".room.json", string.Empty);
                    }
                    else
                    {
                        // Unrecognized file type so just skip
                    }

                    name = Path.GetFileNameWithoutExtension(name);

                    if (!string.IsNullOrEmpty(dataType))
                    {
                        sb.AppendLine(
$@"            services.AddSingleton(s =>
            {{
                var res = JsonSerializer.Deserialize<{dataType}>(@$""{content.SanitizeJson()}"");
                if (string.IsNullOrEmpty(res.Name))
                    res.Name = ""{name}"";
                return res;
            }});");
                    }
                }

                sb = sb.AppendLine(
$@"     }});
}}");

                spc.AddSource("Entities.g.cs", sb.ToString());
            });
        }
    }
}
