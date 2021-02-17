using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No006
{
    public sealed class TopEntityBuilder : ReflectBuilder
    {
        // Properties   
        public string SubEntityGroupName
        {
            get { return this.GetParameter("SubEntityGroupName"); }
            set { this.SetParameter("SubEntityGroupName", value); }
        }


        // Methods          
        protected override object CreateEntity()
        {
            // SubEntityGroupName
            string subEntityGroupName = this.SubEntityGroupName;
            if (string.IsNullOrEmpty(subEntityGroupName) == true) throw new InvalidOperationException();

            // SubEntity
            SubEntity subEntity = this.ReflectContext.CreateEntity<SubEntity>(subEntityGroupName);
            if (subEntity == null) throw new InvalidOperationException();

            // Create
            TopEntity topEntity = new TopEntity(subEntity);

            // Return
            return topEntity;
        }
    }
}
