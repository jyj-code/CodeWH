using DataBase;
using NET.Architect.Interface;
using NET.Architect.Model;
using NET.DataAccessLayer.Common;
using System.Collections.Generic;
using System.Linq;

namespace NET.DataAccessLayer
{
    public class KnowledgeBaseDAL : IDBOperating<Architect.Model.KnowledgeBase>
    {
        public string Sql { get; set; }
        public int Add(List<Architect.Model.KnowledgeBase> _entity)
        {
            Sql = @"insert into KnowledgeBase(CODE,INFO ,TEXT,USERID,LAST_MODIFIED_TIME,Ip,ClientAddressJson)values(@CODE,@INFO ,@TEXT,@USERID,@LAST_MODIFIED_TIME,@Ip,@ClientAddressJson)";
            return DBHelper.SaveCollection(Sql, _entity);
        }
        public int Delete(List<KnowledgeBase> _entity)
        {
            Sql = "DELETE FROM KnowledgeBase WHERE INFO=@INFO";
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

        public List<KnowledgeBase> Find()
        {
            Sql = "select * from KnowledgeBase";
            return DBHelper.ReadCollection<KnowledgeBase>(Sql, null);
        }

        public List<KnowledgeBase> Find(string INFO)
        {
            Sql = string.Format("select * from KnowledgeBase where upper(INFO)=upper('{0}')", INFO);
            return DBHelper.ReadCollection<KnowledgeBase>(Sql, null);
        }

        public int Update(List<KnowledgeBase> _entity)
        {
            Sql = "UPDATE KnowledgeBase SET TEXT=@TEXT,LAST_MODIFIED_TIME=@LAST_MODIFIED_TIME,USERID=@USERID,CODE=@CODE,ClientAddressJson=@ClientAddressJson WHERE INFO=@INFO";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
