using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Reflection
{
    public interface IReflectRepository
    {
        // Methods
        void AddGroup(string groupName);

        void RemoveGroup(string groupName);

        bool ContainsGroup(string groupName);

        IEnumerable<string> GetAllGroupName();
                        
                
        void AddSetting(string groupName, string entityName, ReflectSetting setting);

        void RemoveSetting(string groupName, string entityName);

        bool ContainsSetting(string groupName, string entityName);                 

        IEnumerable<string> GetAllEntityName(string groupName);

        ReflectSetting GetSetting(string groupName, string entityName);     


        void AddDefaultEntityName(string groupName, string entityName);

        void RemoveDefaultEntityName(string groupName);

        bool ContainsDefaultEntityName(string groupName);

        string GetDefaultEntityName(string groupName);
    }
}
