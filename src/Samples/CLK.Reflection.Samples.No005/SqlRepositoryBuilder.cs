using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No005
{
    public sealed class SqlRepositoryBuilder : ReflectBuilder
    {
        // Properties   
        public string ConnectionStringName
        {
            get { return this.GetParameter("ConnectionStringName"); }
            set { this.SetParameter("ConnectionStringName", value); }
        }


        // Methods          
        protected override object CreateEntity()
        {
            // ConnectionStringName
            string connectionStringName = this.ConnectionStringName;
            if (string.IsNullOrEmpty(connectionStringName) == true) throw new InvalidOperationException();

            // ConnectionString
            string connectionString = this.SettingContext.ConnectionStrings[connectionStringName];
            if (string.IsNullOrEmpty(connectionString) == true) throw new InvalidOperationException();

            // Create
            SqlRepository sqlRepository = new SqlRepository(connectionString);

            // Return
            return sqlRepository;
        }
    }
}
