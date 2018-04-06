using NET.Architect.Interface;
using System;
using System.Collections.Generic;
using NET.Architect.Model;
using NET.DataAccessLayer.Common;
using DataBase;

namespace NET.DataAccessLayer
{
    public class TruingLogDAL : IDBOperating<Architect.Model.TruingLog>
    {
        public string Sql { get; set; }

        public int Add(List<TruingLog> _entity)
        {
            Sql = @"insert into TruingLog(ID,UserId,SESSION,INFO,LAST_MODIFIED_TIME)values(@ID,@UserId,@SESSION,@INFO,@LAST_MODIFIED_TIME)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<TruingLog> _entity)
        {
            Sql = "DELETE FROM TruingLog WHERE Id=@Id";
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

        public List<TruingLog> Find()
        {
            Sql = "select a.*,b.* from TruingLog a left join CustomerUserInfo b on a.UserId=b.Id where a.UserId=b.Id order by a.LAST_MODIFIED_TIME desc limit 0,100";
            return DBHelper.ReadCollection<TruingLog>(Sql, null);
        }

        public List<TruingLog> Find(string id)
        {
            Sql = "select * from TruingLog where id=@id";
            return DBHelper.ReadCollection<TruingLog>(Sql, null);
        }

        public int Update(List<TruingLog> _entity)
        {
            throw new NotImplementedException();
        }
    }
}
