// <copyright file="ScriptAPIGenerator.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LateStartStudio.Hero6.Generators
{
    [Generator]
    public class ScriptAPIGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            GenerateEntityAPI(context, "Animation");
            GenerateEntityAPI(context, "CharacterAnimation", parentNamespace: "Animations");
            GenerateEntityAPI(context, "Character", repositoryOverride: true);
            GenerateEntityAPI(context, "InventoryItem");
            GenerateEntityAPI(context, "Item");
            GenerateEntityAPI(context, "Room");
        }

        private void GenerateEntityAPI(IncrementalGeneratorInitializationContext context, string entity, string parentNamespace = null, bool repositoryOverride = false)
        {
            ////if (!System.Diagnostics.Debugger.IsAttached)
            ////    System.Diagnostics.Debugger.Launch();

            if (string.IsNullOrEmpty(parentNamespace))
                parentNamespace = $"{entity}s";

            var typeName = $"LateStartStudio.Hero6.{parentNamespace}.I{entity}";
            var repository = repositoryOverride ? $"LateStartStudio.Hero6.{entity}s.I{entity}Repository" : $"LateStartStudio.Hero6.Repositories.IRepository<{typeName}>";

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
$@"namespace LateStartStudio.Hero6.Scripts;

public static class {entity}s
{{");

                foreach (var m in methods)
                {
                    var name = m.invocation.ArgumentList.Arguments.First().ToString().Replace("\"", string.Empty);

                    sb.AppendLine($@"     public static {typeName} {name.Replace(" ", string.Empty)}(this {repository} repository) => repository[""{name}""];");
                }

                sb.AppendLine($"}}");

                spc.AddSource($"{entity}sScriptAPI.g.cs", sb.ToString());
            });
        }
    }
}
