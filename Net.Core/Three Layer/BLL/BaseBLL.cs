using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T>where T:class,new()
    {
        public IDBOperating<T> CurrentDAL;
        public BaseBLL() { SetCurrentDAL(); }
        public abstract void SetCurrentDAL();
        public int Add(List<T> obj)
        {
            return CurrentDAL.Add(obj);
        }
        public int Add(T obj)
        {
            List<T> list = new List<T>();
            list.Add(obj);
            return CurrentDAL.Add(list);
        }
        public int Delete(List<T> obj)
        {
            return CurrentDAL.Delete(obj);
        }
        public int Delete(T obj)
        {
            List<T> list = new List<T>();
            list.Add(obj);
            return CurrentDAL.Delete(list);
        }
        public int Excute(string _sql)
        {
            return CurrentDAL.Excute(_sql);
        }
        public string ExecuteScalarString(string _sql)
        {
            return CurrentDAL.ExecuteScalarString(_sql);
        }
        public List<T> Find()
        {
            return CurrentDAL.Find();
        }
        public List<T> Find(string _sql)
        {
            return CurrentDAL.Find(_sql);
        }
        public int Update(List<T> obj)
        {
            return CurrentDAL.Update(obj);
        }
        public int Update(T obj)
        {
            List<T> list = new List<T>();
            list.Add(obj);
            return CurrentDAL.Update(list);
        }
    }
}
