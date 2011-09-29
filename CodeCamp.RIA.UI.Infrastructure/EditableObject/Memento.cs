using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeCamp.RIA.UI.Infrastructure
{
    public class Memento<T> where T: class
    {
        private Dictionary<PropertyInfo, object> storedProperties = new Dictionary<PropertyInfo, object>();

        public Memento(T originator)
        {
            this.InitializeMemento(originator);
        }

        public T Originator
        {
            get;
            protected set;
        }

        public void Restore(T originator)
        {
            foreach (var pair in this.storedProperties)
            {
                pair.Key.SetValue(originator, pair.Value, null);
            }
        }

        private void InitializeMemento(T originator)
        {
            if (originator == null)
                throw new ArgumentNullException("Originator", "Originator cannot be null");

            this.Originator = originator;

            IEnumerable<PropertyInfo> propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(p => p.CanRead && p.CanWrite);

            foreach (PropertyInfo property in propertyInfos)
                this.storedProperties[property] = property.GetValue(originator, null);
        }

        public Dictionary<PropertyInfo, object> GetStoredProperties()
        {
            return storedProperties;
        }
    }
}


