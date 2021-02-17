using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CLK.AspNetCore
{
    internal static class AssemblyControllerExtension
    {
        // Methods
        public static void AddAssemblyController(this IMvcCoreBuilder builder, string controllerFilename)
        {
            #region Contracts

            if (builder == null) throw new ArgumentException();
            if (string.IsNullOrEmpty(controllerFilename) == true) throw new ArgumentException();

            #endregion

            // AssemblyFileList
            var assemblyFileList = FileHelper.GetAllFile(controllerFilename);
            if (assemblyFileList == null) throw new InvalidOperationException();

            // AddAssemblyController
            foreach (var assemblyFile in assemblyFileList)
            {
                // Assembly
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFile(assemblyFile.FullName);
                    if (assembly == null) throw new InvalidOperationException();
                }
                catch
                {
                    continue;
                }

                // Register
                builder.AddApplicationPart(assembly);
            }
        }
    }
}
