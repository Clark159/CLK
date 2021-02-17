using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No006
{
    public class TopEntity
    {
        // Constructors
        public TopEntity(SubEntity subEntity)
        {
            #region Contracts

            if (subEntity == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            this.SubEntity = subEntity;
        }


        // Properties   
        public SubEntity SubEntity { get; private set; }
    }
}
