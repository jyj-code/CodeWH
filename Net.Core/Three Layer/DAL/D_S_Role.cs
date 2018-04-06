using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DatabaseOperation;

namespace DAL
{
    public class D_S_Role : Interface.IDBOperating<Model.S_Role>
    {
        public string Sql { get; set; }

        public int Add(List<S_Role> _entity)
        {
            Sql = "INSERT INTO S_Role(ID,NAME,Remark)VALUES(NEWID(),@NAME,@Remark)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<S_Role> _entity)
        {
            Sql = "DELETE S_Role WHERE ID=@ID";
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

        public List<S_Role> Find()
        {
            Sql = "select * from S_Role";
            return DBHelper.ReadCollection<S_Role>(Sql, null);
        }

        public List<S_Role> Find(string _sql)
        {
            return DBHelper.ReadCollection<S_Role>(_sql, null);
        }

        public int Update(List<S_Role> _entity)
        {
            Sql = "UPDATE S_Role SET NAME=@NAME,Remark=@Remark WHERE ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
