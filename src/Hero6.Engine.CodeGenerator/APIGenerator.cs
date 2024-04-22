// <copyright file="APIGenerator.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace LateStartStudio.Hero6.Generators
{
    [Generator]
    public class APIGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            GenerateEntityAPI(context, "Animation");
            GenerateEntityAPI(context, "CharacterAnimation", "Animation");
            GenerateEntityAPI(context, "Character", sb =>
            {
                sb.AppendLine(
$@"     public static ICharacter Player => GlobalServiceProvider.Instance.GetService<ICharacterRepository>().Player;");
            });
            GenerateEntityAPI(context, "InventoryItem");
            GenerateEntityAPI(context, "Item");
            GenerateEntityAPI(context, "Room");

            context.RegisterPostInitializationOutput(callback =>
            {
                callback.AddSource($"GlobalUsings.API.g.cs", SourceText.From("global using LateStartStudio.Hero6.API;", Encoding.UTF8));
            });
        }

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity, Action<StringBuilder> add = null)
            => GenerateEntityAPI(context, entity, entity, add);

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity, string namespaceOverride, Action<StringBuilder> add = null)
        {
            ////if (!System.Diagnostics.Debugger.IsAttached)
            ////    System.Diagnostics.Debugger.Launch();

            var builders = context.SyntaxProvider.CreateSyntaxProvider((node, cancellationToken) =>
            {
                if (node is InvocationExpressionSyntax invocation && invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    return invocation.ArgumentList.Arguments.Any() && memberAccess.Name.ToString().StartsWith($"Add<I{entity}Builder>");
                }

                return false;
            },
            (transformContext, cancellationToken) =>
            {
                return transformContext.Node as InvocationExpressionSyntax;
            });

            // Use compilation -> semantic model to check full namespace and name of the methods we're analyzing to verify their identity
            var methodsCompilation = builders
                .Combine(context.CompilationProvider)
                .Select((invocation, cancellationToken) =>
                    (invocation: invocation.Left,
                    methodSymbol: invocation.Right.GetSemanticModel(invocation.Left.SyntaxTree).GetSymbolInfo(invocation.Left, cancellationToken).Symbol as IMethodSymbol))
                .Where(m => m.methodSymbol?.ToDisplayString().StartsWith($"LateStartStudio.Hero6.Scripts.IDesigner.Add") ?? false)
                .Collect();

            context.RegisterSourceOutput(methodsCompilation, (spc, methods) =>
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

                foreach (var m in methods)
                {
                    var name = m.invocation.ArgumentList.Arguments.First().ToString().Replace("\"", string.Empty);

                    sb.AppendLine($@"     public static I{entity} {name.Replace(" ", string.Empty)} => {entity}Repository[""{name}""];");
                }

                sb.AppendLine(
$@"     private static IRepository<I{entity}> {entity}Repository => GlobalServiceProvider.Instance.GetService<IRepository<I{entity}>>();");

                add?.Invoke(sb);
                sb.AppendLine($"}}");

                spc.AddSource($"{entity}sAPI.g.cs", sb.ToString());
            });
        }
    }
}
