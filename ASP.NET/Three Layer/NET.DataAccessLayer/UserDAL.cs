using NET.Architect.Interface;
using System;
using System.Collections.Generic;
using NET.Architect.Model;
using DataBase;

namespace NET.DataAccessLayer
{
    public class UserDAL : IDBOperating<Architect.Model.User>
    {
        public string Sql { get; set; }
        public int Add(List<User> _entity)
        {
            Sql = @"insert into user(ID,username,password)values(@ID,@username,@password)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<User> _entity)
        {
            Sql = "DELETE FROM user WHERE Id=@Id";
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

        public List<User> Find()
        {
            Sql = "select * from user";
            return DBHelper.ReadCollection<User>(Sql, null);
        }

        public List<User> Find(string id)
        {
            Sql = string.Format("select * from user where id='{0}'",id);
            return DBHelper.ReadCollection<User>(Sql, null);
        }

        public int Update(List<User> _entity)
        {
            throw new NotImplementedException();
        }
    }
}
