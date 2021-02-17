using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection
{
    internal static class ReflectBuilderTransformExtension
    {
        // Convert  
        public static ReflectSetting ToSetting(this ReflectBuilder builder)
        {
            #region Contracts

            if (builder == null) throw new ArgumentNullException();

            #endregion

            // Create
            ReflectSetting setting = new ReflectSetting(builder.GetType().AssemblyQualifiedName);

            // Parameters
            foreach (string parameterKey in builder.Parameters.Keys)
            {
                setting.Parameters.Add(parameterKey, builder.Parameters[parameterKey]);
            }

            // Return
            return setting;
        }
    }
}
