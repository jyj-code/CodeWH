using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Template
    {
        D_Template DAL = new D_Template();
        public List<Template> Find()
        {
            return DAL.Find();
        }
        public List<Template> Find(string _sql)
        {
            return DAL.Find(_sql);
        }
        public string ExecuteScalarString(string _sql)
        {
            return DAL.ExecuteScalarString(_sql);
        }
        /// <summary>
        /// 根据模板ID获取模板
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetTemplate(string ID)
        {
            return DAL.GetTemplate(ID);
        }
    }
}
