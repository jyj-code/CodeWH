using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMongodb
    {
        bool Insert(Object t);
        bool Delete(string objId);
        bool Update(Object t, string objId);
        bool Update(string Obj, string ObjValue, string UObj, string UObjValue);
        T SelectOne<T>(string objId) where T : class;
        List<T> SelectAll<T>() where T : class;
    }
}
