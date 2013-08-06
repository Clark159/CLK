﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Settings;

namespace CLK.Reflection
{
    public abstract partial class ReflectContext
    {
        // Locator
        public static ReflectContext Current { get; set; }
    }

    public abstract partial class ReflectContext : IReflectContext
    {
        // Fields        
        private IReflectContext _reflectContext = null;

        private SettingContext _settingContext = null;


        // Properties
        public ReflectSectionDictionary ReflectSections { get; private set; }


        // Methods
        protected void Initialize(IReflectRepository reflectRepository, SettingContext settingContext)
        {
            #region Contracts

            if (reflectRepository == null) throw new ArgumentNullException();
            if (settingContext == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _reflectContext = this;
            _settingContext = settingContext;

            // Default        
            this.ReflectSections = new ReflectSectionDictionary(reflectRepository);
        }


        private TEntity CreateEntity<TEntity>(ReflectSection section, string entityName) where TEntity : class
        {
            #region Contracts

            if (section == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Builder
            ReflectBuilder builder = section.ReflectBuilders[entityName];
            if (builder == null) throw new InvalidOperationException(string.Format("Fail to Get Builder:{0}", entityName));

            // EntityObject
            object entityObject = builder.CreateEntity(_reflectContext, _settingContext);
            if (entityObject == null) throw new InvalidOperationException(string.Format("Fail to Create Entity:{0}", entityName));

            // Entity
            TEntity entity = entityObject as TEntity;
            if (entityObject == null) throw new InvalidOperationException(string.Format("Fail to Get Entity:{0}", typeof(TEntity)));

            // Return
            return entity;
        }

        public TEntity CreateEntity<TEntity>(string sectionName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(sectionName) == true) throw new ArgumentNullException();

            #endregion

            // Section
            ReflectSection section = this.ReflectSections[sectionName];
            if (section == null) throw new InvalidOperationException(string.Format("Fail to Get Section:{0}", sectionName));

            // EntityName
            string entityName = section.DefaultEntityName;
            if (string.IsNullOrEmpty(entityName) == true) throw new InvalidOperationException(string.Format("Fail to Get DefaultEntityName:{0}", sectionName));

            // Create
            return this.CreateEntity<TEntity>(section, entityName);
        }

        public TEntity CreateEntity<TEntity>(string sectionName, string entityName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(sectionName) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Section
            ReflectSection section = this.ReflectSections[sectionName];
            if (section == null) throw new InvalidOperationException(string.Format("Fail to Get Section:{0}", sectionName));

            // Create
            return this.CreateEntity<TEntity>(section, entityName);
        }

        public IEnumerable<TEntity> CreateAllEntity<TEntity>(string sectionName) where TEntity : class
        {
            #region Contracts

            if (string.IsNullOrEmpty(sectionName) == true) throw new ArgumentNullException();

            #endregion

            // Section
            ReflectSection section = this.ReflectSections[sectionName];
            if (section == null) return new TEntity[0];
                 
            // Create
            List<TEntity> entityList = new List<TEntity>();
            foreach (string entityName in section.ReflectBuilders.Keys)
            {
                entityList.Add(this.CreateEntity<TEntity>(section, entityName));
            }
            return entityList;
        }               
    }

    public interface IReflectContext
    {
        // Methods
        TEntity CreateEntity<TEntity>(string sectionName) where TEntity : class;

        TEntity CreateEntity<TEntity>(string sectionName, string entityName) where TEntity : class;

        IEnumerable<TEntity> CreateAllEntity<TEntity>(string sectionName) where TEntity : class;
    }
}
