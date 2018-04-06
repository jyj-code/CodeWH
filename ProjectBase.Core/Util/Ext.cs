using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace ProjectBase.Core.Util
{
    public static  class Ext
    {
        #region Is()
        /// <summary>
        ///     Returns 'true' if value is NOT null.
        /// </summary>
        /// <param name = "value"></param>
        /// <returns>true if object is not null</returns>
        public static bool Is(this object value)
        {
            return value != null;
        }

        public static bool Is<T>(this T? value)
            where T : struct
        {
            return value.HasValue;
        }
        #endregion Is()
        /// <summary>
        ///     if value is null 'true' is returned
        /// </summary>
        /// <param name = "value"></param>
        /// <returns>returns true if object is null</returns>
       
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        #region IsNull()
        /// <summary>
        ///     if value is null 'true' is returned
        /// </summary>
        /// <param name = "value"></param>
        /// <returns>returns true if object is null</returns>
       
        public static bool IsNull(this object value)
        {
            return value == null;
        }

       
        public static bool IsNull<T>(this T? value)
            where T : struct
        {
            return !value.HasValue;
        }
        #endregion IsNull()

        #region IsEmpty() IsNotEmpty() string
        /// <summary>
        ///     abbr. for (!string.IsNullOrEmpty(value)) : value.IsNotEmpty()
        /// </summary>
        /// <returns>true if not null NOR empty</returns>
       
        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        /// <summary>
        ///     abbr. for 'string.IsNullOrEmpty(value)' : value.IsEmpty()
        /// </summary>
        /// <returns>true if null or empty</returns>
       
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        #endregion IsEmpty() IsNotEmpty() string

        #region IsEmpty() IsNotEmpty() Enumerable
        /// <summary>
        ///     abbr. for 'value.Is() && value.Any()'
        /// </summary>
        /// <returns>true if not null NOR empty</returns>
       
        public static bool IsNotEmpty<T>(this IEnumerable<T> value)
        {
            return value.Is() && value.Any();
        }

        /// <summary>
        ///     abbr. for 'value.IsNull() || !value.Any'
        /// </summary>
        /// <returns>true if null or empty</returns>
       
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return !value.IsNotEmpty();
        }
        #endregion IsNotEmpty()

        #region IList<>
        public static IList<T> Append<T>(this IList<T> list, T value)
        {
            list.Add(value);
            return list;
        }

        public static int CountOrDefault<T>(this IEnumerable<T> list)
        {
            return list.IsNull() ? 0 : list.Count();
        }
        #endregion IList<>

        #region CreateUrl
        const string Solidus = "/";

        /// <summary>
        ///     Smart way how to create functional relative url.
        ///     Does not matter if running on root or virtual directory.
        ///     Controller without action needs different relative path then the one with action
        ///     in the uri
        /// </summary>
        /// <param name = "context">the object which gets this abbility</param>
        /// <param name = "target">relative path from root like "/Content/Css/01Basic.css"</param>
        /// <returns>corrected url, working from any page, any controller and action</returns>
        public static string CreateUrl(this HttpContext context, string target)
        {
            Contract.Requires(target.IsNotEmpty(), "cannot create relative url for null or empty target parameter ");
            Contract.Requires(context.Is(), "Cannot create URL, while passed context is null");

            var result = context.Request.ApplicationPath ?? string.Empty;
            var path = target;

            if (path.StartsWith(Solidus, StringComparison.OrdinalIgnoreCase))
            {
                path = path.Remove(0, 1);
            }

            if (!result.EndsWith(Solidus, StringComparison.OrdinalIgnoreCase))
            {
                result += Solidus;
            }

            return result + path;
        }

        public static string CreateUrl(this HttpContextBase context, string target)
        {
            Contract.Requires(target.IsNotEmpty(), "cannot create relative url for null or empty target parameter ");
            Contract.Requires(context.Is(), "Cannot create URL, while passed context is null");

            var result = context.Request.ApplicationPath ?? string.Empty;
            var path = target;

            if (path.StartsWith(Solidus, StringComparison.OrdinalIgnoreCase))
            {
                path = path.Remove(0, 1);
            }

            if (!result.EndsWith(Solidus, StringComparison.OrdinalIgnoreCase))
            {
                result += Solidus;
            }

            return result + path;
        }
        #endregion CreateUrl

        #region DataTable生成实体
        /// <summary>
        /// DataTable生成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : class, new()
        {
            if (dataTable == null || dataTable.Rows.Count <= 0) throw new ArgumentNullException("dataTable", "当前对象为null无法生成表达式树");
            Func<DataRow, T> func = dataTable.Rows[0].ToExpression<T>();
            List<T> collection = new List<T>(dataTable.Rows.Count);
            foreach (DataRow dr in dataTable.Rows)
            {
                collection.Add(func(dr));
            }
            return collection;
        }

        #endregion

        #region 生成表达式
        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static Func<DataRow, T> ToExpression<T>(this DataRow dataRow) where T : class, new()
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow", "当前对象为null 无法转换成实体");
            ParameterExpression paramter = Expression.Parameter(typeof(DataRow), "dr");
            List<MemberBinding> binds = new List<MemberBinding>();
            for (int i = 0; i < dataRow.ItemArray.Length; i++)
            {
                String colName = dataRow.Table.Columns[i].ColumnName;
                PropertyInfo pInfo = typeof(T).GetProperty(colName);
                if (pInfo == null) continue;
                MethodInfo mInfo = typeof(DataRowExtensions).GetMethod("Field", new Type[] { typeof(DataRow), typeof(String) }).MakeGenericMethod(pInfo.PropertyType);
                MethodCallExpression call = Expression.Call(mInfo, paramter, Expression.Constant(colName, typeof(String)));
                MemberAssignment bind = Expression.Bind(pInfo, call);
                binds.Add(bind);
            }
            MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(T)), binds.ToArray());
            return Expression.Lambda<Func<DataRow, T>>(init, paramter).Compile();
        }

        #endregion
    }
}
