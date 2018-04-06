using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDBOperating<T>
    {
        string Sql { get; set; }
        /// <summary>
        /// 返回集合
        /// </summary>
        /// <returns></returns>
        List<T> Find();
        /// <summary>
        /// 根据单条SQL返回集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> Find(string _sql);
        /// <summary>
        /// 集合增加到数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(List<T> _entity);
        /// <summary>
        /// 集合删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(List<T> _entity);
        /// <summary>
        /// 执行单条sql返回受影响行数
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        int Excute(string _sql);
        /// <summary>
        /// 集合更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(List<T> _entity);
        /// <summary>
        /// 查询字符串单个返回值
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        string ExecuteScalarString(string _sql);
    }
}
