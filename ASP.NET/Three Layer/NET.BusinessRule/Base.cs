using System.Collections.Generic;

namespace NET.BusinessRule
{
    public abstract class Base<T> where T : class, new()
    {
        public NET.DataAccessLayer.Base<T> CurrentDAL;
        public Base() { SetCurrentDAL(); }
        public abstract void SetCurrentDAL();
        public int Add(List<T> obj)
        {
            return CurrentDAL.Add(obj);
        }
        public int Add(T obj)
        {
            return CurrentDAL.Add(obj);
        }
        public int Delete(List<T> obj)
        {
            return CurrentDAL.Delete(obj);
        }
        public int Delete(T obj)
        {
            return CurrentDAL.Delete(obj);
        }
        public int Excute(string _sql)
        {
            return CurrentDAL.Excute(_sql);
        }
        public string ExecuteScalarString(string _sql)
        {
            return CurrentDAL.ExecuteScalarString(_sql);
        }
        /// <summary>
        /// 查询
        /// <param name="entity">查询条件 查询全部 传null</param>
        /// <param name="findId">entity不等于null等的情况下
        /// 1根据主键查询Or关系，
        /// 2根据主键查询And关系,
        /// 3根据entity值作为查询条件Or关系，
        /// 4根据entity值作为查询条件And关系，
        /// 5根据entity值作为查询条件Or关系忽略Int类型等于0，
        /// 6根据entity值作为查询条件And关系忽略Int类型等于0，
        /// <returns></returns>
        public List<T> Find(T t, int findId = 1)
        {
            return CurrentDAL.Find(t,findId);
        }
        public int Update(List<T> obj)
        {
            return CurrentDAL.Update(obj);
        }
        public int Update(T obj)
        {
            return CurrentDAL.Update(obj);
        }
    }
}
