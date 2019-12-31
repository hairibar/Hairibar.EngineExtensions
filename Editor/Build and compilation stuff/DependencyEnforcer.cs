using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using System.Collections.Generic;

namespace Hairibar.EditorExtensions.Core
{
    /// <summary>
    /// Ensures that the AssemblyDefinition that contains this file only has the dependencies that are expliticly allowed here.
    /// All Unity assemblies are implicitly allowed.
    /// </summary>
    [InitializeOnLoad]
    public static class DependencyEnforcer
    {
        private readonly static string SCRIPT_PATH = "Assets/Plugins/Hairibar.EditorExtensions/Core/DependencyEnforcer.cs";

        private readonly static string[] ALLOWED_DEPENDENCIES =
        {
            "Hairibar.EngineExtensions"
        };

        /// <summary>
        /// Register to the event
        /// </summary>
        static DependencyEnforcer()
        {
            CompilationPipeline.assemblyCompilationStarted -= OnAssemblyCompilationStarted;
            CompilationPipeline.assemblyCompilationStarted += OnAssemblyCompilationStarted;
        }

        /// <summary>
        /// Recieves the compilation event, and when the time is right, calls EnforceDependencies with the assembly.
        /// </summary>
        private static void OnAssemblyCompilationStarted(string currentAssemblyName)
        {
            //Clean up the assembly name to get from path to name
            int lastFolderSeparatorIndex = currentAssemblyName.LastIndexOf("/");
            currentAssemblyName = currentAssemblyName.Substring(lastFolderSeparatorIndex + 1, currentAssemblyName.Length - lastFolderSeparatorIndex - 1);

            string scriptAssemblyName = CompilationPipeline.GetAssemblyNameFromScriptPath(SCRIPT_PATH);

            //We're processing the relevant assembly.
            if (currentAssemblyName == scriptAssemblyName)
            {
                //Take .dll off the assembly name
                scriptAssemblyName = scriptAssemblyName.Substring(0, scriptAssemblyName.LastIndexOf(".dll"));
                
                //Find the relevant assembly now that we have the name
                Assembly[] assemblies = CompilationPipeline.GetAssemblies();
                for (int i = 0; i < assemblies.Length; i++)
                {
                    if (assemblies[i].name == scriptAssemblyName)
                    {
                        Assembly assembly = assemblies[i];

                        EnforceDependencies(assembly, ALLOWED_DEPENDENCIES);
                        return;
                    }
                }
            }
        }

        private static void EnforceDependencies(Assembly assembly, string[] allowedDependencies)
        {
            Assembly[] references = assembly.assemblyReferences;
            List<string> nonAllowedDependencies = new List<string>();

            //Heh.
            foreach (Assembly referencedAss in references)
            {
                foreach (string allowedAss in allowedDependencies)
                {
                    if (referencedAss.name != allowedAss && referencedAss.name.IndexOf("Unity") != 0)
                    {
                        nonAllowedDependencies.Add(referencedAss.name);
                    }
                }

                if (nonAllowedDependencies.Count > 0)
                {
                    Debug.LogError($"Assembly <b>{assembly.name}</b> is only allowed to reference the following assemblies: " +
                        $"<b>{DependencyListToString(ALLOWED_DEPENDENCIES)}</b>. " +
                        $"Remove the following dependencies: <b>{DependencyListToString(nonAllowedDependencies.ToArray())}</b>.");
                }
            }
        }

        private static string DependencyListToString(string[] dependencies)
        {
            string result = "";

            for (int i = 0; i < dependencies.Length; i++)
            {
                result += dependencies[i];
                if (i < dependencies.Length - 1) result += ", ";
            }

            return result;
        }
    }
}
