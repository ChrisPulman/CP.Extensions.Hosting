// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace CP.Extensions.Hosting.Plugins.Internals
{
    /// <summary>
    /// This is a wrapper class to simulate the behavior of the AssemblyLoadContext under the .NET Framework.
    /// </summary>
    public class AssemblyLoadContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyLoadContext"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AssemblyLoadContext(string name) => Name = name;

        /// <summary>
        /// Gets default AssemblyLoadContext.
        /// </summary>
        public static AssemblyLoadContext Default { get; } = new AssemblyLoadContext("default");

        /// <summary>
        /// Gets the name of the AssemblyLoadContext.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets all the available assemblies.
        /// </summary>
        public IEnumerable<Assembly> Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        /// <summary>
        /// Load the assembly by name.
        /// </summary>
        /// <param name="assemblyName">AssemblyName.</param>
        /// <returns>Assembly.</returns>
        public Assembly LoadFromAssemblyName(AssemblyName assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            // Attempt to load the assembly, using the same ordering as static load, in the current load context.
            return Load(assemblyName);
        }

        /// <summary>
        /// Load an assembly from the path.
        /// </summary>
        /// <param name="assemblyPath">string with the path to the assembly file.</param>
        /// <returns>Assembly.</returns>
        public Assembly LoadFromAssemblyPath(string assemblyPath) => Assembly.LoadFrom(assemblyPath);

        /// <summary>
        /// Implement the loading of the assembly.
        /// </summary>
        /// <param name="assemblyName">AssemblyName.</param>
        /// <returns>Assembly.</returns>
        protected virtual Assembly Load(AssemblyName assemblyName) => null!;

        /// <summary>
        /// Load the DLL from the specified path.
        /// </summary>
        /// <param name="dllPath">string.</param>
        /// <returns>IntPtr.</returns>
#pragma warning disable IDE0060 // Remove unused parameter
        protected IntPtr LoadUnmanagedDllFromPath(string dllPath) => IntPtr.Zero;
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Loads the specified DLL from the plugin path.
        /// </summary>
        /// <param name="unmanagedDllName">string.</param>
        /// <returns>IntPtr.</returns>
        protected virtual IntPtr LoadUnmanagedDll(string unmanagedDllName) => IntPtr.Zero;
    }
}
