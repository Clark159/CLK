using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples
{
    public sealed class TestEntityBuilder : ReflectBuilder
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
            TestEntity testEntity = new TestEntity(parameterA);

            // Return
            return testEntity;
        }
    }
}
