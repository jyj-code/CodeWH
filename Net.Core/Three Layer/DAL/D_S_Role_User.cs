using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DatabaseOperation;

namespace DAL
{
    public class D_S_Role_User : Interface.IDBOperating<Model.S_Role_User>
    {
        public string Sql { get; set; }

        public int Add(List<S_Role_User> _entity)
        {
            Sql = "INSERT INTO S_Role_User(ID,USER_ID,MENU_ID)VALUES(@ID,USER_ID,MENU_ID)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<S_Role_User> _entity)
        {
            Sql = "DELETE S_Role_User WHERE ID=@ID";
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

        public List<S_Role_User> Find()
        {
            Sql = "select * from S_Role_User";
            return DBHelper.ReadCollection<S_Role_User>(Sql, null);
        }

        public List<S_Role_User> Find(string _sql)
        {
            return DBHelper.ReadCollection<S_Role_User>(_sql, null);
        }

        public int Update(List<S_Role_User> _entity)
        {
            Sql = "UPDATE S_Role_User SET USER_ID=USER_ID,MENU_ID=MENU_ID WHERE ID=ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
