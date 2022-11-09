// <copyright file="ScriptRepository.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Extensions.Logging;

namespace LateStartStudio.Hero6.Scripts;

public class ScriptRepository : IScriptRepository
{
    private readonly ILogger<ScriptRepository> logger;
    private readonly IEnumerable<Script> scripts;
    private readonly ILookup<Type, ScriptMethod> scriptMethods;

    public ScriptRepository(ILogger<ScriptRepository> logger, IEnumerable<Script> scripts)
    {
        this.logger = logger;
        this.scripts = scripts.GroupBy(s => s.GetType()).Select(s => s.First());
        scriptMethods = scripts
            .SelectMany(s => s.GetType().GetMethods().Select(m => (parent: s, method: m)))
            .Select(m => new ScriptMethod(m.parent, m.method))
            .Where(m => m.Attribute != null)
            .Where(m =>
            {
                var returnType = m.Method.ReturnType;

                if (!(returnType == typeof(void) || returnType == typeof(Task)))
                {
                    var message = $@"Return type must be ""void"" or ""{nameof(Task)}"" but currently it's ""{returnType}"".";
                    var exception = new ScriptEventException(message, m.Method);
                    logger.LogError(exception, message);
                    return false;
                }

                return true;
            })
            .ToLookup(m => m.Attribute.Type, m => m);
    }

    public IEnumerable<Script> GetScripts()
    {
        foreach (var script in scripts)
            yield return script;
    }

    public IEnumerable<ScriptMethod> GetScriptMethods() => scriptMethods.SelectMany(s => s);

    public IEnumerable<ScriptMethod> GetScriptMethods(Type type) => scriptMethods[type];
}
