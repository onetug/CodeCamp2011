using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CodeCamp.RIA.UI.Infrastructure
{

    /// <summary>
    /// This class encapsulates implements IEditableObject using the Memento pattern
    /// </summary>
    /// <remarks>
    /// Use only with POCO classes
    /// </remarks>
    /// <typeparam name="T">The reference type to hold a reserve copy of</typeparam>
    public class Caretaker<T> : IEditableObject where T: class
    {
        private Memento<T> memento;

        private T target;
        public T Target 
        {
            get
            {
                return this.target;
            }

            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Target", "Target cannot be null");
                }

                if (Object.ReferenceEquals(this.Target, value))
                    return;

                this.target = value;
            }
        }

        public Caretaker(T target)
        {
            this.Target = target;
        }

        public void BeginEdit()
        {
            if (this.memento == null)
                this.memento = new Memento<T>(this.Target);
        }

        public void CancelEdit()
        {
            if (this.memento == null)
                throw new ArgumentNullException("Memento", "BeginEdit() is not invoked");

            this.memento.Restore(Target);
            this.memento = null;
        }

        public void EndEdit()
        {
            if (this.memento == null)
                throw new ArgumentNullException("Memento", "BeginEdit() is not invoked");

            this.memento = null;
        }

        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                if (this.target != null && this.memento != null)
                {
                    PropertyInfo[] profileProperties = this.target.GetType().GetProperties();
                    Dictionary<PropertyInfo, object> editableProperties = this.memento.GetStoredProperties();
                    foreach (var profileProp in profileProperties)
                    {
                        foreach (var editableProp in editableProperties)
                        {
                            if (profileProp.Name == editableProp.Key.Name && profileProp.Name != "IsDirty")
                            {
                                if (!profileProp.GetValue(target, null).Equals(editableProp.Value))
                                {
                                    return true;
                                    // break;  JAS Unreachable Code
                                }
                            }
                        }
                    }
                    isDirty = (!memento.Equals(target));
                }
                return false;
            }
            set
            {
                isDirty = value;
            }
        }
    }
}
