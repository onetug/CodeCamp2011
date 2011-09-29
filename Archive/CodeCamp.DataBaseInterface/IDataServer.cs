using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.DataServerInterface
{
    public interface IDataServer
    {
        void Add<T>(T aObject)where T:class;
        void Commit();
        IQueryable<T> GetTable<T>() where T:class;
        void Remove<T>(T aObject) where T : class;
        void Rollback();
    }
}
