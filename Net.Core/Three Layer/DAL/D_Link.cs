using DatabaseOperation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
   public class D_Link : Interface.IDBOperating<Model.Link>
    {
        public string Sql { get; set; }

        public int Add(List<Link> _entity)
        {
            Sql = @"INSERT INTO Link(ID,NAME,URL,REMARK,SORT,TYPES)VALUES(@ID,@NAME,@URL,@REMARK,@SORT,@TYPES)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<Link> _entity)
        {
            Sql = "DELETE Link WHERE NAME=@NAME and URL=@URL";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }

        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }

        public List<Link> Find()
        {
            Sql = "select * from Link order by sort";
            return DBHelper.ReadCollection<Link>(Sql, null);
        }

        public List<Link> Find(string _sql)
        {
            return DBHelper.ReadCollection<Link>(_sql, null);
        }

        public int Update(List<Link> _entity)
        {
            Sql = "UPDATE Link SET NAME=@NAME,REMARK=@REMARK,SORT=@SORT,TYPES=@TYPES where URL=@URL";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
