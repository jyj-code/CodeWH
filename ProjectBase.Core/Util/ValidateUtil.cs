using System.Collections.Generic;

namespace ProjectBase.Core.Util
{
    public class ValidateUtil
    {
        #region 检查是否有选中项

        /// <summary>
        /// 检查集合是否为0项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool HasItems<T>(IEnumerable<T> items)
        {
            if (items == null)
                return false;

            foreach (T t in items)
            {
                return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 是否是int类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        { 
            //TODO:
            int temp;
            if (int.TryParse(value,out temp))
            {
                return true;
            }
            else
                return false;
        }
    }

    public enum ValidateFileNameResult : byte
    {
        /// <summary>
        /// 验证通过
        /// </summary>
        Success = 0,

        /// <summary>
        /// 为空
        /// </summary>
        Empty = 1,

        /// <summary>
        /// 文件名太长
        /// </summary>
        TooLong = 2,

        /// <summary>
        /// 包含不支持的符号
        /// </summary>
        InvalidFileName = 3


    }
}