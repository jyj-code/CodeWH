using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DatabaseOperation;

namespace DAL
{
    public class D_S_Menu : Interface.IDBOperating<Model.S_Menu>
    {
        public string Sql { get; set; }

        public int Add(List<S_Menu> _entity)
        {
            Sql = "INSERT INTO S_Menu(ID,NAME,Remark,Controls,Action,Types,Sort)VALUES(@ID,@NAME,@Remark,@Controls,@Action,@Types,@Sort)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<S_Menu> _entity)
        {
            Sql = "DELETE S_Menu WHERE ID=@ID";
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

        public List<S_Menu> Find()
        {
            Sql = "select * from S_Menu";
            return DBHelper.ReadCollection<S_Menu>(Sql, null);
        }

        public List<S_Menu> Find(string _sql)
        {
            return DBHelper.ReadCollection<S_Menu>(_sql, null);
        }

        public int Update(List<S_Menu> _entity)
        {
            Sql = "UPDATE S_Menu SET NAME=@NAME,Remark=@Remark,Controls=@Controls,Action=@Action,Types=@Types,Sort=@Sort WHERE ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
