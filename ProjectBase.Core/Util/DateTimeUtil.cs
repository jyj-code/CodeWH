using System;
using System.Text.RegularExpressions;

namespace ProjectBase.Core.Util
{
    public class DateTimeUtil
    {
        /// <summary>
        /// 系统当前时间（与数据库时间同步）
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now.AddMilliseconds(TimeIntervalFromDatabase);
            }
        }

        /// <summary>
        /// 从数据库获取的标准UTC时间
        /// </summary>
        public static DateTime UtcNow
        {
            get
            {
                return Now.AddHours(-DatabaseTimeDifference);
            }
        }

        public static DateTime SQLMinValue
        {
            get
            {
                return new DateTime(1753, 1, 1, 0, 00, 00);
            }
        }

        /// <summary>
        /// 数据库服务器与标准GMT时间的时差(单位小时)
        /// </summary>
        public static float DatabaseTimeDifference
        {
            get;
            set;
        }

        /// <summary>
        /// 程序当前时间与数据库当前时间的相差毫秒数
        /// </summary>
        private static double? m_TimeIntervalFromDatabase = null;

        public static double TimeIntervalFromDatabase
        {
            get
            {
                return m_TimeIntervalFromDatabase != null ? m_TimeIntervalFromDatabase.Value : 0;
            }
            set
            {
                m_TimeIntervalFromDatabase = value;
            }
        }
        /// <summary>
        /// 检查日期的有效性（那些年月日分开的日期， 有时后面的Day会超出当月最大值）
        /// </summary>
        /// <param name="birthYear"></param>
        /// <param name="birthMonth"></param>
        /// <param name="birthday"></param>
        public static DateTime CheckDateTime(int year, int month, int day)
        {
            if (month <= 0 && month > 12) month = 1;
            if (day <= 0) day = 1;
            if (year < DateTimeUtil.SQLMinValue.Year) year = DateTimeUtil.SQLMinValue.Year;

            if (year > 9999) year = 9999;

            if (month > 0 && month < 13)//天数检查
            {
                int temp = DateTime.DaysInMonth(year <= 0 || year > 9999 ? 2000 : year, month);
                if (day > temp)
                {
                    day = (short)temp;
                }
            }

            return new DateTime(year, month, day);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="datetimeString"></param>
        /// <param name="isEndDateTime"></param>
        /// <param name="considerationTiemDiff">是否考虑时差问题</param>
        /// <returns></returns>
        private static DateTime ConvertStringToDateTime(string datetimeString, bool isEndDateTime)
        {
            string[] englishMonth = new string[] { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

            string patternNumeric = @"^\d{1,2}(?=\D|$)"
                   , patternPlace = @"^\D*\d{1,2}\D*|$"
                   , patternYear = @"(?>(?<=\D|^))\d{4}(?>(?=\D|$))";

            int year
                , month = -1
                , day = 0
                , hour
                , minute
                , secound
                , temp;

            bool isEnglishMonth = false;

            if (isEndDateTime)
            {
                month = 12;
                hour = 23;
                minute = 59;
                secound = 59;
            }
            else
            {
                month = 1;
                day = 1;
                hour = 0;
                minute = 0;
                secound = 0;
            }

            DateTime returnValue;

            if (string.IsNullOrEmpty(datetimeString))
            {
                return isEndDateTime ? DateTime.MaxValue : DateTime.MinValue;
            }

            datetimeString = datetimeString.Trim().ToLower();

            if (!isEndDateTime)
            {
                if (DateTime.TryParse(datetimeString, out returnValue)) { return returnValue; } //如果可以直接转的话就用.NET直接转换
            }

            if (int.TryParse(Regex.Match(datetimeString, patternYear).Value, out year))
            {
                for (int i = 0; i < englishMonth.Length; i++)
                {
                    if (datetimeString.Contains(englishMonth[i]))
                    {
                        month = i + 1;
                        isEnglishMonth = true;
                        datetimeString = Regex.Replace(datetimeString, englishMonth[i] + @"\D*", "");
                        break;
                    }
                }
                datetimeString = Regex.Replace(datetimeString, year + @"\D*", "");
            }
            else
            {
                if (isEndDateTime)
                    return DateTime.MaxValue;
                else
                    return DateTime.MinValue;
                // year = DateTimeUtil.Now.Year;
                //throw new FormatException("无效的日期格式");
            }

            if (!isEnglishMonth)
            {
                if (int.TryParse(Regex.Match(datetimeString, patternNumeric).Value, out temp))
                {
                    month = temp;
                    datetimeString = Regex.Replace(datetimeString, patternPlace, "");
                }
            }

            if (int.TryParse(Regex.Match(datetimeString, patternNumeric).Value, out temp))
            {
                day = temp;
                datetimeString = Regex.Replace(datetimeString, patternPlace, "");
            }
            else
            {
                if (isEndDateTime) day = DateTime.DaysInMonth(year, month);
            }

            if (int.TryParse(Regex.Match(datetimeString, patternNumeric).Value, out temp))
            {
                hour = temp;
                datetimeString = Regex.Replace(datetimeString, patternPlace, "");
            }

            if (int.TryParse(Regex.Match(datetimeString, patternNumeric).Value, out temp))
            {
                minute = temp;
                datetimeString = Regex.Replace(datetimeString, patternPlace, "");
            }

            if (int.TryParse(Regex.Match(datetimeString, patternNumeric).Value, out temp))
            {
                secound = temp;
            }

            DateTime dtResult = new DateTime(year, month, day, hour, minute, secound, isEndDateTime ? 998 : 0);

            return dtResult;
        }

        /// <summary>
        /// 获取本周1的日期  时间为0点
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonday()
        {
            int dayOfWeek = (int)Now.DayOfWeek;
            DateTime monday;
            if (dayOfWeek == 0)
                monday = Now.AddDays(-6);
            else
                monday = Now.AddDays(1 - dayOfWeek);

            monday = new DateTime(monday.Year, monday.Month, monday.Day);

            return monday;
        }
    }
}
