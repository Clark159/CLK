using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection
{
    internal static class ReflectSettingTransformExtension
    {
        // Convert  
        public static ReflectBuilder ToBuilder(this ReflectSetting setting)
        {
            #region Contracts

            if (setting == null) throw new ArgumentNullException();

            #endregion

            // Type
            Type type = Type.GetType(setting.BuilderType);
            if (type == null) throw new InvalidOperationException(string.Format("Fail to create type:{0}", setting.BuilderType));

            // Builder
            ReflectBuilder builder = Activator.CreateInstance(type) as ReflectBuilder;
            if (type == null) throw new InvalidOperationException(string.Format("Fail to create instance:{0}", setting.BuilderType));

            // Parameters
            foreach (string parameterKey in setting.Parameters.Keys)
            {
                builder.Parameters.Add(parameterKey, setting.Parameters[parameterKey]);
            }

            // Return
            return builder;
        }
    }
}
