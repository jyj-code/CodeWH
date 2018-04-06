using DataBase;
using NET.Architect.Common;
using NET.DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NET.DataAccessLayer
{
    public abstract class Base<T> where T : class
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public abstract string DbTableName { get; set; }

        #region 添加方法
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(List<T> entity)
        {
            T t = entity[0];
            StringBuilder str = new StringBuilder();
            StringBuilder strValues = new StringBuilder();
            foreach (var item in StatTool.GetObjKeyValue(t))
            {
                if (!string.IsNullOrEmpty(item.Value.Value))
                {
                    str.Append($" {item.Key},");
                    strValues.Append($" @{item.Key},");
                }
            }
            return DBHelper.SaveCollection(string.Format(ConstTool.Insert, t.GetType().Name, str.ToString().LnterceptLastOne(), strValues.ToString().LnterceptLastOne()), entity);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            return Add(new List<T>() { entity });
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(List<T> entity)
        {
            T t = entity[0];
            StringBuilder strWhere = new StringBuilder();
            foreach (var item in StatTool.GetObjKeyValue(t))
            {
                if (!string.IsNullOrEmpty(item.Value.Value) && item.Value.IsKey)
                    strWhere.Append($" {item.Key}=@{item.Key} And");
            }
            if (strWhere.Length < 1) return 0;
            return DBHelper.SaveCollection(string.Format(ConstTool.Delete, t.GetType().Name, strWhere.ToString().LnterceptLastThree()), entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            return Delete(new List<T>() { entity });
        }
        #endregion

        #region 修改
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(List<T> entity)
        {
            T t = entity[0];
            StringBuilder str = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            foreach (var item in StatTool.GetObjKeyValue(t))
            {
                if (!string.IsNullOrEmpty(item.Value.Value))
                {
                    if (item.Value.IsKey)
                        strWhere.Append($" {item.Key}=@{item.Key} And");
                    else
                        str.Append($" {item.Key}=@{item.Key},");
                }
            }
            if (strWhere.Length < 1) return 0;
            return DBHelper.SaveCollection(String.Format(ConstTool.Update, t.GetType().Name, str.ToString().LnterceptLastOne(), strWhere.ToString().LnterceptLastThree()).ToUpper(), entity);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return Update(new List<T>() { entity });
        }
        #endregion

        #region 查询
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
        public List<T> Find(T entity, int findId = 1)
        {
            #region 变量申明
            T t = entity;
            string flage = String.Empty;
            string flage2 = String.Empty;
            StringBuilder str = new StringBuilder();
            StringBuilder strWhere = new StringBuilder(); 
            #endregion
            if (entity== null)
                flage2 = "*";
            else
            {
                foreach (var item in StatTool.GetObjKeyValue(t))
                {
                    #region 组装Where条件
                    switch (findId)
                    {
                        case 1:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 1 && item.Value.IsKey)
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' Or");
                            }
                            break;
                        case 2:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 2 && item.Value.IsKey)
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' And");
                            }
                            break;
                        case 3:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 3)
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' Or");
                            }
                            break;
                        case 4:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 4)
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' And");
                            }
                            break;
                        case 5:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 5 && item.Value.Value.Trim() != "0")
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' Or");
                            }
                            break;
                        case 6:
                            if (!string.IsNullOrEmpty(item.Value.Value) && findId == 6 && item.Value.Value.Trim() != "0")
                            {
                                strWhere.Append($" {item.Key}='{item.Value.Value}' And");
                            }
                            break;
                    }
                    #endregion
                    str.Append($" {item.Key},");
                }
            }
            if (strWhere != null && strWhere.Length > 1)
                flage = $" WHERE {strWhere.ToString().LnterceptLastThree()}";
            if (str != null && str.Length > 1)
                flage2 = str.ToString().LnterceptLastOne();
            return DBHelper.ReadCollection<T>(String.Format(ConstTool.Select, flage2,DbTableName) + flage, null);
        }

        #endregion

        #region 执行SQL
        /// <summary>
        /// 执行SQL返回int类型
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }
        /// <summary>
        /// 执行SQL返回string类型
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }
        #endregion
    }
}
