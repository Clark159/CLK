using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Settings;

namespace CLK.Reflection
{
    public partial class ReflectContext
    {
        // Locator
        private static ReflectContext _instance = null;

        public static ReflectContext Current
        {
            set
            {
                _instance = value;
            }
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException();
                }
                return _instance;
            }
        }
    }

    public partial class ReflectContext : IReflectContext
    {
        // Fields        
        private IReflectContext _reflectContext = null;

        private SettingContext _settingContext = null;


        // Constructors
        public ReflectContext(IReflectRepository reflectRepository, SettingContext settingContext)
        {
            #region Contracts

            if (reflectRepository == null) throw new ArgumentNullException();
            if (settingContext == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _reflectContext = this;
            _settingContext = settingContext;

            // ReflectGroupDictionary
            IReflectGroupRepository reflectGroupRepository = null;
            reflectGroupRepository = new ReflectGroupRepository(reflectRepository);
            reflectGroupRepository = new CacheReflectGroupRepository(reflectGroupRepository);
            this.ReflectGroups = new ReflectGroupDictionary(reflectGroupRepository);
        }


        // Properties
        public ReflectGroupDictionary ReflectGroups { get; private set; }


        // Methods        
        private TEntity CreateEntity<TEntity>(ReflectGroup group, string entityName) where TEntity : class
        {
            #region Contracts

            if (group == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Builder
            ReflectBuilder builder = group.ReflectBuilders[entityName];
            if (builder == null) throw new InvalidOperationException(string.Format("Fail to Get Builder:{0}", entityName));

            // EntityObject
            object entityObject = null;
            try
            {
                // Attach
                builder.ReflectContext = _reflectContext;
                builder.SettingContext = _settingContext;

                // Create
                entityObject = builder.CreateEntity();
                if (entityObject == null) throw new InvalidOperationException(string.Format("Fail to Create Entity:{0}", entityName));
            }
            finally
            {
                // Detach
                builder.ReflectContext = null;
                builder.SettingContext = null;
            }

            // Entity
            TEntity entity = entityObject as TEntity;
            if (entity == null) throw new InvalidOperationException(string.Format("Fail to Create Entity:{0}", typeof(TEntity)));

            // Return
            return entity;
        }

        public TEntity CreateEntity<TEntity>(string groupName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Group
            ReflectGroup group = this.ReflectGroups[groupName];
            if (group == null) throw new InvalidOperationException(string.Format("Fail to Get Group:{0}", groupName));

            // EntityName
            string entityName = group.DefaultEntityName;
            if (string.IsNullOrEmpty(entityName) == true) throw new InvalidOperationException(string.Format("Fail to Get DefaultEntityName:{0}", groupName));

            // Create
            return this.CreateEntity<TEntity>(group, entityName);
        }

        public TEntity CreateEntity<TEntity>(string groupName, string entityName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Group
            ReflectGroup group = this.ReflectGroups[groupName];
            if (group == null) throw new InvalidOperationException(string.Format("Fail to Get Group:{0}", groupName));

            // Create
            return this.CreateEntity<TEntity>(group, entityName);
        }

        public IEnumerable<TEntity> CreateAllEntity<TEntity>(string groupName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Group
            ReflectGroup group = this.ReflectGroups[groupName];
            if (group == null) return new TEntity[0];

            // Create
            List<TEntity> entityList = new List<TEntity>();
            foreach (string entityName in group.ReflectBuilders.Keys)
            {
                entityList.Add(this.CreateEntity<TEntity>(group, entityName));
            }
            return entityList;
        }
    }

    public interface IReflectContext
    {
        // Methods
        TEntity CreateEntity<TEntity>(string groupName) where TEntity : class;

        TEntity CreateEntity<TEntity>(string groupName, string entityName) where TEntity : class;

        IEnumerable<TEntity> CreateAllEntity<TEntity>(string groupName) where TEntity : class;
    }
}
