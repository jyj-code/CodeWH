using Dapper;
using DatabaseOperation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class D_Template
    {
        public string Sql { get; set; }
        public List<Template> Find()
        {
            Sql = "select * from Template";
            return DBHelper.ReadCollection<Template>(Sql, null);
        }

        public List<Template> Find(string _sql)
        {
            return DBHelper.ReadCollection<Template>(_sql, null);
        }
        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }
        public string GetTemplate(string ID)
        {
            return DBHelper.ExecuteScalarString(string.Format("select TEMPLATE_CONTENT from Template where id='{0}'",ID), null);
        }
    }
}
