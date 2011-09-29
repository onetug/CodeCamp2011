using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CodeCamp.CoreClasses;
using System.Data.Entity.Infrastructure;
using System.Reflection;


namespace CodeCamp.DataServerEF4
{
    [System.ComponentModel.Composition.Export]
    public class DataServer : DbContext, CodeCamp.DataServerInterface.IDataServer
    {
        public DataServer()
        {
        }
        public DataServer(string aDBName)
            : base(aDBName)
        {
        }

        [System.ComponentModel.Composition.ImportingConstructor]
        public DataServer(Dictionary<string, string> aSettings)
        {
            if(aSettings.ContainsKey("Assemblies")==false)
            {
                throw new Exception("No Assembly with persitsent classes passed");
            }
            Database.SetInitializer<DataServer>(null);
            string[] _PersistedAssemblies = aSettings["Assemblies"].Split(',');
            persistedClasses = new List<object>();
            foreach (string _AssemblyName in _PersistedAssemblies)
            {
                if (string.IsNullOrEmpty(_AssemblyName))
                    continue;
                Assembly _Assembly = Assembly.Load(_AssemblyName);
                if(_Assembly==null)
                {
                    throw new Exception("Cannot load assembly with name:" + _AssemblyName);
                }
                var _PersistedTypes = from _Type in _Assembly.GetTypes() where _Type.IsTransient() == false select _Type;
                persistedClasses.AddRange(_PersistedTypes);
            }

        }

        public void Add<T>(T aObject)where T:class
        {
            var _Table = this.Set<T>();
            _Table.Add(aObject);

        }
        public void Commit()
        {
            //TODO transactions are not implemented yet, must be done...
            this.SaveChanges();
        }
        public IQueryable<T> GetTable<T>() where T:class
        {
            var _Table=this.Set<T>();
            if(_Table==null)
            {
                throw new Exception("There is no persited class registered with name:"  + typeof(T).FullName);
            }
            return _Table;
        }

        private void RegisterClass(Type aClassType, System.Data.Entity.ModelConfiguration.ModelBuilder aModelBuilder, MethodInfo aRegisterMethod)
        {
            var _GenericMethod = aRegisterMethod.MakeGenericMethod(aClassType);
            _GenericMethod.Invoke(aModelBuilder, null);
        }

        public void Remove<T>(T aObject) where T : class
        {
            var _Table = this.Set<T>();
            _Table.Remove(aObject);
        }
        public void Rollback()
        {
            //TODO transactions are not implemented yet, must be done...
            throw new NotSupportedException();
        }

        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            MethodInfo _MethodInfo=typeof(System.Data.Entity.ModelConfiguration.ModelBuilder).GetMethod("RegisterSet", new Type[]{});
            foreach (Type _Class in persistedClasses)
            {
                RegisterClass(_Class, modelBuilder, _MethodInfo);
            }
            base.OnModelCreating(modelBuilder);
        }
        private static List<object> persistedClasses;
    }
}
