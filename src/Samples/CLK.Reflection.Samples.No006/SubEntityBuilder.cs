using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No006
{
    public sealed class SubEntityBuilder : ReflectBuilder
    {
        // Properties   
        public string ParameterA
        {
            get { return this.GetParameter("ParameterA"); }
            set { this.SetParameter("ParameterA", value); }
        }


        // Methods          
        protected override object CreateEntity()
        {
            // Parameters
            string parameterA = this.ParameterA;
            if (string.IsNullOrEmpty(parameterA) == true) throw new InvalidOperationException();

            // Create
            SubEntity subEntity = new SubEntity(parameterA);

            // Return
            return subEntity;
        }
    }
}
